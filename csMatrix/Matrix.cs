using System;
using System.Collections;
using System.Text;

namespace csMatrix
{
    /// <summary>
    /// A Matrix class for C#
    /// </summary>
    public class Matrix : IEnumerable
    {
        #region Fields
        /// <summary>
        /// Storage array for the Matrix data.
        /// </summary>
        private double[] data;

        /// <summary>
        /// The number of rows in this Matrix, before transposing.
        /// </summary>
        private int rows;

        /// <summary>
        /// The number of columns in this Matrix, before transposing.
        /// </summary>
        private int columns;
        #endregion

        #region Constructors
        /// <summary>
        /// Static constructor 
        /// </summary>
        static Matrix()
        {
            Arithmetic = new csMatrix.Arithmetic.Basic();
            RowColumnOperations = new csMatrix.RowColumnOperations.Basic();
            Populate = new csMatrix.Populate.Basic();
        }

        /// <summary>
        /// Constructor to create a new Matrix while specifying the number of rows and columns.
        /// </summary>
        /// <param name="rows">The number of rows to initialise the Matrix with.</param>
        /// <param name="columns">The number of columns to initialise the Matrix with.</param>
        /// <exception cref="ArgumentException">Thrown when the supplied dimensions are less than 1.</exception>
        public Matrix(int rows, int columns)
        {
            if (rows < 1 || columns < 1)
            {
                throw new ArgumentException("A Matrix instance must have dimensions of at least 1");
            }
            this.Rows = rows;
            this.Columns = columns;
            Size = rows * columns;
            Dimensions = new int[] { Rows, Columns };
            data = new double[Size];
            IsTransposed = false;
        }

        /// <summary>
        /// Constructor to create a new Matrix while specifying the number of rows,
        /// columns and default values.
        /// </summary>
        /// <param name="rows">The number of rows to initialise the Matrix with.</param>
        /// <param name="cols">The number of columns to initialise the Matrix with.</param>
        /// <param name="value">The value to fill the Matrix with.</param>
        /// <exception cref="ArgumentException">Thrown when the supplied dimensions are less than 1.</exception>
        public Matrix(int rows, int columns, double value) : this(rows, columns)
        {
            Fill(value);
        }

        /// <summary>
        /// Constructor to create a new square Matrix.
        /// </summary>
        /// <param name="dimensions">The number of rows and columns to initialise the
        /// Matrix with. There will be an equal number of rows and columns.</param>
        /// <exception cref="ArgumentException">Thrown when the supplied dimensions are less than 1.</exception>
        public Matrix(int dimensions) : this(dimensions, dimensions)
        {
        }

        /// <summary>
        /// Constructor to create a new square Matrix filled with a default value.
        /// </summary>
        /// <param name="dimensions">The number of rows and columns to initialise the
        /// Matrix with. There will be an equal number of rows and columns.</param>
        /// <param name="value">The value to fill the Matrix with.</param>
        /// <exception cref="ArgumentException">Thrown when the supplied dimensions are less than 1.</exception>
        public Matrix(int dimensions, double value) : this(dimensions, dimensions, value)
        {
        }

        /// <summary>
        /// Constructor to create a new Matrix based on an existing two-dimensional array.
        /// </summary>
        /// <param name="array">The array to specify values for a new Matrix.</param>
        /// <exception cref="NullReferenceException">Thrown when the array is null.</exception>
        public Matrix(double[,] array) : this(array.GetLength(0), array.GetLength(1))
        {
            int index = 0;
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    data[index++] = array[row, column];
                }
            }
        }

        /// <summary>
        /// Constructor to create a new Matrix based on an existing Matrix.
        /// </summary>
        /// <param name="m">The existing Matrix to specify values for a new Matrix.</param>
        /// <exception cref="NullReferenceException">Thrown when Matrix m is null.</exception>
        public Matrix(Matrix m) : this(m.Rows, m.Columns)
        {
            for (int i = 0; i < Size; i++)
                data[i] = m.data[i];
        }
        #endregion

        #region Properties
        /// <summary>
        /// The class used to perform Matrix arithmetic operations
        /// </summary>
        public static IMatrixArithmetic Arithmetic { get; set; }

        /// <summary>
        /// The class used to perform Matrix arithmetic operations
        /// </summary>
        public static IMatrixRowColumn RowColumnOperations { get; set; }

        /// <summary>
        /// The class used to populate a Matrix in different ways
        /// </summary>
        public static IMatrixPopulate Populate { get; set; }

        /// <summary>
        /// Indicates whether or not this Matrix row and column dimensions are equal.
        /// </summary>
        public bool IsSquare => Rows == Columns;

        /// <summary>
        /// Get the dimensions of this Matrix in a single-dimensional array of the form
        /// [rows,columns].
        /// </summary>
        public int[] Dimensions { get; }

        /// <summary>
        /// Get the number of rows in this Matrix.
        /// </summary>
        public int Rows
        {
            get { return IsTransposed ? columns : rows; }
            private set { rows = value; }
        }

        /// <summary>
        /// Get the number of columns in this Matrix.
        /// </summary>
        public int Columns
        {
            get { return IsTransposed ? rows : columns; }
            private set { columns = value; }
        }

        /// <summary>
        /// Get the total number of elements in this Matrix.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Indicates whether this Matrix is transposed (i.e. rows and columns are swapped)
        /// </summary>
        public bool IsTransposed { get; set; }
        #endregion

        #region Indexers
        /// <summary>
        /// Indexer to easily access a specific location in this Matrix.
        /// </summary>
        /// <param name="row">The row of the Matrix location to access.</param>
        /// <param name="column">The column of the Matrix location to access.</param>
        /// <returns>The value stored at the given row/column location.</returns>
        /// <remarks>Matrices are zero-indexed.</remarks>
        public double this[int row, int column]
        {
            get
            {
                return data[GetIndex(row, column)];
            }
            set
            {
                data[GetIndex(row, column)] = value;
            }
        }

        /// <summary>
        /// Indexer to directly access a position within an unrolled Matrix.
        /// </summary>
        /// <param name="index">The index within the complete Matrix, starting from
        /// row 0, column 0, and progressing through each row.</returns>
        /// <remarks>Matrices are zero-indexed. Providing a single-index alternative
        /// allows a simple way to iterate through the entire Matrix data in a single
        /// loop.</remarks>
        public double this[int index]
        {
            get { return data[index]; }
            set { data[index] = value; }
        }
        #endregion

        #region Operators
        /// <summary>
        /// Override the == operator to compare Matrix values.
        /// </summary>
        /// <param name="m1">The first Matrix to compare.</param>
        /// <param name="m2">The second Matrix to compare.</param>
        /// <returns>True if the values of both matrices match.</returns>
        public static bool operator ==(Matrix m1, Matrix m2)
        {
            return m1.Equals(m2);
        }

        /// <summary>
        /// Override the != operator to compare Matrix values.
        /// </summary>
        /// <param name="m1">The first Matrix to compare.</param>
        /// <param name="m2">The second Matrix to compare.</param>
        /// <returns>True if the values of both matrices differ.</returns>
        public static bool operator !=(Matrix m1, Matrix m2)
        {
            return !(m1 == m2);
        }

        /// <summary>
        /// Add two matrices together.
        /// </summary>
        /// <param name="m1">The first Matrix to add.</param>
        /// <param name="m2">The second Matrix to add.</param>
        /// <returns>The result of adding the two matrices together.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when both matrices have
        /// different dimensions.</exception>
        /// <exception cref="NullReferenceException">Thrown when either Matrix is null.</exception>
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            return Matrix.Add(m1, m2);
        }

        /// <summary>
        /// Add a number to each element in a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to add numbers to.</param>
        /// <param name="scalar">The number to add to each element in a Matrix.</param>
        /// <returns>The result of adding the number to each element in a Matrix.</returns>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static Matrix operator +(Matrix m1, double scalar)
        {
            return Matrix.Add(m1, scalar);
        }

        /// <summary>
        /// Add a number to each element in a Matrix.
        /// </summary>
        /// <param name="scalar">The number to add to each element in a Matrix.</param>
        /// <param name="m">The Matrix to add numbers to.</param>
        /// <returns>The result of adding the number to each element in a Matrix.</returns>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static Matrix operator +(double scalar, Matrix m1)
        {
            return Matrix.Add(m1, scalar);
        }

        /// <summary>
        /// Unary negative operator.
        /// </summary>
        /// <param name="m">The Matrix to negate.</param>
        /// <returns>The result of negating every element in the given Matrix.</returns>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static Matrix operator -(Matrix m)
        {
            return Matrix.Negate(m);
        }

        /// <summary>
        /// Subtract one Matrix from another.
        /// </summary>
        /// <param name="m1">The first Matrix to subtract from.</param>
        /// <param name="m2">The second Matrix to subtract from the first.</param>
        /// <returns>The result of subtracting the second Matrix from the first.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when both matrices have
        /// different dimensions.</exception>
        /// <exception cref="NullReferenceException">Thrown when either Matrix is null.</exception>
        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            return Matrix.Subtract(m1, m2);
        }

        /// <summary>
        /// Subtract a number from each element in a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to subtract from the number.</param>
        /// <param name="scalar">The number to subtract from each element in a Matrix.</param>
        /// <returns>The result of subracting a given number from each element.</returns>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static Matrix operator -(Matrix m, double scalar)
        {
            return Matrix.Subtract(m, scalar);
        }

        /// <summary>
        /// Subtract a number from each element in a Matrix.
        /// </summary>
        /// <param name="scalar">The number to subtract from each element in a Matrix.</param>
        /// <param name="m">The Matrix to subtract from the number.</param>
        /// <returns>The result of subracting a given number from each element.</returns>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static Matrix operator -(double scalar, Matrix m)
        {
            return Matrix.Subtract(m, scalar);
        }

        /// <summary>
        /// Multiply two matrices together.
        /// </summary>
        /// <param name="m1">An n*m dimension Matrix.</param>
        /// <param name="m2">An m*p dimension Matrix.</param>
        /// <returns>An n*p Matrix that is the product of m1 and m2.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when the number of columns in the
        /// first Matrix don't match the number of rows in the second Matrix.</exception>
        /// <exception cref="NullReferenceException">Thrown when either Matrix is null.</exception>
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            return Matrix.Multiply(m1, m2);
        }

        /// <summary>
        /// Scalar multiplication of a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to apply multiplication to.</param>
        /// <param name="scalar">The scalar value to multiply each element of the Matrix by.</param>
        /// <returns>A Matrix representing the scalar multiplication of m * scalar.</returns>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static Matrix operator *(Matrix m, double scalar)
        {
            return Matrix.Multiply(m, scalar);
        }

        /// <summary>
        /// Scalar multiplication of a Matrix.
        /// </summary>
        /// <param name="scalar">The scalar value to multiply each element of the Matrix by.</param>
        /// <param name="m">The Matrix to apply multiplication to.</param>
        /// <returns>A Matrix representing the scalar multiplication of m * scalar.</returns>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static Matrix operator *(double scalar, Matrix m)
        {
            return Matrix.Multiply(m, scalar);
        }

        /// <summary>
        /// Scalar division of a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to apply division to.</param>
        /// <param name="scalar">The scalar value to divide each element of the Matrix by.</param>
        /// <returns>A Matrix representing the scalar division of m / scalar.</returns>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static Matrix operator /(Matrix m, double scalar)
        {
            return Matrix.Divide(m, scalar);
        }
        #endregion

        #region Methods
        #region Instance Methods
        #region Generic Element Operations
        /// <summary>
        /// Perform the given operation on each Matrix element.
        /// </summary>
        /// <param name="op">The operation to perform on each Matrix element.</param>
        public void ElementOperation(Func<double, double> op)
        {
            Arithmetic.ElementOperation(this, op);
        }

        /// <summary>
        /// Perform the given operation on each Matrix element, using corresponding elements
        /// from a second Matrix.
        /// </summary>
        /// <param name="m">A second Matrix with the same dimensions as this one, whose elements
        /// will be used as the second parameter in each elementwise operation.</param>
        /// <param name="op">The operation to perform on each Matrix element.</param>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when both matrices have
        /// different dimensions.</exception>
        public void ElementOperation(Matrix m, Func<double, double, double> op)
        {
            if (!this.HasSameDimensions(m)) throw new InvalidMatrixDimensionsException("Cannot add Matrices with different dimensions");
            Arithmetic.ElementOperation(this, m, op);
        }

        /// <summary>
        /// Perform the given operation on each Matrix element.
        /// </summary>
        /// <param name="scalar">A scalar value to use as a second operand in each operation.</param>
        /// <param name="op">The operation to perform on each Matrix element.</param>
        public void ElementOperation(double scalar, Func<double, double, double> op)
        {
            Arithmetic.ElementOperation(this, scalar, op);
        }
        #endregion

        #region Arithmetic
        /// <summary>
        /// Add a Matrix to this instance.
        /// </summary>
        /// <param name="m">The Matrix to add to this one.</param>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when both matrices have
        /// different dimensions.</exception>
        /// <exception cref="NullReferenceException">Thrown when either Matrix is null.</exception>
        public void Add(Matrix m)
        {
            if (!this.HasSameDimensions(m)) throw new InvalidMatrixDimensionsException("Cannot add Matrices with different dimensions");
            Arithmetic.ElementOperation(this, m, (a, b) => a + b);
        }

        /// <summary>
        /// Add a number to each element in this Matrix.
        /// </summary>
        /// <param name="scalar">The number to add to each element in a Matrix.</param>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public void Add(double scalar)
        {
            Arithmetic.ElementOperation(this, scalar, (a, b) => a + b);
        }

        /// <summary>
        /// Unary negative operator, negates every element in this Matrix.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public void Negate()
        {
            Arithmetic.ElementOperation(this, a => -a);
        }

        /// <summary>
        /// Subtract a Matrix from this instance.
        /// </summary>
        /// <param name="m">The Matrix to subtract from this instance.</param>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when both matrices have
        /// different dimensions.</exception>
        /// <exception cref="NullReferenceException">Thrown when either Matrix is null.</exception>
        public void Subtract(Matrix m)
        {
            if (!this.HasSameDimensions(m)) throw new InvalidMatrixDimensionsException("Cannot add Matrices with different dimensions");
            Arithmetic.ElementOperation(this, m, (a, b) => a - b);
        }

        /// <summary>
        /// Subtract a number from each element in this Matrix.
        /// </summary>
        /// <param name="scalar">The number to subtract from each element in this Matrix.</param>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public void Subtract(double scalar)
        {
            Arithmetic.ElementOperation(this, scalar, (a, b) => a - b);
        }

        /// <summary>
        /// Scalar multiplication of this Matrix instance.
        /// </summary>
        /// <param name="scalar">The scalar value to multiply each element of the Matrix by.</param>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public void Multiply(double scalar)
        {
            Arithmetic.ElementOperation(this, scalar, (a, b) => a * b);
        }

        /// <summary>
        /// Scalar division of this Matrix instance.
        /// </summary>
        /// <param name="scalar">The scalar value to divide each element of the Matrix by.</param>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public void Divide(double scalar)
        {
            Arithmetic.ElementOperation(this, scalar, (a, b) => a / b);
        }
        #endregion

        #region Equality
        /// <summary>
        /// Override the Object.Equals method to compare Matrix values.
        /// </summary>
        /// <param name="obj">The object to compare to this Matrix.</param>
        /// <returns>True if obj is a Matrix, and its values match the current
        /// Matrix values.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Matrix m = obj as Matrix;
            return this.Equals(m);
        }

        /// <summary>
        /// Compare this Matrix with a second Matrix by its element's values.
        /// </summary>
        /// <param name="m">The Matrix to compare to this one.</param>
        /// <returns>True if both matrices contain the same elements.</returns>
        public bool Equals(Matrix m)
        {
            return Arithmetic.Equals(this, m);
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Override the default hash code.
        /// </summary>
        /// <returns>A bitwise XOR based on rows and columns of this Matrix.</returns>
        public override int GetHashCode()
        {
            return Rows ^ Columns;
        }

        /// <summary>
        /// Convert this Matrix to a string.
        /// </summary>
        /// <returns>A string representation of this Matrix.</returns>
        /// <remarks>All elements are rounded to two decimal places.</remarks>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int index = 0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    sb.AppendFormat("{0:0.00} ", data[index++]);
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// Implement the GetEnumerator method to run against the data array.
        /// </summary>
        /// <returns>Returns an enumerator for the data array.</returns>
        public IEnumerator GetEnumerator()
        {
            return data.GetEnumerator();
        }

        /// <summary>
        /// Checks if this Matrix has the same dimensions as another.
        /// </summary>
        /// <returns><c>true</c>, if this Matrix has the same dimensions as the
        /// other Matrix, <c>false</c> otherwise.</returns>
        /// <param name="m">A second Matrix to compare to this instance.</param>
        public bool HasSameDimensions(Matrix m)
        {
            return (this.Rows == m.Rows && this.Columns == m.Columns);
        }

        /// <summary>
        /// Get the direct index of a given row and column, taking into account
        /// in-memory transposition.
        /// </summary>
        /// <param name="row">The row position.</param>
        /// <param name="column">The column position.</param>
        /// <returns>A single index to access the given row and column.</returns>
        protected int GetIndex(int row, int column)
        {
            if (IsTransposed)
            {
                return (column * Rows) + row;
            }
            else
            {
                return (row * Columns) + column;
            }
        }

        #region Row/Column methods
        /// <summary>
        /// Swap two rows in this Matrix.
        /// </summary>
        /// <param name="row1">The first row to swap.</param>
        /// <param name="row2">The second row to swap.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when called with non-existent rows.</exception>
        /// <remarks>This method updates the instance it's called on.</remarks>
        public void SwapRows(int row1, int row2)
        {
            RowColumnOperations.SwapRows(this, row1, row2);
        }

        /// <summary>
        /// Swap two columns in this Matrix.
        /// </summary>
        /// <param name="column1">The first column to swap.</param>
        /// <param name="column2">The second column to swap.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when called with non-existent columns.</exception>
        /// <remarks>This method updates the instance it's called on.</remarks>
        public void SwapColumns(int column1, int column2)
        {
            RowColumnOperations.SwapColumns(this, column1, column2);
        }

        /// <summary>
        /// Transpose this Matrix, either permanently, or in-memory (i.e., the data array doesn't
        /// change, but accessing it does).
        /// </summary>
        /// <param name="InMemory">Indicates whether the Matrix should be transposed in-memory.</param>
        public void Transpose(bool InMemory)
        {
            if (InMemory)
            {
                IsTransposed = !IsTransposed;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region Populate
        /// <summary>
        /// Fills this Matrix with a given number.
        /// </summary>
        /// <param name="number">The number to assign to every element in the Matrix.</param>
        public void Fill(double number)
        {
            Populate.Number(this, number);
        }

        /// <summary>
        /// Fills this Matrix with zeros, except for ones along the main diagonal.
        /// </summary>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when Matrix is not square.</exception>
        public void Identity()
        {
            Populate.Identity(this);
        }

        /// <summary>
        /// Fills this Matrix with random numbers between 0.0 (inclusive) and 1.0 (exclusive).
        /// </summary>
        public void Rand()
        {
            Populate.Rand(this, (int)DateTime.Now.Ticks & 0x0000FFFF);
        }

        /// <summary>
        /// Fills this Matrix with random numbers between 0.0 (inclusive) and 1.0 (exclusive).
        /// </summary>
        /// <param name="seed">The number used to calculate a starting value.</param>
        public void Rand(int seed)
        {
            Populate.Rand(this, seed);
        }

        /// <summary>
        /// Fills this Matrix with zeros.
        /// </summary>
        /// <remarks>A new Matrix that doesn't specify any initial values will default
        /// to zeros. In that case, there is no need to call this method immediately
        /// afterwards.</remarks>
        public void Zeros()
        {
            Populate.Number(this, 0.0);
        }

        /// <summary>
        /// Fills this Matrix with ones.
        /// </summary>
        public void Ones()
        {
            Populate.Number(this, 1.0);
        }
        #endregion
        #endregion

        #region Static Methods
        /// <summary>
        /// Perform the given operation on each Matrix element.
        /// </summary>
        /// <param name="m">The Matrix to perform the operation on (remains unchanged).</param>
        /// <param name="scalar">A scalar value to use as a second operand in each operation.</param>
        /// <param name="op">The operation to perform on each Matrix element.</param>
        /// <returns>A new Matrix populated with the result of applying the given operation to
        /// Matrix m.</returns>
        public static Matrix ElementOperation(Matrix m, Func<double, double> op)
        {
            Matrix result = new Matrix(m);
            Arithmetic.ElementOperation(result, op);
            return result;
        }

        /// <summary>
        /// Perform the given operation on each Matrix element, using corresponding elements
        /// from a second Matrix.
        /// </summary>
        /// <param name="m1">The first Matrix to use within the given operation.</param>
        /// <param name="m2">A second Matrix with the same dimensions as the first, whose elements
        /// will be used as the second parameter in each elementwise operation.</param>
        /// <param name="op">The operation to perform on each Matrix element.</param>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when both matrices have
        /// different dimensions.</exception>
        public static Matrix ElementOperation(Matrix m1, Matrix m2, Func<double, double, double> op)
        {
            if (!m1.HasSameDimensions(m2)) throw new InvalidMatrixDimensionsException("Cannot operate Matrices with different dimensions");
            Matrix result = new Matrix(m1);
            Arithmetic.ElementOperation(result, m2, op);
            return result;
        }

        /// <summary>
        /// Perform the given operation on each Matrix element.
        /// </summary>
        /// <param name="m">The Matrix to perform the operation on (remains unchanged).</param>
        /// <param name="scalar">A scalar value to use as a second operand in each operation.</param>
        /// <param name="op">The operation to perform on each Matrix element.</param>
        /// <returns>A new Matrix populated with the result of applying the given operation to
        /// Matrix m.</returns>
        public static Matrix ElementOperation(Matrix m, double scalar, Func<double, double, double> op)
        {
            Matrix result = new Matrix(m);
            Arithmetic.ElementOperation(result, scalar, op);
            return result;
        }

        /// <summary>
        /// Add two matrices together.
        /// </summary>
        /// <param name="m1">The first Matrix to add.</param>
        /// <param name="m2">The second Matrix to add.</param>
        /// <returns>The result of adding the two matrices together.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when both matrices have
        /// different dimensions.</exception>
        /// <exception cref="NullReferenceException">Thrown when either Matrix is null.</exception>
        public static Matrix Add(Matrix m1, Matrix m2)
        {
            if (!m1.HasSameDimensions(m2)) throw new InvalidMatrixDimensionsException("Cannot add Matrices with different dimensions");
            Matrix result = new Matrix(m1);
            Arithmetic.ElementOperation(result, m2, (a, b) => a + b);
            return result;
        }

        /// <summary>
        /// Add a number to each element in a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to add numbers to.</param>
        /// <param name="scalar">The number to add to each element in a Matrix.</param>
        /// <returns>The result of adding the number to each element in a Matrix.</returns>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static Matrix Add(Matrix m, double scalar)
        {
            Matrix result = new Matrix(m);
            Arithmetic.ElementOperation(result, scalar, (a, b) => a + b);
            return result;
        }

        /// <summary>
        /// Unary negative operator.
        /// </summary>
        /// <param name="m">The Matrix to negate.</param>
        /// <returns>The result of negating every element in the given Matrix.</returns>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static Matrix Negate(Matrix m)
        {
            Matrix result = new Matrix(m);
            Arithmetic.ElementOperation(result, a => -a);
            return result;
        }

        /// <summary>
        /// Subtract one Matrix from another.
        /// </summary>
        /// <param name="m1">The first Matrix to subtract from.</param>
        /// <param name="m2">The second Matrix to subtract from the first.</param>
        /// <returns>The result of subtracting the second Matrix from the first.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when both matrices have
        /// different dimensions.</exception>
        /// <exception cref="NullReferenceException">Thrown when either Matrix is null.</exception>
        public static Matrix Subtract(Matrix m1, Matrix m2)
        {
            if (!m1.HasSameDimensions(m2)) throw new InvalidMatrixDimensionsException("Cannot subtract Matrices with different dimensions");
            Matrix result = new Matrix(m1);
            Arithmetic.ElementOperation(result, m2, (a, b) => a - b);
            return result;
        }

        /// <summary>
        /// Subtract a number from each element in a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to subtract from the number.</param>
        /// <param name="scalar">The number to subtract from each element in a Matrix.</param>
        /// <returns>The result of subracting a given number from each element.</returns>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static Matrix Subtract(Matrix m, double scalar)
        {
            Matrix result = new Matrix(m);
            Arithmetic.ElementOperation(result, scalar, (a, b) => a - b);
            return result;
        }

        /// <summary>
        /// Multiply two matrices together.
        /// </summary>
        /// <param name="m1">An n*m dimension Matrix.</param>
        /// <param name="m2">An m*p dimension Matrix.</param>
        /// <returns>An n*p Matrix that is the product of m1 and m2.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when the number of columns in the
        /// first Matrix don't match the number of rows in the second Matrix.</exception>
        /// <exception cref="NullReferenceException">Thrown when either Matrix is null.</exception>
        public static Matrix Multiply(Matrix m1, Matrix m2)
        {
            if (m1.Rows != m2.Columns) throw new InvalidMatrixDimensionsException("Matrices can only be multiplied if the number of columns in the first Matrix match the number of rows in the second Matrix");
            return Arithmetic.Multiply(m1, m2);
        }

        /// <summary>
        /// Scalar multiplication of a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to apply multiplication to.</param>
        /// <param name="scalar">The scalar value to multiply each element of the Matrix by.</param>
        /// <returns>A Matrix representing the scalar multiplication of m * scalar.</returns>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static Matrix Multiply(Matrix m, double scalar)
        {
            Matrix result = new Matrix(m);
            Arithmetic.ElementOperation(result, scalar, (a, b) => a * b);
            return result;
        }

        /// <summary>
        /// Scalar division of a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to apply division to.</param>
        /// <param name="scalar">The scalar value to divide each element of the Matrix by.</param>
        /// <returns>A Matrix representing the scalar division of m / scalar.</returns>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static Matrix Divide(Matrix m, double scalar)
        {
            Matrix result = new Matrix(m);
            Arithmetic.ElementOperation(result, scalar, (a, b) => a / b);
            return result;
        }
        #endregion
        #endregion
    }
}

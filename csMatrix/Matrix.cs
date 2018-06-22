using System;
using System.Collections;

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
        #endregion

        #region Constructors
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
            for (int i = 0; i < Size; i++)
                data[i] = value;
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
        public int Rows { get; }

        /// <summary>
        /// Get the number of columns in this Matrix.
        /// </summary>
        public int Columns { get; }

        /// <summary>
        /// Get the total number of elements in this Matrix.
        /// </summary>
        public int Size { get; }
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
            get { return data[(row * Columns) + column]; }
            set { data[(row * Columns) + column] = value; }
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
            return MatrixArithmetic.Add(m1, m2);
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
            return MatrixArithmetic.Add(m1, scalar);
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
            return MatrixArithmetic.Add(m1, scalar);
        }

        /// <summary>
        /// Unary negative operator.
        /// </summary>
        /// <param name="m">The Matrix to negate.</param>
        /// <returns>The result of negating every element in the given Matrix.</returns>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static Matrix operator -(Matrix m)
        {
            return MatrixArithmetic.Negate(m);
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
            return MatrixArithmetic.Subtract(m1, m2);
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
            return MatrixArithmetic.Subtract(m, scalar);
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
            return MatrixArithmetic.Subtract(m, scalar);
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
            return MatrixArithmetic.Multiply(m1, m2);
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
            return MatrixArithmetic.Multiply(m, scalar);
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
            return MatrixArithmetic.Multiply(m, scalar);
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
            return MatrixArithmetic.Divide(m, scalar);
        }
        #endregion

        #region Methods
        #region Instance Methods
        /// <summary>
        /// Add a Matrix to this instance.
        /// </summary>
        /// <param name="m">The Matrix to add to this one.</param>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when both matrices have
        /// different dimensions.</exception>
        /// <exception cref="NullReferenceException">Thrown when either Matrix is null.</exception>
        public void Add(Matrix m)
        {
            MatrixMutators.Add(this, m);
        }

        /// <summary>
        /// Add a number to each element in this Matrix.
        /// </summary>
        /// <param name="scalar">The number to add to each element in a Matrix.</param>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public void Add(double scalar)
        {
            MatrixMutators.Add(this, scalar);
        }

        /// <summary>
        /// Unary negative operator, negates every element in this Matrix.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public void Negate()
        {
            MatrixMutators.Negate(this);
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
            MatrixMutators.Subtract(this, m);
        }

        /// <summary>
        /// Subtract a number from each element in this Matrix.
        /// </summary>
        /// <param name="scalar">The number to subtract from each element in this Matrix.</param>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public void Subtract(double scalar)
        {
            MatrixMutators.Subtract(this, scalar);
        }

        /// <summary>
        /// Scalar multiplication of this Matrix instance.
        /// </summary>
        /// <param name="scalar">The scalar value to multiply each element of the Matrix by.</param>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public void Multiply(double scalar)
        {
            MatrixMutators.Multiply(this, scalar);
        }

        /// <summary>
        /// Scalar division of this Matrix instance.
        /// </summary>
        /// <param name="scalar">The scalar value to divide each element of the Matrix by.</param>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public void Divide(double scalar)
        {
            MatrixMutators.Divide(this, scalar);
        }

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
            if (object.ReferenceEquals(null, m)) return false;
            if (ReferenceEquals(this, m)) return true;

            if (!this.HasSameDimensions(m)) return false;

            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    if (this[row, column] != m[row, column]) return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Override the default hash code.
        /// </summary>
        /// <returns>A bitwise XOR based on rows and columns of this Matrix.</returns>
        public override int GetHashCode()
        {
            return Rows ^ Columns;
        }

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
        /// Swap two rows in this Matrix.
        /// </summary>
        /// <param name="row1">The first row to swap.</param>
        /// <param name="row2">The second row to swap.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when called with non-existent rows.</exception>
        /// <remarks>This method updates the instance it's called on.</remarks>
        public void SwapRows(int row1, int row2)
        {
            MatrixMutators.SwapRows(this, row1, row2);
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
            MatrixMutators.SwapColumns(this, column1, column2);
        }
        #endregion

        #region Static Methods
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
            return MatrixArithmetic.Add(m1, m2);
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
            return MatrixArithmetic.Add(m, scalar);
        }

        /// <summary>
        /// Unary negative operator.
        /// </summary>
        /// <param name="m">The Matrix to negate.</param>
        /// <returns>The result of negating every element in the given Matrix.</returns>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static Matrix Negate(Matrix m)
        {
            return MatrixArithmetic.Negate(m);
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
            return MatrixArithmetic.Subtract(m1, m2);
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
            return MatrixArithmetic.Subtract(m, scalar);
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
            return MatrixArithmetic.Multiply(m1, m2);
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
            return MatrixArithmetic.Multiply(m, scalar);
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
            return MatrixArithmetic.Divide(m, scalar);
        }
        #endregion
        #endregion
    }
}

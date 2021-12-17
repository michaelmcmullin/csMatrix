using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csMatrix
{
    /// <summary>
    /// Represents a Matrix
    /// </summary>
    public class Matrix : IEnumerable, IEquatable<Matrix>
    {
        #region Fields
        /// <summary>
        /// The number of rows in this <c>Matrix</c>, before transposing.
        /// </summary>
        private int rows;

        /// <summary>
        /// The number of columns in this <c>Matrix</c>, before transposing.
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
            TransposeOperations = new csMatrix.TransposeOperations.Basic();
            InverseOperations = new csMatrix.InverseOperations.Basic();
            Operations = new csMatrix.Operations.Basic();
        }

        /// <summary>
        /// Constructor to create a new <c>Matrix</c> while specifying the number of rows and columns.
        /// </summary>
        /// <param name="rows">The number of rows to initialise the <c>Matrix</c> with.</param>
        /// <param name="columns">The number of columns to initialise the <c>Matrix</c> with.</param>
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
            Data = new double[Size];
            IsTransposed = false;
        }

        /// <summary>
        /// Constructor to create a new <c>Matrix</c> while specifying the number of rows,
        /// columns and default values.
        /// </summary>
        /// <param name="rows">The number of rows to initialise the <c>Matrix</c> with.</param>
        /// <param name="columns">The number of columns to initialise the <c>Matrix</c> with.</param>
        /// <param name="value">The value to fill the <c>Matrix</c> with.</param>
        /// <exception cref="ArgumentException">Thrown when the supplied dimensions are less than 1.</exception>
        public Matrix(int rows, int columns, double value) : this(rows, columns)
        {
            Fill(value);
        }

        /// <summary>
        /// Constructor to create a new square <c>Matrix</c>.
        /// </summary>
        /// <param name="dimensions">The number of rows and columns to initialise the
        /// <c>Matrix</c> with. There will be an equal number of rows and columns.</param>
        /// <exception cref="ArgumentException">Thrown when the supplied dimensions are less than 1.</exception>
        public Matrix(int dimensions) : this(dimensions, dimensions)
        {
        }

        /// <summary>
        /// Constructor to create a new square <c>Matrix</c> filled with a default value.
        /// </summary>
        /// <param name="dimensions">The number of rows and columns to initialise the
        /// <c>Matrix</c> with. There will be an equal number of rows and columns.</param>
        /// <param name="value">The value to fill the <c>Matrix</c> with.</param>
        /// <exception cref="ArgumentException">Thrown when the supplied dimensions are less than 1.</exception>
        public Matrix(int dimensions, double value) : this(dimensions, dimensions, value)
        {
        }

        /// <summary>
        /// Constructor to create a new <c>Matrix</c> based on an existing two-dimensional array.
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
                    Data[index++] = array[row, column];
                }
            }
        }

        /// <summary>
        /// Constructor to create a new <c>Matrix</c> based on an existing one-dimensional array.
        /// </summary>
        /// <param name="rows">The number of rows to initialise the <c>Matrix</c> with.</param>
        /// <param name="columns">The number of columns to initialise the <c>Matrix</c> with.</param>
        /// <param name="data">The array to specify values for a new <c>Matrix</c>.</param>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when the given row and
        /// column count don't match the number of elements in the data array.</exception>
        /// <exception cref="NullReferenceException">Thrown when the array is null.</exception>
        public Matrix(int rows, int columns, double[] data) : this(rows, columns)
        {
            if ((rows * columns) != data.GetLength(0))
                throw new InvalidMatrixDimensionsException("Data array does not fit the required Matrix dimensions.");
            for(int index = 0; index < data.GetLength(0); index++)
            {
                this[index] = data[index];
            }
        }

        /// <summary>
        /// Constructor to create a new <c>Matrix</c> based on an existing Matrix.
        /// </summary>
        /// <param name="m">The existing <c>Matrix</c> to specify values for a new <c>Matrix</c>.</param>
        /// <exception cref="NullReferenceException">Thrown when <c>Matrix</c> <c>m</c> is null.</exception>
        public Matrix(Matrix m) : this(m.Rows, m.Columns)
        {
            if (!m.IsTransposed)
            {
                for (int i = 0; i < Size; i++)
                    Data[i] = m.Data[i];
            }
            else
            {
                int i = 0;
                foreach(double element in m)
                {
                    Data[i++] = element;
                }
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the class to use for <c>Matrix</c> arithmetic operations
        /// </summary>
        /// <value>
        /// A class instance implementing the <c>IMatrixArithmetic</c> interface
        /// which is used by the <c>Matrix</c> class to perform arithmetic. It
        /// defaults to a value of <c>csMatrix.Arithmetic.Basic</c>.
        /// </value>
        public static IMatrixArithmetic Arithmetic { get; set; }

        /// <summary>
        /// Gets or sets the class used to perform <c>Matrix</c> row/column operations
        /// </summary>
        /// <value>
        /// A class instance implementing the <c>IMatrixRowColumn</c> interface
        /// which is used by the <c>Matrix</c> class to manipulate rows and columns.
        /// It defaults to a value of <c>csMatrix.RowColumnOperations.Basic</c>.
        /// </value>
        public static IMatrixRowColumn RowColumnOperations { get; set; }

        /// <summary>
        /// Gets or sets the class used to populate a <c>Matrix</c> in different ways
        /// </summary>
        /// <value>
        /// A class instance implementing the <c>IMatrixPopulate</c> interface which
        /// is used to provide different ways of populating a <c>Matrix</c> instance.
        /// It defaults to a value of <c>csMatrix.Populate.Basic</c>.
        /// </value>
        public static IMatrixPopulate Populate { get; set; }

        /// <summary>
        /// Gets or sets the class used to perform <c>Matrix</c> transpose operations
        /// </summary>
        /// <value>
        /// A class instance implementing the <c>IMatrixTransposeOperations</c>
        /// interface which provides the <c>Matrix</c> class with transpose
        /// operations. It defaults to an instance of
        /// <c>csMatrix.TransposeOperations.Basic</c>.
        /// </value>
        public static IMatrixTransposeOperations TransposeOperations { get; set; }

        /// <summary>
        /// Gets or sets the class used to perform inverse <c>Matrix</c> operations
        /// </summary>
        /// <value>
        /// A class instance implementing the <c>IMatrixInverseOperations</c>
        /// interface which provides the <c>Matrix</c> class with inverse operations.
        /// It defaults to an instance of <c>csMatrix.InverseOperations.Basic</c>.
        /// </value>
        public static IMatrixInverseOperations InverseOperations { get; set; }

        /// <summary>
        /// Gets or sets the class used to perform general <c>Matrix</c> operations
        /// </summary>
        /// <value>
        /// A class instance implementing the <c>IMatrixOperations</c> interface
        /// which provides additional operations to the <c>Matrix</c> class. It
        /// defaults to <c>csMatrix.Operations.Basic</c>.
        /// </value>
        public static IMatrixOperations Operations { get; set; }

        /// <summary>
        /// Indicates whether or not this <c>Matrix</c> row and column dimensions are equal.
        /// </summary>
        public bool IsSquare => Rows == Columns;

        /// <summary>
        /// Indicates whether or not this <c>Matrix</c> is a Magic Square.
        /// </summary>
        public bool IsMagic => Populate.IsMagic(this);

        /// <summary>
        /// Get the dimensions of this <c>Matrix</c> in a single-dimensional array of the form
        /// <c>[rows,columns]</c>.
        /// </summary>
        public int[] Dimensions { get; private set; }

        /// <summary>
        /// Get the number of rows in this <c>Matrix</c>.
        /// </summary>
        public int Rows
        {
            get { return IsTransposed ? columns : rows; }
            private set { rows = value; }
        }

        /// <summary>
        /// Get the number of columns in this <c>Matrix</c>.
        /// </summary>
        public int Columns
        {
            get { return IsTransposed ? rows : columns; }
            private set { columns = value; }
        }

        /// <summary>
        /// Get the total number of elements in this <c>Matrix</c>.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Indicates whether this <c>Matrix</c> is transposed (i.e. rows and columns are swapped)
        /// </summary>
        public bool IsTransposed {
            get { return isTransposed; }
            set
            {
                if (value != isTransposed)
                {
                    isTransposed = value;
                    TransposeDimensions();
                }
            }
        }

        /// <summary>
        /// Backing variable for <c>IsTransposed</c>
        /// </summary>
        private bool isTransposed = false;

        /// <summary>
        /// Get the backing data array for this <c>Matrix</c>.
        /// </summary>
        public double[] Data { get; private set; }
        #endregion

        #region Indexers
        /// <summary>
        /// Indexer to easily access a specific location in this <c>Matrix</c>.
        /// </summary>
        /// <param name="row">The row of the <c>Matrix</c> location to access.</param>
        /// <param name="column">The column of the <c>Matrix</c> location to access.</param>
        /// <returns>The value stored at the given row/column location.</returns>
        /// <remarks>Matrices are zero-indexed.</remarks>
        public double this[int row, int column]
        {
            get
            {
                return Data[GetIndex(row, column)];
            }
            set
            {
                Data[GetIndex(row, column)] = value;
            }
        }

        /// <summary>
        /// Indexer to directly access a position within an unrolled <c>Matrix</c>.
        /// </summary>
        /// <param name="index">The index within the complete <c>Matrix</c>, starting from
        /// row 0, column 0, and progressing through each row.</param>
        /// <returns>The element at the given index.</returns>
        /// <remarks>Matrices are zero-indexed. Providing a single-index alternative
        /// allows a simple way to iterate through the entire <c>Matrix</c> data in a single
        /// loop.</remarks>
        public double this[int index]
        {
            get { return Data[GetTransposedIndex(index)]; }
            set { Data[GetTransposedIndex(index)] = value; }
        }
        #endregion

        #region Operators
        /// <summary>
        /// Override the <c>==</c> operator to compare <c>Matrix</c> values.
        /// </summary>
        /// <param name="m1">The first <c>Matrix</c> to compare.</param>
        /// <param name="m2">The second <c>Matrix</c> to compare.</param>
        /// <returns><c>true</c> if the values of both matrices match.</returns>
        public static bool operator ==(Matrix m1, Matrix m2)
        {
            if(object.ReferenceEquals(m1, null))
            {
                return object.ReferenceEquals(m2, null);
            }
            if (object.ReferenceEquals(m2, null)) return false;
            return m1.Equals(m2);
        }

        /// <summary>
        /// Override the <c>!=</c> operator to compare <c>Matrix</c> values.
        /// </summary>
        /// <param name="m1">The first <c>Matrix</c> to compare.</param>
        /// <param name="m2">The second <c>Matrix</c> to compare.</param>
        /// <returns><c>true</c> if the values of both matrices differ.</returns>
        public static bool operator !=(Matrix m1, Matrix m2)
        {
            return !(m1 == m2);
        }

        /// <summary>
        /// Add two matrices together.
        /// </summary>
        /// <param name="m1">The first <c>Matrix</c> to add.</param>
        /// <param name="m2">The second <c>Matrix</c> to add.</param>
        /// <returns>The result of adding the two matrices together.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when both matrices have
        /// different dimensions.</exception>
        /// <exception cref="NullReferenceException">Thrown when either <c>Matrix</c> is <c>null</c>.</exception>
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            return Matrix.Add(m1, m2);
        }

        /// <summary>
        /// Add a number to each element in a <c>Matrix</c>.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to add numbers to.</param>
        /// <param name="scalar">The number to add to each element in a <c>Matrix</c>.</param>
        /// <returns>The result of adding the number to each element in a <c>Matrix</c>.</returns>
        /// <exception cref="NullReferenceException">Thrown when <c>Matrix</c> is null.</exception>
        public static Matrix operator +(Matrix m, double scalar)
        {
            return Matrix.Add(m, scalar);
        }

        /// <summary>
        /// Add a number to each element in a <c>Matrix</c>.
        /// </summary>
        /// <param name="scalar">The number to add to each element in a <c>Matrix</c>.</param>
        /// <param name="m">The <c>Matrix</c> to add numbers to.</param>
        /// <returns>The result of adding the number to each element in a <c>Matrix</c>.</returns>
        /// <exception cref="NullReferenceException">Thrown when <c>Matrix</c> is null.</exception>
        public static Matrix operator +(double scalar, Matrix m)
        {
            return Matrix.Add(m, scalar);
        }

        /// <summary>
        /// Unary negative operator.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to negate.</param>
        /// <returns>The result of negating every element in the given <c>Matrix</c>.</returns>
        /// <exception cref="NullReferenceException">Thrown when <c>Matrix</c> is null.</exception>
        public static Matrix operator -(Matrix m)
        {
            return Matrix.Negate(m);
        }

        /// <summary>
        /// Subtract one <c>Matrix</c> from another.
        /// </summary>
        /// <param name="m1">The first <c>Matrix</c> to subtract from.</param>
        /// <param name="m2">The second <c>Matrix</c> to subtract from the first.</param>
        /// <returns>The result of subtracting the second <c>Matrix</c> from the first.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when both matrices have
        /// different dimensions.</exception>
        /// <exception cref="NullReferenceException">Thrown when either <c>Matrix</c> is null.</exception>
        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            return Matrix.Subtract(m1, m2);
        }

        /// <summary>
        /// Subtract a number from each element in a <c>Matrix</c>.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to subtract from the number.</param>
        /// <param name="scalar">The number to subtract from each element in a <c>Matrix</c>.</param>
        /// <returns>The result of subracting a given number from each element.</returns>
        /// <exception cref="NullReferenceException">Thrown when <c>Matrix</c> is null.</exception>
        public static Matrix operator -(Matrix m, double scalar)
        {
            return Matrix.Subtract(m, scalar);
        }

        /// <summary>
        /// Subtract a number from each element in a <c>Matrix</c>.
        /// </summary>
        /// <param name="scalar">The number to subtract from each element in a <c>Matrix</c>.</param>
        /// <param name="m">The <c>Matrix</c> to subtract from the number.</param>
        /// <returns>The result of subracting a given number from each element.</returns>
        /// <exception cref="NullReferenceException">Thrown when <c>Matrix</c> is null.</exception>
        public static Matrix operator -(double scalar, Matrix m)
        {
            return Matrix.Subtract(scalar, m);
        }

        /// <summary>
        /// Multiply two matrices together.
        /// </summary>
        /// <param name="m1">An n*m dimension <c>Matrix</c>.</param>
        /// <param name="m2">An m*p dimension <c>Matrix</c>.</param>
        /// <returns>An n*p <c>Matrix</c> that is the product of <c>m1</c> and <c>m2</c>.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when the number of columns in the
        /// first <c>Matrix</c> don't match the number of rows in the second <c>Matrix</c>.</exception>
        /// <exception cref="NullReferenceException">Thrown when either <c>Matrix</c> is null.</exception>
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            return Matrix.Multiply(m1, m2);
        }

        /// <summary>
        /// Scalar multiplication of a <c>Matrix</c>.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to apply multiplication to.</param>
        /// <param name="scalar">The scalar value to multiply each element of the <c>Matrix</c> by.</param>
        /// <returns>A <c>Matrix</c> representing the scalar multiplication of <c>m * scalar</c>.</returns>
        /// <exception cref="NullReferenceException">Thrown when <c>Matrix</c> is null.</exception>
        public static Matrix operator *(Matrix m, double scalar)
        {
            return Matrix.Multiply(m, scalar);
        }

        /// <summary>
        /// Scalar multiplication of a <c>Matrix</c>.
        /// </summary>
        /// <param name="scalar">The scalar value to multiply each element of the <c>Matrix</c> by.</param>
        /// <param name="m">The <c>Matrix</c> to apply multiplication to.</param>
        /// <returns>A <c>Matrix</c> representing the scalar multiplication of <c>m * scalar</c>.</returns>
        /// <exception cref="NullReferenceException">Thrown when <c>Matrix</c> is <c>null</c>.</exception>
        public static Matrix operator *(double scalar, Matrix m)
        {
            return Matrix.Multiply(m, scalar);
        }

        /// <summary>
        /// Scalar division of a <c>Matrix</c>.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to apply division to.</param>
        /// <param name="scalar">The scalar value to divide each element of the <c>Matrix</c> by.</param>
        /// <returns>A <c>Matrix</c> representing the scalar division of <c>m / scalar</c>.</returns>
        /// <exception cref="NullReferenceException">Thrown when <c>Matrix</c> is <c>null</c>.</exception>
        public static Matrix operator /(Matrix m, double scalar)
        {
            return Matrix.Divide(m, scalar);
        }
        #endregion

        #region Methods
        #region Instance Methods
        #region Matrix creation
        /// <summary>
        /// Replace this <c>Matrix</c> instance with data from another <c>Matrix</c>.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to obtain new data from.</param>
        public void Load(Matrix m)
        {
            if (!ReferenceEquals(this, m))
            {
                this.Rows = m.Rows;
                this.Columns = m.Columns;
                Size = rows * columns;
                Dimensions = new int[] { Rows, Columns };
                Data = new double[Size];
                IsTransposed = m.IsTransposed;
                for (int i = 0; i < Size; i++)
                    Data[i] = m.Data[i];
            }
        }
        #endregion

        #region General Element Operations
        /// <summary>
        /// Perform the given operation on each <c>Matrix</c> element.
        /// </summary>
        /// <param name="op">The operation to perform on each <c>Matrix</c> element.</param>
        /// <returns>A reference to this <c>Matrix</c> instance.</returns>
        public Matrix ElementOperation(Func<double, double> op)
        {
            Arithmetic.ElementOperation(this, op);
            return this;
        }

        /// <summary>
        /// Perform the given operation on each <c>Matrix</c> element, using corresponding elements
        /// from a second <c>Matrix</c>.
        /// </summary>
        /// <param name="m">A second <c>Matrix</c> with the same dimensions as this one, whose elements
        /// will be used as the second parameter in each elementwise operation.</param>
        /// <param name="op">The operation to perform on each <c>Matrix</c> element.</param>
        /// <returns>A reference to this <c>Matrix</c> instance.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when both matrices have
        /// different dimensions.</exception>
        public Matrix ElementOperation(Matrix m, Func<double, double, double> op)
        {
            if (!this.HasSameDimensions(m)) throw new InvalidMatrixDimensionsException("Cannot operate on Matrices with different dimensions");
            Arithmetic.ElementOperation(this, m, op);
            return this;
        }

        /// <summary>
        /// Perform the given operation on each <c>Matrix</c> element.
        /// </summary>
        /// <param name="scalar">A scalar value to use as a second operand in each operation.</param>
        /// <param name="op">The operation to perform on each <c>Matrix</c> element.</param>
        /// <returns>A reference to this <c>Matrix</c> instance.</returns>
        public Matrix ElementOperation(double scalar, Func<double, double, double> op)
        {
            Arithmetic.ElementOperation(this, scalar, op);
            return this;
        }
        #endregion

        #region Arithmetic
        /// <summary>
        /// Add a <c>Matrix</c> to this instance.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to add to this one.</param>
        /// <returns>A reference to this <c>Matrix</c> instance.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when both matrices have
        /// different dimensions.</exception>
        /// <exception cref="NullReferenceException">Thrown when either <c>Matrix</c> is null.</exception>
        /// <remarks><para>When using the fluent syntax to add a <c>Matrix</c> to itself repeatedly, be aware
        /// that the value will update after each call to this method:</para>
        /// <code>myMatrix.Add(myMatrix).Add(myMatrix);</code>
        /// <para>At first glance, this might look like it's the equivalent of <c>myMatrix * 3.0</c>.
        /// In fact, it's more like <c>myMatrix * 4.0</c>, because after the first <c>Add</c> method,
        /// <c>myMatrix</c> has now doubled. The second call is adding this updated value to itself,
        /// not the original value of <c>myMatrix</c>.</para>
        /// </remarks>
        public Matrix Add(Matrix m)
        {
            if (!this.HasSameDimensions(m)) throw new InvalidMatrixDimensionsException("Cannot add Matrices with different dimensions");
            Arithmetic.ElementOperation(this, m, (a, b) => a + b);
            return this;
        }

        /// <summary>
        /// Add a number to each element in this <c>Matrix</c>.
        /// </summary>
        /// <param name="scalar">The number to add to each element in a <c>Matrix</c>.</param>
        /// <returns>A reference to this <c>Matrix</c> instance.</returns>
        /// <exception cref="NullReferenceException">Thrown when <c>Matrix</c> is null.</exception>
        public Matrix Add(double scalar)
        {
            Arithmetic.ElementOperation(this, scalar, (a, b) => a + b);
            return this;
        }

        /// <summary>
        /// Unary negative operator, negates every element in this <c>Matrix</c>.
        /// </summary>
        /// <returns>A reference to this <c>Matrix</c> instance.</returns>
        /// <exception cref="NullReferenceException">Thrown when <c>Matrix</c> is null.</exception>
        public Matrix Negate()
        {
            Arithmetic.ElementOperation(this, a => -a);
            return this;
        }

        /// <summary>
        /// Subtract a <c>Matrix</c> from this instance.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to subtract from this instance.</param>
        /// <returns>A reference to this <c>Matrix</c> instance.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when both matrices have
        /// different dimensions.</exception>
        /// <exception cref="NullReferenceException">Thrown when either <c>Matrix</c> is null.</exception>
        /// <remarks><para>When using the fluent syntax to subtract a <c>Matrix</c> from itself repeatedly, be aware
        /// that the value will update after each call to this method:</para>
        /// <code>myMatrix.Subtract(myMatrix).Subtract(myMatrix);</code>
        /// <para>At first glance, this might look like it's the equivalent of <c>-myMatrix</c>.
        /// In fact, it will be a zero <c>Matrix</c>, because after the first <c>Subtract</c> method,
        /// <c>myMatrix</c> is now zero. The second call is subtracting this updated value from itself,
        /// not the original value of <c>myMatrix</c>.</para>
        /// </remarks>
        public Matrix Subtract(Matrix m)
        {
            if (!this.HasSameDimensions(m)) throw new InvalidMatrixDimensionsException("Cannot add Matrices with different dimensions");
            Arithmetic.ElementOperation(this, m, (a, b) => a - b);
            return this;
        }

        /// <summary>
        /// Subtract a number from each element in this Matrix.
        /// </summary>
        /// <param name="scalar">The number to subtract from each element in this <c>Matrix</c>.</param>
        /// <returns>A reference to this <c>Matrix</c> instance.</returns>
        /// <exception cref="NullReferenceException">Thrown when <c>Matrix</c> is null.</exception>
        public Matrix Subtract(double scalar)
        {
            Arithmetic.ElementOperation(this, scalar, (a, b) => a - b);
            return this;
        }

        /// <summary>
        /// Multiply this <c>Matrix</c> by another.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to multiply this instance by.</param>
        /// <returns>A <c>Matrix</c> that is the product of this instance and <c>m</c>.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when the number of rows in this
        /// <c>Matrix</c> instance don't match the number of columns in <c>Matrix</c> <c>m</c>.</exception>
        /// <exception cref="NullReferenceException">Thrown when either <c>Matrix</c> is null.</exception>
        public Matrix Multiply(Matrix m)
        {
            if (this.Columns != m.Rows) throw new InvalidMatrixDimensionsException("Matrices can only be multiplied if the number of columns in the first Matrix match the number of rows in the second Matrix");
            this.Load(Arithmetic.Multiply(this, m));
            return this;
        }

        /// <summary>
        /// Scalar multiplication of this <c>Matrix</c> instance.
        /// </summary>
        /// <param name="scalar">The scalar value to multiply each element of the <c>Matrix</c> by.</param>
        /// <returns>A reference to this <c>Matrix</c> instance.</returns>
        /// <exception cref="NullReferenceException">Thrown when <c>Matrix</c> is null.</exception>
        public Matrix Multiply(double scalar)
        {
            Arithmetic.ElementOperation(this, scalar, (a, b) => a * b);
            return this;
        }

        /// <summary>
        /// Scalar division of this <c>Matrix</c> instance.
        /// </summary>
        /// <param name="scalar">The scalar value to divide each element of the <c>Matrix</c> by.</param>
        /// <returns>A reference to this <c>Matrix</c> instance.</returns>
        /// <exception cref="NullReferenceException">Thrown when <c>Matrix</c> is null.</exception>
        public Matrix Divide(double scalar)
        {
            Arithmetic.ElementOperation(this, scalar, (a, b) => a / b);
            return this;
        }
        #endregion

        #region Equality
        /// <summary>
        /// Override the <c>Object.Equals</c> method to compare <c>Matrix</c> values.
        /// </summary>
        /// <param name="obj">The object to compare to this <c>Matrix</c>.</param>
        /// <returns><c>true</c> if <c>obj</c> is a <c>Matrix</c>, and its values match the current
        /// <c>Matrix</c> values.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Matrix m = obj as Matrix;
            return this.Equals(m);
        }

        /// <summary>
        /// Compare this <c>Matrix</c> with a second <c>Matrix</c> by its element's values.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to compare to this one.</param>
        /// <returns><c>true</c> if both matrices contain the same elements.</returns>
        public bool Equals(Matrix m)
        {
            return Arithmetic.Equals(this, m);
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Override the default hash code.
        /// </summary>
        /// <returns>A bitwise XOR based on rows and columns of this <c>Matrix</c>.</returns>
        public override int GetHashCode()
        {
            return Rows ^ Columns;
        }

        /// <summary>
        /// Convert this <c>Matrix</c> to a <c>string</c>.
        /// </summary>
        /// <returns>A <c>string</c> representation of this <c>Matrix</c>.</returns>
        /// <remarks>All elements are rounded to two decimal places.</remarks>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int index = 0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    sb.AppendFormat("{0:0.00} ", this[index++]);
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }
        #endregion

        #region Enumerators
        /// <summary>
        /// Implement the <c>GetEnumerator</c> method to run against the data array.
        /// </summary>
        /// <returns>Returns an enumerator for this <c>Matrix</c>.</returns>
        public IEnumerator GetEnumerator()
        {
            return new MatrixEnumerator(this);
        }

        /// <summary>
        /// An enumerator class for <c>Matrix</c> objects.
        /// </summary>
        public class MatrixEnumerator : IEnumerator<double>
        {
            private int currentIndex = -1;
            private Matrix currentMatrix;

            /// <summary>
            /// Constructor for <c>MatrixEnumerator</c>
            /// </summary>
            /// <param name="m">The <c>Matrix</c> to create an enumerator for.</param>
            public MatrixEnumerator(Matrix m)
            {
                currentMatrix = m;
            }

            /// <summary>
            /// Gets the element at the current position of the enumerator.
            /// </summary>
            public double Current
            {
                get
                {
                    if (currentIndex >= 0 && currentIndex < currentMatrix.Size)
                        return currentMatrix[currentIndex];
                    else
                        throw new IndexOutOfRangeException();
                }
            }

            /// <summary>
            /// Gets the element at the current position of the enumerator (nongeneric).
            /// </summary>
            object IEnumerator.Current
            {
                get { return Current; }
            }

            /// <summary>
            /// Empty <c>Dispose</c> method to satisfy <c>IEnumerator&lt;T&gt;</c> interface
            /// </summary>
            void IDisposable.Dispose()
            {
            }

            /// <summary>
            /// Advances the enumerator to the next element in the <c>Matrix</c>.
            /// </summary>
            /// <returns>true if successful, or false if it has advanced past the
            /// end of the <c>Matrix</c>.</returns>
            public bool MoveNext()
            {
                currentIndex++;
                return currentIndex < currentMatrix.Size;
            }

            /// <summary>
            /// Set the enumerator to its initial position, before the first element.
            /// </summary>
            public void Reset()
            {
                currentIndex = -1;
            }
        }
        #endregion

        #region Misc
        /// <summary>
        /// Checks if this <c>Matrix</c> has the same dimensions as another.
        /// </summary>
        /// <returns><c>true</c>, if this <c>Matrix</c> has the same dimensions as the
        /// other <c>Matrix</c>, <c>false</c> otherwise.</returns>
        /// <param name="m">A second <c>Matrix</c> to compare to this instance.</param>
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

        /// <summary>
        /// Get the direct index of an element taking into account in-memory transposition.
        /// </summary>
        /// <param name="index">The index of the requested element for a non-transposed <c>Matrix</c></param>
        /// <returns>A single index to access the correct element.</returns>
        /// <remarks>When accessing a <c>Matrix</c> sequentially, it runs through the columns of each
        /// row in turn. However, when the <c>Matrix</c> has been transposed in-memory, the same index
        /// will run through the rows of each column instead. This method is used by the indexer
        /// to ensure that all Matrices are accessed row by row.</remarks>
        protected int GetTransposedIndex(int index)
        {
            if (IsTransposed)
            {
                int row = index / Columns;
                int column = index % Columns;
                return (column * Rows) + row;
            }
            else
            {
                return index;
            }
        }
        #endregion

        #region Row/Column methods
        /// <summary>
        /// Swap two rows in this <c>Matrix</c>.
        /// </summary>
        /// <param name="row1">The first row to swap.</param>
        /// <param name="row2">The second row to swap.</param>
        /// <returns>A reference to this <c>Matrix</c> instance.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when called with non-existent rows.</exception>
        /// <remarks>This method updates the instance it's called on.</remarks>
        public Matrix SwapRows(int row1, int row2)
        {
            RowColumnOperations.SwapRows(this, row1, row2);
            return this;
        }

        /// <summary>
        /// Swap two columns in this <c>Matrix</c>.
        /// </summary>
        /// <param name="column1">The first column to swap.</param>
        /// <param name="column2">The second column to swap.</param>
        /// <returns>A reference to this <c>Matrix</c> instance.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when called with non-existent columns.</exception>
        /// <remarks>This method updates the instance it's called on.</remarks>
        public Matrix SwapColumns(int column1, int column2)
        {
            RowColumnOperations.SwapColumns(this, column1, column2);
            return this;
        }

        /// <summary>
        /// Inserts a number of additional columns to this <c>Matrix</c> instance, populating them
        /// with a given value.
        /// </summary>
        /// <param name="column">The column index to insert the new columns at.</param>
        /// <param name="count">The number of columns to insert.</param>
        /// <param name="value">The default value to add to the newly added columns.</param>
        /// <returns>A reference to this instance after the columns have been added.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when attempting to add columns outside
        /// the range of valid column indices (i.e. 0 to <c>this.Columns</c>).</exception>
        public Matrix AddColumns(int column, int count, double value)
        {
            if (count > 0)
                Load(RowColumnOperations.AddColumns(this, column, count, value));
            return this;
        }

        /// <summary>
        /// Inserts a number of additional rows to this <c>Matrix</c> instance, populating them
        /// with a given value.
        /// </summary>
        /// <param name="row">The row index to insert the new columns at.</param>
        /// <param name="count">The number of rows to insert.</param>
        /// <param name="value">The default value to add to the newly added rows.</param>
        /// <returns>A reference to this instance after the rows have been added.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when attempting to add rows outside
        /// the range of valid row indices (i.e. 0 to <c>this.Rows</c>).</exception>
        public Matrix AddRows(int row, int count, double value)
        {
            if (count > 0)
                Load(RowColumnOperations.AddRows(this, row, count, value));
            return this;
        }

        /// <summary>
        /// Removes a number of columns from this <c>Matrix</c> instance.
        /// </summary>
        /// <param name="column">The column index to start removal from.</param>
        /// <param name="count">The number of columns to remove.</param>
        /// <returns>A reference to this <c>Matrix</c> after the specified columns have been removed.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when attempting to remove columns outside
        /// the range of valid column indices (i.e. 0 to <c>m.Columns</c>).</exception>
        public Matrix RemoveColumns(int column, int count)
        {
            if (count > 0)
                Load(RowColumnOperations.RemoveColumns(this, column, count));
            return this;
        }

        /// <summary>
        /// Removes a number of rows from this <c>Matrix</c> instance.
        /// </summary>
        /// <param name="row">The row index to start removal from.</param>
        /// <param name="count">The number of rows to remove.</param>
        /// <returns>A reference to this <c>Matrix</c> after the specified rows have been removed.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when attempting to remove rows outside
        /// the range of valid row indices (i.e. 0 to <c>m.Rows</c>).</exception>
        public Matrix RemoveRows(int row, int count)
        {
            if (count > 0)
                Load(RowColumnOperations.RemoveRows(this, row, count));
            return this;
        }
        #endregion

        #region Transpose
        /// <summary>
        /// Transpose this <c>Matrix</c>, either permanently, or in-memory (i.e., the data array doesn't
        /// change, but accessing it does).
        /// </summary>
        /// <param name="SwapDimensions">Indicates whether the <c>Matrix</c> should be transposed by simply
        /// swapping the dimensions (i.e. rows become columns and vice versa).</param>
        /// <returns>A reference to this <c>Matrix</c> instance after transposing.</returns>
        /// <remarks>Swapping dimensions is a very quick way of transposing this <c>Matrix</c>. However,
        /// further operations may end up being slower. Setting <c>SwapDimensions</c> to <c>false</c>
        /// makes transposing a more expensive operation, but further operations may end up being
        /// faster.</remarks>
        public Matrix Transpose(bool SwapDimensions)
        {
            if (SwapDimensions)
            {
                IsTransposed = !IsTransposed;
            }
            else
            {
                Load(TransposeOperations.Transpose(this));
            }
            return this;
        }

        /// <summary>
        /// Multiply this instance by the transpose of a second <c>Matrix</c>.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to multiply this instance by its transpose.</param>
        /// <returns>A reference to this instance after being multiplied by the
        /// transpose of the given <c>Matrix</c>.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when the two <c>Matrix</c>
        /// instances have incompatible dimensions for multiplication.</exception>
        public Matrix MultiplyByTranspose(Matrix m)
        {
            if (this.Columns != m.Columns)
            {
                throw new InvalidMatrixDimensionsException("Cannot multiply by transpose because the second Matrix has a different number of columns to the first.");
            }
            Load(TransposeOperations.MultiplyByTranspose(this, m));
            return this;
        }

        /// <summary>
        /// Multiply this <c>Matrix</c> instance by its own transpose.
        /// </summary>
        /// <returns>A reference to this instance after being multiplied by its
        /// transpose.</returns>
        public Matrix MultiplyByOwnTranspose()
        {
            Load(TransposeOperations.MultiplyByTranspose(this));
            return this;
        }

        /// <summary>
        /// Multiply the transpose of this instance by a second <c>Matrix</c>.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to multiply the transpose of this instance by.</param>
        /// <returns>A reference to this instance after being transposed, then multiplied by
        /// the given <c>Matrix</c>.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when the two <c>Matrix</c>
        /// instances have incompatible dimensions for multiplication.</exception>
        public Matrix MultiplyTransposeBy(Matrix m)
        {
            if (this.Rows != m.Rows)
            {
                throw new InvalidMatrixDimensionsException("Cannot multiply transpose by this Matrix because the second Matrix has a different number of rows to the first.");
            }

            Load(TransposeOperations.MultiplyTransposeBy(this, m));
            return this;
        }

        /// <summary>
        /// Multiply the transpose of this instance by the original value of this instance.
        /// </summary>
        /// <returns>A reference to this instance after being transposed, then multiplied by
        /// the original value.</returns>
        public Matrix MultiplyOwnTransposeBy()
        {
            Load(TransposeOperations.MultiplyTransposeBy(this));
            return this;
        }

        /// <summary>
        /// Utility method to swap the dimensions array when transposed
        /// </summary>
        protected void TransposeDimensions()
        {
            int tmp = Dimensions[0];
            Dimensions[0] = Dimensions[1];
            Dimensions[1] = tmp;
        }
        #endregion

        #region Inverse
        /// <summary>
        /// Calculate the inverse of this <c>Matrix</c>.
        /// </summary>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when this <c>Matrix</c> is
        /// not square.</exception>
        /// <exception cref="NonInvertibleMatrixException">Thrown when this <c>Matrix</c> is not
        /// invertible.</exception>
        /// <returns>A reference to this <c>Matrix</c> instance after inversion.</returns>
        public Matrix Inverse()
        {
            Load(InverseOperations.Inverse(this));
            return this;
        }
        #endregion

        #region Populate
        /// <summary>
        /// Fills this <c>Matrix</c> with a given number.
        /// </summary>
        /// <param name="number">The number to assign to every element in the <c>Matrix</c>.</param>
        /// <returns>A reference to this <c>Matrix</c> instance.</returns>
        public Matrix Fill(double number)
        {
            Populate.Number(this, number);
            return this;
        }

        /// <summary>
        /// Fills this <c>Matrix</c> with zeros, except for ones along the main diagonal.
        /// </summary>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when <c>Matrix</c> is not square.</exception>
        /// <returns>A reference to this <c>Matrix</c> instance.</returns>
        public Matrix Identity()
        {
            Populate.Identity(this);
            return this;
        }

        /// <summary>
        /// Fills this <c>Matrix</c> with random numbers between 0.0 (inclusive) and 1.0 (exclusive).
        /// </summary>
        /// <returns>A reference to this <c>Matrix</c> instance.</returns>
        public Matrix Rand()
        {
            Populate.Rand(this, (int)DateTime.Now.Ticks & 0x0000FFFF);
            return this;
        }

        /// <summary>
        /// Fills this <c>Matrix</c> with random numbers between 0.0 (inclusive) and 1.0 (exclusive).
        /// </summary>
        /// <param name="seed">The number used to calculate a starting value.</param>
        /// <returns>A reference to this <c>Matrix</c> instance.</returns>
        public Matrix Rand(int seed)
        {
            Populate.Rand(this, seed);
            return this;
        }

        /// <summary>
        /// Fills this <c>Matrix</c> with zeros.
        /// </summary>
        /// <remarks>A new <c>Matrix</c> that doesn't specify any initial values will default
        /// to zeros. In that case, there is no need to call this method immediately
        /// afterwards.</remarks>
        /// <returns>A reference to this <c>Matrix</c> instance.</returns>
        public Matrix Zeros()
        {
            Populate.Number(this, 0.0);
            return this;
        }

        /// <summary>
        /// Fills this <c>Matrix</c> with ones.
        /// </summary>
        /// <returns>A reference to this <c>Matrix</c> instance.</returns>
        public Matrix Ones()
        {
            Populate.Number(this, 1.0);
            return this;
        }

        /// <summary>
        /// Populates this <c>Matrix</c> such that the sum of each row, column or diagonal
        /// equals the same constant.
        /// </summary>
        /// <returns>A reference to this <c>Matrix</c> instance.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when the dimensions
        /// of this <c>Matrix</c> are not square, or if the dimensions are 2 x 2.</exception>
        public Matrix Magic()
        {
            Populate.Magic(this);
            return this;
        }
        #endregion

        #region Operations
        /// <summary>
        /// Joins another <c>Matrix</c> onto this one.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to join to this one</param>
        /// <param name="dimension">Determines whether the second <c>Matrix</c> should be
        /// joined to this one by adding rows, columns, or automatically determine
        /// the most suitable dimension.</param>
        /// <returns>A reference to this instance after joining.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when the
        /// two <c>Matrix</c> instances don't share the correct number of elements along
        /// the specified dimension.</exception>
        public Matrix Join(Matrix m, MatrixDimension dimension)
        {
            Load(Operations.Join(this, m, dimension));
            return this;
        }

        /// <summary>
        /// Run a given operation on all elements in a particular dimension to reduce that dimension
        /// to a single row or column.
        /// </summary>
        /// <param name="dimension">Indicate whether to operate on rows or columns.</param>
        /// <param name="op">The <c>delegate</c> method to operate with.</param>
        /// <returns>A <c>Matrix</c> populated with the results of performing the given operation.</returns>
        /// <remarks>If the current <c>Matrix</c> is a row or column vector, then a 1*1 <c>Matrix</c>
        /// will be returned, regardless of which dimension is chosen. If the dimension is
        /// set to 'Auto', then the first non-singleton dimension is chosen. If no singleton
        /// dimension exists, then columns are used as the default.</remarks>
        public Matrix ReduceDimension(MatrixDimension dimension, Func<double, double, double> op)
        {
            Load(Operations.ReduceDimension(this, dimension, op));
            return this;
        }

        /// <summary>
        /// Run a set of operations on all elements in a particular dimension to reduce that dimension
        /// to a single row or column, and then perform an aggregate operation to produce a statistical
        /// result.
        /// </summary>
        /// <param name="dimension">Indicate whether to operate on rows or columns.</param>
        /// <param name="op">The <c>delegate</c> method to operate with.</param>
        /// <remarks>If the current <c>Matrix</c> is a row or column vector, then a 1*1 <c>Matrix</c>
        /// will be returned, regardless of which dimension is chosen. If the dimension is
        /// set to 'Auto', then the first non-singleton dimension is chosen. If no singleton
        /// dimension exists, then columns are used as the default.</remarks>
        public Matrix StatisticalReduce(MatrixDimension dimension, Func<Matrix, double> op)
        {
            Load(Operations.StatisticalReduce(this, dimension, op));
            return this;
        }
        #endregion

        #region Extraction
        /// <summary>
        /// Extract a number of columns from this <c>Matrix</c>, discarding the rest.
        /// </summary>
        /// <param name="column">The column index to start extraction from.</param>
        /// <param name="count">The number of columns to extract.</param>
        /// <returns>A reference to this <c>Matrix</c> after the columns have been extracted
        /// and the remaining columns discarded.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when attempting to extract columns outside
        /// the range of valid column indices (i.e. 0 to <c>m.Columns</c>).</exception>
        public Matrix ExtractColumns(int column, int count)
        {
            Load(RowColumnOperations.ExtractColumns(this, column, count));
            return this;
        }

        /// <summary>
        /// Extract a number of columns from this <c>Matrix</c>, discarding the rest.
        /// </summary>
        /// <param name="row">The row index to start extraction from.</param>
        /// <param name="count">The number of rows to extract.</param>
        /// <returns>A reference to this Matrix after the rows have been extracted
        /// and the remaining rows discarded.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when attempting to remove rows outside
        /// the range of valid row indices (i.e. 0 to <c>m.Rows</c>).</exception>
        public Matrix ExtractRows(int row, int count)
        {
            Load(RowColumnOperations.ExtractRows(this, row, count));
            return this;
        }

        /// <summary>
        /// Extract a portion of this <c>Matrix</c>, discarding the rest.
        /// </summary>
        /// <param name="startingIndex">The zero-based starting index of the <c>Matrix</c> to start
        /// extracting data from.</param>
        /// <param name="rows">The number of rows to extract.</param>
        /// <param name="columns">The number of columns to extract.</param>
        /// <returns>A reference to this <c>Matrix</c> after extraction.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when there are not
        /// enough elements to fill the extracted <c>Matrix</c>.</exception>
        public Matrix Extract(int startingIndex, int rows, int columns)
        {
            Load(Operations.Extract(this, startingIndex, rows, columns));
            return this;
        }

        /// <summary>
        /// Extract a portion of this <c>Matrix</c>, discarding the rest.
        /// </summary>
        /// <param name="startingRow">The zero-based row of this <c>Matrix</c> to start
        /// extracting data from.</param>
        /// <param name="startingColumn">The zero-based column of this <c>Matrix</c> to start
        /// extracting data from.</param>
        /// <param name="rows">The number of rows to extract.</param>
        /// <param name="columns">The number of columns to extract.</param>
        /// <returns>A reference to this <c>Matrix</c> after extraction.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when there are not
        /// enough elements to fill the extracted <c>Matrix</c>.</exception>
        public Matrix Extract(int startingRow, int startingColumn, int rows, int columns)
        {
            int startingIndex = GetIndex(startingRow, startingColumn);
            return Extract(startingIndex, rows, columns);
        }
        #endregion
        #endregion

        #region Static Methods
        #region General element operations
        /// <summary>
        /// Perform the given operation on each <c>Matrix</c> element.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to perform the operation on (remains unchanged).</param>
        /// <param name="op">The operation to perform on each <c>Matrix</c> element.</param>
        /// <returns>A new <c>Matrix</c> populated with the result of applying the given operation to
        /// <c>Matrix</c> <c>m</c>.</returns>
        public static Matrix ElementOperation(Matrix m, Func<double, double> op)
        {
            Matrix result = new Matrix(m);
            Arithmetic.ElementOperation(result, op);
            return result;
        }

        /// <summary>
        /// Perform the given operation on each <c>Matrix</c> element, using corresponding elements
        /// from a second <c>Matrix</c>.
        /// </summary>
        /// <param name="m1">The first <c>Matrix</c> to use within the given operation.</param>
        /// <param name="m2">A second <c>Matrix</c> with the same dimensions as the first, whose elements
        /// will be used as the second parameter in each elementwise operation.</param>
        /// <param name="op">The operation to perform on each <c>Matrix</c> element.</param>
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
        /// Perform the given operation on each <c>Matrix</c> element.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to perform the operation on (remains unchanged).</param>
        /// <param name="scalar">A scalar value to use as a second operand in each operation.</param>
        /// <param name="op">The operation to perform on each <c>Matrix</c> element.</param>
        /// <returns>A new <c>Matrix</c> populated with the result of applying the given operation to
        /// <c>Matrix</c> <c>m</c>.</returns>
        public static Matrix ElementOperation(Matrix m, double scalar, Func<double, double, double> op)
        {
            Matrix result = new Matrix(m);
            Arithmetic.ElementOperation(result, scalar, op);
            return result;
        }
        #endregion

        #region Arithmetic
        /// <summary>
        /// Add two matrices together.
        /// </summary>
        /// <param name="m1">The first <c>Matrix</c> to add.</param>
        /// <param name="m2">The second <c>Matrix</c> to add.</param>
        /// <returns>The result of adding the two matrices together.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when both matrices have
        /// different dimensions.</exception>
        /// <exception cref="NullReferenceException">Thrown when either <c>Matrix</c> is null.</exception>
        public static Matrix Add(Matrix m1, Matrix m2)
        {
            if (!m1.HasSameDimensions(m2)) throw new InvalidMatrixDimensionsException("Cannot add Matrices with different dimensions");
            Matrix result = new Matrix(m1);
            Arithmetic.ElementOperation(result, m2, (a, b) => a + b);
            return result;
        }

        /// <summary>
        /// Add a number to each element in a <c>Matrix</c>.
        /// </summary>
        /// <param name="m">The Matrix to add numbers to.</param>
        /// <param name="scalar">The number to add to each element in a <c>Matrix</c>.</param>
        /// <returns>The result of adding the number to each element in a <c>Matrix</c>.</returns>
        /// <exception cref="NullReferenceException">Thrown when <c>Matrix</c> is null.</exception>
        public static Matrix Add(Matrix m, double scalar)
        {
            Matrix result = new Matrix(m);
            Arithmetic.ElementOperation(result, scalar, (a, b) => a + b);
            return result;
        }

        /// <summary>
        /// Unary negative operator.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to negate.</param>
        /// <returns>The result of negating every element in the given <c>Matrix</c>.</returns>
        /// <exception cref="NullReferenceException">Thrown when <c>Matrix</c> is null.</exception>
        public static Matrix Negate(Matrix m)
        {
            Matrix result = new Matrix(m);
            Arithmetic.ElementOperation(result, a => -a);
            return result;
        }

        /// <summary>
        /// Subtract one <c>Matrix</c> from another.
        /// </summary>
        /// <param name="m1">The first <c>Matrix</c> to subtract from.</param>
        /// <param name="m2">The second <c>Matrix</c> to subtract from the first.</param>
        /// <returns>The result of subtracting the second <c>Matrix</c> from the first.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when both matrices have
        /// different dimensions.</exception>
        /// <exception cref="NullReferenceException">Thrown when either <c>Matrix</c> is null.</exception>
        public static Matrix Subtract(Matrix m1, Matrix m2)
        {
            if (!m1.HasSameDimensions(m2)) throw new InvalidMatrixDimensionsException("Cannot subtract Matrices with different dimensions");
            Matrix result = new Matrix(m1);
            Arithmetic.ElementOperation(result, m2, (a, b) => a - b);
            return result;
        }

        /// <summary>
        /// Subtract a number from each element in a <c>Matrix</c>.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to subtract from the number.</param>
        /// <param name="scalar">The number to subtract from each element in a <c>Matrix</c>.</param>
        /// <returns>The result of subracting a given number from each element.</returns>
        /// <exception cref="NullReferenceException">Thrown when <c>Matrix</c> is null.</exception>
        public static Matrix Subtract(Matrix m, double scalar)
        {
            Matrix result = new Matrix(m);
            Arithmetic.ElementOperation(result, scalar, (a, b) => a - b);
            return result;
        }

        /// <summary>
        /// Subtract a each element in a <c>Matrix</c> from a number.
        /// </summary>
        /// <param name="scalar">The number to subtract each element in a <c>Matrix</c> from.</param>
        /// <param name="m">The <c>Matrix</c> to subtract from the number.</param>
        /// <returns>The result of subracting each element from a given number.</returns>
        /// <exception cref="NullReferenceException">Thrown when <c>Matrix</c> is null.</exception>
        public static Matrix Subtract(double scalar, Matrix m)
        {
            Matrix result = new Matrix(m);
            Arithmetic.ElementOperation(result, scalar, (a, b) => b - a);
            return result;
        }

        /// <summary>
        /// Multiply two matrices together.
        /// </summary>
        /// <param name="m1">An n*m dimension <c>Matrix</c>.</param>
        /// <param name="m2">An m*p dimension <c>Matrix</c>.</param>
        /// <returns>An n*p Matrix that is the product of m1 and m2.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when the number of rows in the
        /// first <c>Matrix</c> don't match the number of columns in the second <c>Matrix</c>.</exception>
        /// <exception cref="NullReferenceException">Thrown when either <c>Matrix</c> is null.</exception>
        public static Matrix Multiply(Matrix m1, Matrix m2)
        {
            if (m1.Columns != m2.Rows) throw new InvalidMatrixDimensionsException("Matrices can only be multiplied if the number of columns in the first Matrix match the number of rows in the second Matrix");
            return Arithmetic.Multiply(m1, m2);
        }

        /// <summary>
        /// Scalar multiplication of a <c>Matrix</c>.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to apply multiplication to.</param>
        /// <param name="scalar">The scalar value to multiply each element of the <c>Matrix</c> by.</param>
        /// <returns>A <c>Matrix</c> representing the scalar multiplication of m * scalar.</returns>
        /// <exception cref="NullReferenceException">Thrown when <c>Matrix</c> is null.</exception>
        public static Matrix Multiply(Matrix m, double scalar)
        {
            Matrix result = new Matrix(m);
            Arithmetic.ElementOperation(result, scalar, (a, b) => a * b);
            return result;
        }

        /// <summary>
        /// Scalar division of a <c>Matrix</c>.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to apply division to.</param>
        /// <param name="scalar">The scalar value to divide each element of the <c>Matrix</c> by.</param>
        /// <returns>A <c>Matrix</c> representing the scalar division of m / scalar.</returns>
        /// <exception cref="NullReferenceException">Thrown when <c>Matrix</c> is null.</exception>
        public static Matrix Divide(Matrix m, double scalar)
        {
            Matrix result = new Matrix(m);
            Arithmetic.ElementOperation(result, scalar, (a, b) => a / b);
            return result;
        }
        #endregion

        #region Rows/Columns
        /// <summary>
        /// Swap two rows in a given <c>Matrix</c>.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to swap rows in.</param>
        /// <param name="row1">The first row to swap.</param>
        /// <param name="row2">The second row to swap.</param>
        /// <returns>A new <c>Matrix</c> with the rows swapped.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when called with non-existent rows.</exception>
        public static Matrix SwapRows(Matrix m, int row1, int row2)
        {
            Matrix result = new Matrix(m);
            RowColumnOperations.SwapRows(result, row1, row2);
            return result;
        }

        /// <summary>
        /// Swap two columns in a given <c>Matrix</c>.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to swap columns in.</param>
        /// <param name="column1">The first column to swap.</param>
        /// <param name="column2">The second column to swap.</param>
        /// <returns>A new <c>Matrix</c> with the columns swapped.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when called with non-existent columns.</exception>
        public static Matrix SwapColumns(Matrix m, int column1, int column2)
        {
            Matrix result = new Matrix(m);
            RowColumnOperations.SwapColumns(result, column1, column2);
            return result;
        }

        /// <summary>
        /// Inserts a number of additional columns to a <c>Matrix</c>, populating them with a given value.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to add columns to.</param>
        /// <param name="column">The column index to insert the new columns at.</param>
        /// <param name="count">The number of columns to insert.</param>
        /// <param name="value">The default value to add to the newly added columns.</param>
        /// <returns>A new Matrix with the additional columns added.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when attempting to add columns outside
        /// the range of valid column indices (i.e. 0 to <c>m.Columns</c>).</exception>
        public static Matrix AddColumns(Matrix m, int column, int count, double value)
        {
            return RowColumnOperations.AddColumns(m, column, count, value);
        }

        /// <summary>
        /// Inserts a number of additional rows to a <c>Matrix</c>, populating them with a given value.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to add rows to.</param>
        /// <param name="row">The row index to insert the new columns at.</param>
        /// <param name="count">The number of rows to insert.</param>
        /// <param name="value">The default value to add to the newly added rows.</param>
        /// <returns>A new <c>Matrix</c> with the additional rows added.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when attempting to add rows outside
        /// the range of valid row indices (i.e. 0 to <c>m.Rows</c>).</exception>
        public static Matrix AddRows(Matrix m, int row, int count, double value)
        {
            return RowColumnOperations.AddRows(m, row, count, value);
        }

        /// <summary>
        /// Removes a number of columns from a <c>Matrix</c>.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to remove columns from.</param>
        /// <param name="column">The column index to start removal from.</param>
        /// <param name="count">The number of columns to remove.</param>
        /// <returns>A new <c>Matrix</c> with the specified columns removed.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when attempting to remove columns outside
        /// the range of valid column indices (i.e. 0 to <c>m.Columns</c>).</exception>
        public static Matrix RemoveColumns(Matrix m, int column, int count)
        {
            return RowColumnOperations.RemoveColumns(m, column, count);
        }

        /// <summary>
        /// Removes a number of rows from a <c>Matrix</c>.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to remove rows from.</param>
        /// <param name="row">The row index to start removal from.</param>
        /// <param name="count">The number of rows to remove.</param>
        /// <returns>A new <c>Matrix</c> with the specified rows removed.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when attempting to remove rows outside
        /// the range of valid row indices (i.e. 0 to m.Rows).</exception>
        public static Matrix RemoveRows(Matrix m, int row, int count)
        {
            return RowColumnOperations.RemoveRows(m, row, count);
        }

        #endregion

        #region Transpose
        /// <summary>
        /// Get the transposed version of the given <c>Matrix</c> (swap rows and columns).
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to transpose.</param>
        /// <returns>A new <c>Matrix</c> that is the transpose of the original.</returns>
        public static Matrix Transpose(Matrix m)
        {
            return TransposeOperations.Transpose(m);
        }

        /// <summary>
        /// Multiply one <c>Matrix</c> by the transpose of a second Matrix.
        /// </summary>
        /// <param name="m1">The <c>Matrix</c> to multiply.</param>
        /// <param name="m2">The <c>Matrix</c> to multiply the first <c>Matrix</c> by its transpose.</param>
        /// <returns>A new <c>Matrix</c> with the result of multiplying <c>Matrix</c> m1 by the transpose
        /// of <c>Matrix</c> m2.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when the two <c>Matrix</c>
        /// instances have incompatible dimensions for multiplication.</exception>
        public static Matrix MultiplyByTranspose(Matrix m1, Matrix m2)
        {
            if (m1.Columns != m2.Columns)
            {
                throw new InvalidMatrixDimensionsException("Cannot multiply by transpose because the second Matrix has a different number of columns to the first.");
            }
            return TransposeOperations.MultiplyByTranspose(m1, m2);
        }

        /// <summary>
        /// Multiply a <c>Matrix</c> by its transpose.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to multiply by its transpose.</param>
        /// <returns>The result of multiplying m1 with its transpose.</returns>
        public static Matrix MultiplyByOwnTranspose(Matrix m)
        {
            return TransposeOperations.MultiplyByTranspose(m);
        }

        /// <summary>
        /// Multiply the transpose of one <c>Matrix</c> by the a second Matrix.
        /// </summary>
        /// <param name="m1">The <c>Matrix</c> to transpose and multiply.</param>
        /// <param name="m2">The <c>Matrix</c> to multiply the transpose of the first <c>Matrix</c> by.</param>
        /// <returns>A new <c>Matrix</c> with the result of multiplying the transpose of <c>Matrix</c> m1 by
        /// <c>Matrix</c> m2.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when the two <c>Matrix</c>
        /// instances have incompatible dimensions for multiplication.</exception>
        public static Matrix MultiplyTransposeBy(Matrix m1, Matrix m2)
        {
            if (m1.Rows != m2.Rows)
            {
                throw new InvalidMatrixDimensionsException("Cannot multiply transpose by this Matrix because the second Matrix has a different number of rows to the first.");
            }

            return TransposeOperations.MultiplyTransposeBy(m1, m2);
        }

        /// <summary>
        /// Multiply the Transpose of a <c>Matrix</c> by the original <c>Matrix</c>.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to transpose, and multiply by itself.</param>
        /// <returns>The result of multiplying the Transpose of the given <c>Matrix</c>
        /// by itself.</returns>
        public static Matrix MultiplyOwnTransposeBy(Matrix m)
        {
            return TransposeOperations.MultiplyTransposeBy(m);
        }
        #endregion

        #region Inverse
        /// <summary>
        /// Calculate the inverse of the given <c>Matrix</c>.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to calculate the inverse of.</param>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when the supplied <c>Matrix</c> is
        /// not square.</exception>
        /// <exception cref="NonInvertibleMatrixException">Thrown when the supplied <c>Matrix</c> is not
        /// invertible.</exception>
        /// <returns>A new <c>Matrix</c> that is the inverse of the supplied <c>Matrix</c>.</returns>
        public static Matrix Inverse(Matrix m)
        {
            return InverseOperations.Inverse(m);
        }
        #endregion

        #region Operations
        /// <summary>
        /// Joins two <c>Matrix</c> instances together.
        /// </summary>
        /// <param name="m1">The first <c>Matrix</c> to join.</param>
        /// <param name="m2">The second <c>Matrix</c> to join to the first.</param>
        /// <param name="dimension">Determines whether the second <c>Matrix</c> should be
        /// joined to this one by adding rows, columns, or automatically determine
        /// the most suitable dimension.</param>
        /// <returns>A <c>Matrix</c> that consists of both input <c>Matrix</c> instances joined
        /// into one single <c>Matrix</c>.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when the
        /// two <c>Matrix</c> instances don't share the correct number of elements along
        /// the specified dimension.</exception>
        public static Matrix Join(Matrix m1, Matrix m2, MatrixDimension dimension)
        {
            return Operations.Join(m1, m2, dimension);
        }

        /// <summary>
        /// Run a given operation on all elements in a particular dimension to reduce that dimension
        /// to a single row or column.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to operate on.</param>
        /// <param name="dimension">Indicate whether to operate on rows or columns.</param>
        /// <param name="op">The delegate method to operate with.</param>
        /// <returns>A <c>Matrix</c> populated with the results of performing the given operation.</returns>
        /// <remarks>If the current <c>Matrix</c> is a row or column vector, then a 1*1 <c>Matrix</c>
        /// will be returned, regardless of which dimension is chosen. If the dimension is
        /// set to 'Auto', then the first non-singleton dimension is chosen. If no singleton
        /// dimension exists, then columns are used as the default.</remarks>
        public static Matrix ReduceDimension(Matrix m, MatrixDimension dimension, Func<double, double, double> op)
        {
            return Operations.ReduceDimension(m, dimension, op);
        }

        /// <summary>
        /// Run a set of operations on all elements in a particular dimension to reduce that dimension
        /// to a single row, and then perform an aggregate operation to produce a statistical result.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to operate on.</param>
        /// <param name="dimension">Indicate whether to operate on rows or columns.</param>
        /// <param name="op">The delegate method to operate with.</param>
        /// <remarks>If the current <c>Matrix</c> is a row or column vector, then a 1*1 <c>Matrix</c>
        /// will be returned, regardless of which dimension is chosen. If the dimension is
        /// set to 'Auto', then the first non-singleton dimension is chosen. If no singleton
        /// dimension exists, then columns are used as the default.</remarks>
        public static Matrix StatisticalReduce(Matrix m, MatrixDimension dimension, Func<Matrix, double> op)
        {
            return Operations.StatisticalReduce(m, dimension, op);
        }
        #endregion

        #region Extraction
        /// <summary>
        /// Extract a number of rows from this <c>Matrix</c>, discarding the rest.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to extract rows from.</param>
        /// <param name="row">The row index to start extraction from.</param>
        /// <param name="count">The number of rows to extract.</param>
        /// <returns>A new <c>Matrix</c> containing the extracted rows.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when attempting to remove rows outside
        /// the range of valid row indices (i.e. 0 to <c>m.Rows</c>).</exception>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when attempting to extract zero
        /// rows.</exception>
        public static Matrix ExtractRows(Matrix m, int row, int count)
        {
            return RowColumnOperations.ExtractRows(m, row, count);
        }

        /// <summary>
        /// Extract a number of columns from a <c>Matrix</c>, discarding the rest.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to extract columns from.</param>
        /// <param name="column">The column index to start extraction from.</param>
        /// <param name="count">The number of columns to extract.</param>
        /// <returns>A new <c>Matrix</c> containing the extracted columns.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when attempting to extract columns outside
        /// the range of valid column indices (i.e. 0 to <c>m.Columns</c>).</exception>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when attempting to extract zero
        /// columns.</exception>
        public static Matrix ExtractColumns(Matrix m, int column, int count)
        {
            return RowColumnOperations.ExtractColumns(m, column, count);
        }

        /// <summary>
        /// Extract a new <c>Matrix</c> from an existing one.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to extract data from.</param>
        /// <param name="startingIndex">The zero-based starting index of the <c>Matrix</c> to start
        /// extracting data from.</param>
        /// <param name="rows">The number of rows to extract.</param>
        /// <param name="columns">The number of columns to extract.</param>
        /// <returns>The extracted <c>Matrix</c>.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when there are not
        /// enough elements to fill the new <c>Matrix</c>.</exception>
        public static Matrix Extract(Matrix m, int startingIndex, int rows, int columns)
        {
            return Operations.Extract(m, startingIndex, rows, columns);
        }

        /// <summary>
        /// Extract a new <c>Matrix</c> from an existing one.
        /// </summary>
        /// <param name="m">The <c>Matrix</c> to extract data from.</param>
        /// <param name="startingRow">The zero-based row of the given <c>Matrix</c> to start
        /// extracting data from.</param>
        /// <param name="startingColumn">The zero-based column of the given <c>Matrix</c> to start
        /// extracting data from.</param>
        /// <param name="rows">The number of rows to extract.</param>
        /// <param name="columns">The number of columns to extract.</param>
        /// <returns>The extracted <c>Matrix</c>.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when there are not
        /// enough elements to fill the new <c>Matrix</c>.</exception>
        public static Matrix Extract(Matrix m, int startingRow, int startingColumn, int rows, int columns)
        {
            int startingIndex = m.GetIndex(startingRow, startingColumn);
            return Extract(m, startingIndex, rows, columns);
        }
        #endregion
        #endregion
        #endregion
    }
}

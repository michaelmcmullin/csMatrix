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
        protected double[] data;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor to create a new Matrix while specifying the number of rows and columns.
        /// </summary>
        /// <param name="rows">The number of rows to initialise the Matrix with.</param>
        /// <param name="columns">The number of columns to initialise the Matrix with.</param>
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
        public Matrix(int dimensions) : this(dimensions, dimensions)
        {
        }

        /// <summary>
        /// Constructor to create a new square Matrix filled with a default value.
        /// </summary>
        /// <param name="dimensions">The number of rows and columns to initialise the
        /// Matrix with. There will be an equal number of rows and columns.</param>
        /// <param name="value">The value to fill the Matrix with.</param>
        public Matrix(int dimensions, double value) : this(dimensions, dimensions, value)
        {
        }

        /// <summary>
        /// Constructor to create a new Matrix based on an existing two-dimensional array.
        /// </summary>
        /// <param name="array">The array to specify values for a new Matrix.</param>
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
        #endregion

        #region Methods
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
        public bool hasSameDimensions(Matrix m)
        {
            return (this.Rows == m.Rows && this.Columns == m.Columns);
        }

        /// <summary>
        /// Swap two rows in this Matrix.
        /// </summary>
        /// <param name="row1">The first row to swap.</param>
        /// <param name="row2">The second row to swap.</param>
        public void SwapRows(int row1, int row2)
        {
        }

        /// <summary>
        /// Swap two columns in this Matrix.
        /// </summary>
        /// <param name="column1">The first column to swap.</param>
        /// <param name="column2">The second column to swap.</param>
        public void SwapColumns(int column1, int column2)
        {
        }
        #endregion
    }
}

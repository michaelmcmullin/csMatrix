using System;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixTests
    {
        #region Constructors
        [Fact]
        public void MatrixConstructorIntInt()
        {
            int rows = 3, columns = 5;
            Matrix m = new Matrix(rows, columns);
            Assert.Equal(rows, m.Rows);
            Assert.Equal(columns, m.Columns);
        }

        [Fact]
        public void MatrixConstructorIntIntDouble()
        {
            int rows = 3, columns = 5;
            double value = 3.14;
            Matrix m = new Matrix(rows, columns, value);
            Assert.Equal(rows, m.Rows);
            Assert.Equal(columns, m.Columns);
            Assert.Equal(value, m[0, 0]);
            Assert.Equal(value, m[rows - 1, columns - 1]);
            Assert.Equal(value, m[1, 1]);
        }

        [Fact]
        public void MatrixConstructorInt()
        {
            int rows = 3;
            Matrix m = new Matrix(rows, rows);
            Assert.Equal(rows, m.Rows);
            Assert.Equal(rows, m.Columns);
        }

        [Fact]
        public void MatrixConstructorIntDouble()
        {
            int rows = 3;
            double value = 3.14;
            Matrix m = new Matrix(rows, rows, value);
            Assert.Equal(rows, m.Rows);
            Assert.Equal(rows, m.Columns);
            Assert.Equal(value, m[0, 0]);
            Assert.Equal(value, m[rows - 1, rows - 1]);
            Assert.Equal(value, m[1, 1]);
        }

        [Fact]
        public void MatrixConstructorDoubleArray()
        {
            double[,] arr = { { 1, 2, 3 }, { 4, 5, 6 } };
            int rows = arr.GetLength(0);
            int columns = arr.GetLength(1);
            Matrix m = new Matrix(arr);
            Assert.Equal(rows, m.Rows);
            Assert.Equal(columns, m.Columns);
            Assert.Equal(arr[0,0], m[0, 0]);
            Assert.Equal(arr[rows - 1, columns - 1], m[rows - 1, columns - 1]);
        }

        [Fact]
        public void MatrixConstructorMatrix()
        {
            int rows = 3, columns = 2;
            double value = 3.14;
            Matrix m1 = new Matrix(rows, columns, value);
            Matrix m2 = new Matrix(m1);
            Assert.Equal(rows, m2.Rows);
            Assert.Equal(columns, m2.Columns);
            Assert.Equal(value, m2[0, 0]);
            Assert.Equal(value, m2[rows - 1, columns - 1]);
            Assert.Equal(value, m2[1, 1]);
        }

        [Theory]
        [InlineData(-3, 5)]
        [InlineData(3, -5)]
        [InlineData(-3, -5)]
        public void MatrixConstructorNegativeIntInt(int rows, int columns)
        {
            Assert.Throws<ArgumentException>(() => new Matrix(rows, columns));
        }
        #endregion

        #region Properties
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(5, 5)]
        public void MatrixIsSquare(int rows, int columns)
        {
            Matrix m = new Matrix(rows, columns);
            Assert.True(m.IsSquare);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(5, 2)]
        public void MatrixIsNotSquare(int rows, int columns)
        {
            Matrix m = new Matrix(rows, columns);
            Assert.False(m.IsSquare);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(5, 2)]
        public void MatrixDimensions(int rows, int columns)
        {
            Matrix m = new Matrix(rows, columns);
            Assert.Equal(rows, m.Dimensions[0]);
            Assert.Equal(columns, m.Dimensions[1]);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(5, 2)]
        public void MatrixRowsColumns(int rows, int columns)
        {
            Matrix m = new Matrix(rows, columns);
            Assert.Equal(rows, m.Rows);
            Assert.Equal(columns, m.Columns);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(5, 2)]
        public void MatrixSize(int rows, int columns)
        {
            Matrix m = new Matrix(rows, columns);
            int size = rows * columns;
            Assert.Equal(size, m.Size);
        }
        #endregion

        #region Methods
        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(5, 2)]
        public void MatrixGetEnumerator(int rows, int columns)
        {
            Matrix m = new Matrix(rows, columns);
            int size = 0;
            foreach(double element in m)
            {
                size++;
            }
            Assert.Equal(m.Size, size);
        }

        [Fact]
        public void MatrixSwapRowsFirstSecond()
        {
            Matrix m = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 }, { 7.0, 8.0, 9.0 } });
            Matrix expected = new Matrix(new double[,] { { 4.0, 5.0, 6.0 }, { 1.0, 2.0, 3.0 }, { 7.0, 8.0, 9.0 } });
            m.SwapRows(0, 1);
            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixSwapRowsLastSecondLast()
        {
            Matrix m = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 }, { 7.0, 8.0, 9.0 } });
            Matrix expected = new Matrix(new double[,] { { 4.0, 5.0, 6.0 }, { 1.0, 2.0, 3.0 }, { 7.0, 8.0, 9.0 } });
            m.SwapRows(0, 1);
            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixSwapRowsOutOfRange()
        {
            Matrix m = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 }, { 7.0, 8.0, 9.0 } });
            Assert.Throws<IndexOutOfRangeException>(() => m.SwapRows(m.Rows, 0));
        }

        [Fact]
        public void MatrixSwapColumnsFirstSecond()
        {
            Matrix m = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 }, { 7.0, 8.0, 9.0 } });
            Matrix expected = new Matrix(new double[,] { { 2.0, 1.0, 3.0 }, { 5.0, 4.0, 6.0 }, { 8.0, 7.0, 9.0 } });
            m.SwapColumns(0, 1);
            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixSwapColumnsLastSecondLast()
        {
            Matrix m = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 }, { 7.0, 8.0, 9.0 } });
            Matrix expected = new Matrix(new double[,] { { 1.0, 3.0, 2.0 }, { 4.0, 6.0, 5.0 }, { 7.0, 9.0, 8.0 } });
            m.SwapColumns(m.Columns - 1, m.Columns - 2);
            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixSwapColumnsOutOfRange()
        {
            Matrix m = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 }, { 7.0, 8.0, 9.0 } });
            Assert.Throws<IndexOutOfRangeException>(() => m.SwapColumns(m.Columns, 0));
        }
        #endregion
    }
}

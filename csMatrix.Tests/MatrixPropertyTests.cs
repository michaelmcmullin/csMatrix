using System;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixPropertyTests
    {
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
        public void MatrixDimensionsTransposed(int rows, int columns)
        {
            Matrix m = new Matrix(rows, columns);
            m.Transpose(true);
            Assert.Equal(columns, m.Dimensions[0]);
            Assert.Equal(rows, m.Dimensions[1]);
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
        public void MatrixRowsColumnsTransposed(int rows, int columns)
        {
            Matrix m = new Matrix(rows, columns);
            m.Transpose(true);
            Assert.Equal(columns, m.Rows);
            Assert.Equal(rows, m.Columns);
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
    }
}

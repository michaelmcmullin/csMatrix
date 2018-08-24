using System;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixConstructorsTests
    {
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
            Assert.Equal(arr[0, 0], m[0, 0]);
            Assert.Equal(arr[rows - 1, columns - 1], m[rows - 1, columns - 1]);
        }

        [Fact]
        public void MatrixConstructorDoubleArrayNull()
        {
            double[,] arr = null;
            Assert.Throws<NullReferenceException>(() => new Matrix(arr));
        }

        [Fact]
        public void MatrixConstructorSingleArray()
        {
            double[] arr = { 1, 2, 3, 4, 5, 6 };
            int rows = 3;
            int columns = 2;
            Matrix m = new Matrix(rows, columns, arr);
            Matrix expected = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
        }

        [Fact]
        public void MatrixConstructorSingleArrayNull()
        {
            double[] arr = null;
            Assert.Throws<NullReferenceException>(() => new Matrix(3, 4, arr));
        }

        [Fact]
        public void MatrixConstructorSingleArrayTooFewDimensions()
        {
            double[] arr = { 1, 2, 3, 4, 5, 6 };
            Assert.Throws<InvalidMatrixDimensionsException>(() => new Matrix(1, 1, arr));
        }

        [Fact]
        public void MatrixConstructorSingleArrayTooManyDimensions()
        {
            double[] arr = { 1, 2, 3, 4, 5, 6 };
            Assert.Throws<InvalidMatrixDimensionsException>(() => new Matrix(10, 5, arr));
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

        [Fact]
        public void MatrixConstructorMatrixTransosed()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            m1.Transpose(true);
            Matrix m2 = new Matrix(m1);
            Matrix expected = Setup.GetTestMatrix1Transposed();

            Assert.True(m1.IsTransposed);
            Assert.False(m2.IsTransposed);
            Assert.Equal(expected.Rows, m2.Rows);
            Assert.Equal(expected.Columns, m2.Columns);
            Assert.True(expected == m2);
        }

        [Fact]
        public void MatrixConstructorMatrixNull()
        {
            Matrix m1 = null;
            Assert.Throws<NullReferenceException>(() => new Matrix(m1));
        }

        [Theory]
        [InlineData(-3, 5)]
        [InlineData(3, -5)]
        [InlineData(-3, -5)]
        public void MatrixConstructorNegativeIntInt(int rows, int columns)
        {
            Assert.Throws<ArgumentException>(() => new Matrix(rows, columns));
        }
    }
}

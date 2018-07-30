using System;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixRowColumnOperationTests
    {
        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(5, 2)]
        public void MatrixGetEnumerator(int rows, int columns)
        {
            Matrix m = new Matrix(rows, columns);
            int size = 0;
            foreach (double element in m)
            {
                size++;
            }
            Assert.Equal(m.Size, size);
        }

        [Fact]
        public void MatrixSwapRowsFirstSecond()
        {
            Matrix m1 = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 }, { 7.0, 8.0, 9.0 } });
            Matrix m2 = new Matrix(m1);
            Matrix expected = new Matrix(new double[,] { { 4.0, 5.0, 6.0 }, { 1.0, 2.0, 3.0 }, { 7.0, 8.0, 9.0 } });

            m1.SwapRows(0, 1);
            Matrix test = Matrix.SwapRows(m2, 0, 1);

            Assert.Equal(expected, m1);
            Assert.Equal(expected, test);
            Assert.NotEqual(m2, test); // Ensures m2 wasn't mutated.
        }

        [Fact]
        public void MatrixSwapRowsLastSecondLast()
        {
            Matrix m1 = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 }, { 7.0, 8.0, 9.0 } });
            Matrix m2 = new Matrix(m1);
            Matrix expected = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 7.0, 8.0, 9.0 }, { 4.0, 5.0, 6.0 } });

            m1.SwapRows(1, 2);
            Matrix test = Matrix.SwapRows(m2, 1, 2);

            Assert.Equal(expected, m1);
            Assert.Equal(expected, test);
            Assert.NotEqual(m2, test); // Ensures m2 wasn't mutated.
        }

        [Fact]
        public void MatrixSwapRowsOutOfRange()
        {
            Matrix m = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 }, { 7.0, 8.0, 9.0 } });
            Assert.Throws<IndexOutOfRangeException>(() => m.SwapRows(m.Rows, 0));
            Assert.Throws<IndexOutOfRangeException>(() => Matrix.SwapRows(m, m.Rows, 0));
        }

        [Fact]
        public void MatrixSwapColumnsFirstSecond()
        {
            Matrix m1 = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 }, { 7.0, 8.0, 9.0 } });
            Matrix m2 = new Matrix(m1);
            Matrix expected = new Matrix(new double[,] { { 2.0, 1.0, 3.0 }, { 5.0, 4.0, 6.0 }, { 8.0, 7.0, 9.0 } });

            m1.SwapColumns(0, 1);
            Matrix test = Matrix.SwapColumns(m2, 0, 1);

            Assert.Equal(expected, m1);
            Assert.Equal(expected, test);
            Assert.NotEqual(m2, test); // Ensures m2 wasn't mutated.
        }

        [Fact]
        public void MatrixSwapColumnsLastSecondLast()
        {
            Matrix m1 = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 }, { 7.0, 8.0, 9.0 } });
            Matrix m2 = new Matrix(m1);
            Matrix expected = new Matrix(new double[,] { { 1.0, 3.0, 2.0 }, { 4.0, 6.0, 5.0 }, { 7.0, 9.0, 8.0 } });

            m1.SwapColumns(m1.Columns - 1, m1.Columns - 2);
            Matrix test = Matrix.SwapColumns(m2, m2.Columns - 1, m2.Columns - 2);

            Assert.Equal(expected, m1);
            Assert.Equal(expected, test);
            Assert.NotEqual(m2, test); // Ensures m2 wasn't mutated.
        }

        [Fact]
        public void MatrixSwapColumnsOutOfRange()
        {
            Matrix m = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 }, { 7.0, 8.0, 9.0 } });
            Assert.Throws<IndexOutOfRangeException>(() => m.SwapColumns(m.Columns, 0));
            Assert.Throws<IndexOutOfRangeException>(() => Matrix.SwapColumns(m, m.Columns, 0));
        }
    }
}

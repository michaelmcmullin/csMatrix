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

        [Fact]
        public void MatrixAddRowsOneAtStart()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.AddRows(Setup.GetTestMatrix1(), 0, 1, 2.0);
            m1.AddRows(0, 1, 2.0);

            Matrix expected = new Matrix(new double[,] { { 2.0, 2.0, 2.0 }, { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 } });
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Fact]
        public void MatrixAddRowsOneAtEnd()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.AddRows(Setup.GetTestMatrix1(), m1.Rows, 1, 2.0);
            m1.AddRows(m1.Rows, 1, 2.0);

            Matrix expected = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 }, { 2.0, 2.0, 2.0 } });
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Fact]
        public void MatrixAddRowsTwoInCentre()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.AddRows(Setup.GetTestMatrix1(), 1, 2, 2.0);
            m1.AddRows(1, 2, 2.0);

            Matrix expected = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 2.0, 2.0, 2.0 }, { 2.0, 2.0, 2.0 }, { 4.0, 5.0, 6.0 } });
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Fact]
        public void MatrixAddRowsZeroRows()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.AddRows(Setup.GetTestMatrix1(), 1, 0, 2.0);
            m1.AddRows(1, 0, 2.0);

            Matrix expected = Setup.GetTestMatrix1();
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(-1)]
        public void MatrixAddRowsOutOfRange(int startRow)
        {
            Matrix m = Setup.GetTestMatrix1();
            Assert.Throws<IndexOutOfRangeException>(() => m.AddRows(startRow, 1, 0));
            Assert.Throws<IndexOutOfRangeException>(() => Matrix.AddRows(Setup.GetTestMatrix1(), startRow, 1, 0));
        }

        [Fact]
        public void MatrixAddColumnsOneAtStart()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.AddColumns(Setup.GetTestMatrix1(), 0, 1, 2.0);
            m1.AddColumns(0, 1, 2.0);

            Matrix expected = new Matrix(new double[,] { { 2.0, 1.0, 2.0, 3.0 }, { 2.0, 4.0, 5.0, 6.0 } });
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Fact]
        public void MatrixAddColumnsOneAtEnd()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.AddColumns(Setup.GetTestMatrix1(), m1.Rows, 1, 2.0);
            m1.AddColumns(m1.Rows, 1, 2.0);

            Matrix expected = new Matrix(new double[,] { { 1.0, 2.0, 3.0, 2.0 }, { 4.0, 5.0, 6.0, 2.0 } });
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Fact]
        public void MatrixAddColumnsTwoInCentre()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.AddColumns(Setup.GetTestMatrix1(), 1, 2, 2.0);
            m1.AddColumns(1, 2, 2.0);

            Matrix expected = new Matrix(new double[,] { { 1.0, 2.0, 2.0, 2.0, 3.0 }, { 4.0, 2.0, 2.0, 5.0, 6.0 } });
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Fact]
        public void MatrixAddColumnsZeroColumns()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.AddColumns(Setup.GetTestMatrix1(), 1, 0, 2.0);
            m1.AddColumns(1, 0, 2.0);

            Matrix expected = Setup.GetTestMatrix1();
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(-1)]
        public void MatrixAddColumnsOutOfRange(int startColumn)
        {
            Matrix m = Setup.GetTestMatrix1();
            Assert.Throws<IndexOutOfRangeException>(() => m.AddColumns(startColumn, 1, 0));
            Assert.Throws<IndexOutOfRangeException>(() => Matrix.AddColumns(Setup.GetTestMatrix1(), startColumn, 1, 0));
        }

        [Fact]
        public void MatrixExtractRowsFirstRow()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.ExtractRows(Setup.GetTestMatrix1(), 0, 1);
            m1.ExtractRows(0, 1);

            Matrix expected = new Matrix(new double[,] { { 1.0, 2.0, 3.0 } });
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Fact]
        public void MatrixExtractRowsLastRow()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.ExtractRows(Setup.GetTestMatrix1(), m1.Rows - 1, 1);
            m1.ExtractRows(m1.Rows - 1, 1);

            Matrix expected = new Matrix(new double[,] { { 4.0, 5.0, 6.0 } });
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Fact]
        public void MatrixExtractRowsTwoRows()
        {
            Matrix m1 = Setup.GetTestMatrix3();
            Matrix m2 = Matrix.ExtractRows(Setup.GetTestMatrix3(), 1, 2);
            m1.ExtractRows(1, 2);

            Matrix expected = new Matrix(new double[,] { { 3.0, 4.0 }, { 5.0, 6.0 } });
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Theory]
        [InlineData(5, 1)]
        [InlineData(0, 5)]
        [InlineData(-1, 1)]
        public void MatrixExtractRowsOutOfRange(int startRow, int count)
        {
            Matrix m = Setup.GetTestMatrix1();
            Assert.Throws<IndexOutOfRangeException>(() => m.ExtractRows(startRow, count));
            Assert.Throws<IndexOutOfRangeException>(() => Matrix.ExtractRows(Setup.GetTestMatrix1(), startRow, count));
        }

        [Fact]
        public void MatrixExtractRowsZeroRows()
        {
            Matrix m = Setup.GetTestMatrix1();
            Assert.Throws<InvalidMatrixDimensionsException>(() => m.ExtractRows(0, 0));
            Assert.Throws<InvalidMatrixDimensionsException>(() => Matrix.ExtractRows(Setup.GetTestMatrix1(), 0, 0));
        }

        [Fact]
        public void MatrixExtractColumnsFirstColumn()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.ExtractColumns(Setup.GetTestMatrix1(), 0, 1);
            m1.ExtractColumns(0, 1);

            Matrix expected = new Matrix(new double[,] { { 1.0 }, { 4.0 } });
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Fact]
        public void MatrixExtractColumnsLastColumn()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.ExtractColumns(Setup.GetTestMatrix1(), m1.Columns - 1, 1);
            m1.ExtractColumns(m1.Columns - 1, 1);

            Matrix expected = new Matrix(new double[,] { { 3.0 }, { 6.0 } });
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Fact]
        public void MatrixExtractColumnsTwoColumns()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.ExtractColumns(Setup.GetTestMatrix1(), 1, 2);
            m1.ExtractColumns(1, 2);

            Matrix expected = new Matrix(new double[,] { { 2.0, 3.0 }, { 5.0, 6.0 } });
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Theory]
        [InlineData(5, 1)]
        [InlineData(0, 5)]
        [InlineData(-1, 1)]
        public void MatrixExtractColumnsOutOfRange(int startColumn, int count)
        {
            Matrix m = Setup.GetTestMatrix1();
            Assert.Throws<IndexOutOfRangeException>(() => m.ExtractColumns(startColumn, count));
            Assert.Throws<IndexOutOfRangeException>(() => Matrix.ExtractColumns(Setup.GetTestMatrix1(), startColumn, count));
        }

        [Fact]
        public void MatrixExtractColumnsZeroColumns()
        {
            Matrix m = Setup.GetTestMatrix1();
            Assert.Throws<InvalidMatrixDimensionsException>(() => m.ExtractColumns(0, 0));
            Assert.Throws<InvalidMatrixDimensionsException>(() => Matrix.ExtractColumns(Setup.GetTestMatrix1(), 0, 0));
        }

        [Fact]
        public void MatrixRemoveRowsOneAtStart()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.RemoveRows(Setup.GetTestMatrix1(), 0, 1);
            m1.RemoveRows(0, 1);

            Matrix expected = new Matrix(new double[,] { { 4.0, 5.0, 6.0 } });
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Fact]
        public void MatrixRemoveRowsOneAtEnd()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.RemoveRows(Setup.GetTestMatrix1(), m1.Rows - 1, 1);
            m1.RemoveRows(m1.Rows - 1, 1);

            Matrix expected = new Matrix(new double[,] { { 1.0, 2.0, 3.0 } });
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Fact]
        public void MatrixRemoveRowsTwoInCentre()
        {
            Matrix m1 = Setup.GetTestMatrix3();
            Matrix m2 = Matrix.RemoveRows(Setup.GetTestMatrix3(), 1, 2);
            m1.RemoveRows(1, 2);

            Matrix expected = new Matrix(new double[,] { { 1.0, 2.0 } });
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Fact]
        public void MatrixRemoveRowsZeroRows()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.RemoveRows(Setup.GetTestMatrix1(), 1, 0);
            m1.RemoveRows(1, 0);

            Matrix expected = Setup.GetTestMatrix1();
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(-1)]
        public void MatrixRemoveRowsOutOfRange(int startRow)
        {
            Matrix m = Setup.GetTestMatrix1();
            Assert.Throws<IndexOutOfRangeException>(() => m.RemoveRows(startRow, 1));
            Assert.Throws<IndexOutOfRangeException>(() => Matrix.RemoveRows(Setup.GetTestMatrix1(), startRow, 1));
        }

        [Fact]
        public void MatrixRemoveColumnsOneAtStart()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.RemoveColumns(Setup.GetTestMatrix1(), 0, 1);
            m1.RemoveColumns(0, 1);

            Matrix expected = new Matrix(new double[,] { { 2.0, 3.0 }, { 5.0, 6.0 } });
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Fact]
        public void MatrixRemoveColumnsOneAtEnd()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.RemoveColumns(Setup.GetTestMatrix1(), m1.Columns - 1, 1);
            m1.RemoveColumns(m1.Columns - 1, 1);

            Matrix expected = new Matrix(new double[,] { { 1.0, 2.0 }, { 4.0, 5.0 } });
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Fact]
        public void MatrixRemoveColumnsTwoInCentre()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.RemoveColumns(Setup.GetTestMatrix1(), 1, 2);
            m1.RemoveColumns(1, 2);

            Matrix expected = new Matrix(new double[,] { { 1.0 }, { 4.0 } });
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Fact]
        public void MatrixRemoveColumnsZeroColumns()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.RemoveColumns(Setup.GetTestMatrix1(), 1, 0);
            m1.RemoveColumns(1, 0);

            Matrix expected = Setup.GetTestMatrix1();
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(-1)]
        public void MatrixRemoveColumnsOutOfRange(int startColumn)
        {
            Matrix m = Setup.GetTestMatrix1();
            Assert.Throws<IndexOutOfRangeException>(() => m.RemoveColumns(startColumn, 1));
            Assert.Throws<IndexOutOfRangeException>(() => Matrix.RemoveColumns(Setup.GetTestMatrix1(), startColumn, 1));
        }
    }
}

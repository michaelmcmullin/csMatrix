using System;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixOperationExtractTests
    {
        [Theory]
        [InlineData(0, 1, 1, new double[] { 1.0 })]
        [InlineData(2, 1, 1, new double[] { 3.0 })]
        [InlineData(5, 1, 1, new double[] { 6.0 })]
        [InlineData(2, 2, 1, new double[] { 3.0, 6.0 })]
        [InlineData(1, 2, 2, new double[] { 2.0, 3.0, 5.0, 6.0 })]
        public void MatrixExtractElements(int start, int rows, int columns, double[] result)
        {
            Matrix m1 = Setup.GetTestMatrix1();
            m1.Extract(start, rows, columns);
            Matrix m2 = Matrix.Extract(Setup.GetTestMatrix1(), start, rows, columns);

            Matrix expected = new Matrix(rows, columns, result);

            Assert.Equal(result.GetLength(0), m1.Size);
            Assert.Equal(result.GetLength(0), m2.Size);
            Assert.True(m1 == m2);
            Assert.True(m1 == expected);
            Assert.True(m2 == expected);
        }

        [Theory]
        [InlineData(0, 0, 1, 1, new double[] { 1.0 })]
        [InlineData(0, 2, 1, 1, new double[] { 3.0 })]
        [InlineData(1, 2, 1, 1, new double[] { 6.0 })]
        [InlineData(0, 2, 2, 1, new double[] { 3.0, 6.0 })]
        [InlineData(0, 1, 2, 2, new double[] { 2.0, 3.0, 5.0, 6.0 })]
        public void MatrixExtractElementsRowColumn(int startRow, int startColumn, int rows, int columns, double[] result)
        {
            Matrix m1 = Setup.GetTestMatrix1();
            m1.Extract(startRow, startColumn, rows, columns);
            Matrix m2 = Matrix.Extract(Setup.GetTestMatrix1(), startRow, startColumn, rows, columns);

            Matrix expected = new Matrix(rows, columns, result);

            Assert.Equal(result.GetLength(0), m1.Size);
            Assert.Equal(result.GetLength(0), m2.Size);
            Assert.True(m1 == m2);
            Assert.True(m1 == expected);
            Assert.True(m2 == expected);
        }

        [Theory]
        [InlineData(0, 5, 5)]
        [InlineData(5, 1, 2)]
        [InlineData(2, 2, 2)]
        [InlineData(3, 2, 1)]
        public void MatrixExtractInvalidDimensions(int start, int rows, int columns)
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Assert.Throws<InvalidMatrixDimensionsException>(() => m1.Extract(start, rows, columns));
            Assert.Throws<InvalidMatrixDimensionsException>(() => Matrix.Extract(Setup.GetTestMatrix1(), start, rows, columns));
        }

        [Theory]
        [InlineData(6)]
        [InlineData(-1)]
        public void MatrixExtractIndexOutOfRange(int start)
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Assert.Throws<IndexOutOfRangeException>(() => m1.Extract(start, 1, 1));
            Assert.Throws<IndexOutOfRangeException>(() => Matrix.Extract(Setup.GetTestMatrix1(), start, 1, 1));
        }
    }
}

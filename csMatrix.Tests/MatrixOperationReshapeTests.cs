using System;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixOperationReshapeTests
    {
        [Theory]
        [InlineData(0, 1, 1, new double[] { 1.0 })]
        public void MatrixReshapeExtractElements(int start, int rows, int columns, double[] result)
        {
            Matrix m1 = Setup.GetTestMatrix1();
            m1.Reshape(start, rows, columns);
            Matrix m2 = Matrix.Reshape(Setup.GetTestMatrix1(), start, rows, columns);

            Matrix expected = new Matrix(rows, columns, result);

            Assert.Equal(result.GetLength(0), m1.Size);
            Assert.Equal(result.GetLength(0), m2.Size);
            Assert.True(m1 == m2);
            Assert.True(m1 == expected);
            Assert.True(m2 == expected);
        }
    }
}

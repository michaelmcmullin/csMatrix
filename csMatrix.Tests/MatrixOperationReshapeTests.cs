using System;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixOperationReshapeTests
    {
        [Fact]
        public void MatrixReshapeExtractFirstElement()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            m1.Reshape(0, 1, 1);
            Matrix m2 = Matrix.Reshape(Setup.GetTestMatrix1(), 0, 1, 1);

            Matrix expected = new Matrix(new double[,] { { 1.0 } });

            Assert.Equal(1, m1.Size);
            Assert.Equal(1, m2.Size);
            Assert.True(m1 == m2);
            Assert.True(m1 == expected);
            Assert.True(m2 == expected);
        }

        //[Theory]
        //[InlineData(0,1,1,)]
    }
}

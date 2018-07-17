using System;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixEnumeratorTests
    {
        [Fact]
        public void MatrixEnumerator()
        {
            Matrix testMatrix1 = Setup.GetTestMatrix1();
            double start = 1.0;
            foreach (double d in testMatrix1)
            {
                Assert.Equal(start++, d);
            }
        }

        [Fact]
        public void MatrixEnumeratorTranspose()
        {
            double start = 1.0;
            Matrix m = new Matrix(new double[,] { { 1.0, 3.0 }, { 2.0, 4.0 } });
            m.Transpose(true);

            foreach (double d in m)
            {
                Assert.Equal(start++, d);
            }
        }
    }
}

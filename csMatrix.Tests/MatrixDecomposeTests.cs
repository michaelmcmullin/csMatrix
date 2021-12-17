using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixDecomposeTests
    {
        [Fact]
        public void MatrixDecomposeSquareMatrix()
        {
            Matrix m1 = new Matrix(new double[,] { { 3, 7, 2, 5 }, { 1, 8, 4, 2 }, { 2, 1, 9, 3 }, { 5, 4, 7, 1 } });
            Matrix expected = new Matrix(new double[,] { { 5, 4, 7, 1 }, { 0.2, 7.2, 2.6, 1.8 }, { 0.4, -0.083, 6.417, 2.75 }, { 0.6, 0.639, -0.602, 4.905 } });

            Matrix m2 = Matrix.Decompose(m1);
            m1.Decompose();
            Assert.True(m1.NearlyEqual(expected, 0.001));
            Assert.True(m2.NearlyEqual(expected, 0.001));
        }

        [Fact]
        public void MatrixDecomposeNonSquareMatrix()
        {
            Matrix testMatrix1 = Setup.GetTestMatrix1();

            Matrix m = new Matrix(testMatrix1);
            Assert.Throws<InvalidMatrixDimensionsException>(() => m.Decompose());
            Assert.Throws<InvalidMatrixDimensionsException>(() => Matrix.Decompose(m));
        }
    }
}

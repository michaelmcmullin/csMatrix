using System;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixMethodsInverseTests
    {
        [Fact]
        public void MatrixInverseSquareMatrix()
        {
            Matrix m1 = new Matrix(new double[,] { { 1, 0, 5 }, { 2, 1, 6 }, { 3, 4, 0 } });
            Matrix expected = new Matrix(new double[,] { { -24, 20, -5 }, { 18, -15, 4 }, { 5, -4, 1 } });

            Matrix m2 = Matrix.Inverse(m1);
            m1.Inverse();
            Assert.Equal(expected, m1);
            Assert.Equal(expected, m2);
        }

        [Fact]
        public void MatrixInverseSquareNonInvertibleMatrix()
        {
            Matrix m = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });
            Assert.Throws<NonInvertibleMatrixException>(() => m.Inverse());
            Assert.Throws<NonInvertibleMatrixException>(() => Matrix.Inverse(m));
        }

        [Fact]
        public void MatrixInverseNonSquareMatrix()
        {
            Matrix testMatrix1 = Setup.GetTestMatrix1();

            Matrix m = new Matrix(testMatrix1);
            Assert.Throws<InvalidMatrixDimensionsException>(() => m.Inverse());
            Assert.Throws<InvalidMatrixDimensionsException>(() => Matrix.Inverse(m));
        }
    }
}

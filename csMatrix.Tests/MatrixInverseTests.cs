using System;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixInverseTests
    {
        [Fact]
        public void MatrixInverseSquareMatrix()
        {
            Matrix m1 = new Matrix(new double[,] { { 1, 0, 5 }, { 2, 1, 6 }, { 3, 4, 0 } });
            Matrix expected = new Matrix(new double[,] { { -24, 20, -5 }, { 18, -15, 4 }, { 5, -4, 1 } });

            Matrix m2 = Matrix.Inverse(m1);
            m1.Inverse();
            Assert.True(m1.NearlyEqual(expected, 0.001));
            Assert.True(m2.NearlyEqual(expected, 0.001));
        }

        [Fact]
        public void MatrixTimesInverseIsIdentity()
        {
            Matrix m1 = new Matrix(new double[,] { { 1, 0, 5 }, { 2, 1, 6 }, { 3, 4, 0 } });
            Matrix expected = new Matrix(new double[,] { { -24, 20, -5 }, { 18, -15, 4 }, { 5, -4, 1 } });
            Matrix identity = new Matrix(3).Identity();

            Matrix m2 = Matrix.Inverse(m1);
            Matrix actual = m1 * m2;

            Assert.True(m2.NearlyEqual(expected, 0.001));
            Assert.True(actual.NearlyEqual(identity, 0.001));
        }

        [Fact]
        public void MatrixInverseDifficultMatrix()
        {
            Matrix m1 = Setup.GetTestInvertibleMatrix1();
            Matrix expected = Setup.GetExpectedMatrix1Inverted();

            Matrix m2 = Matrix.Inverse(m1);
            m1.Inverse();
            Assert.True(m1.NearlyEqual(expected, 0.0001));
            Assert.True(m2.NearlyEqual(expected, 0.0001));
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

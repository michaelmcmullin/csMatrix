using System;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixMethodsPopulateTests
    {
        [Fact]
        public void MatrixFillDouble()
        {
            Matrix m = new Matrix(2, 2);
            m.Fill(1.5);
            Matrix expected = new Matrix(new double[,] { { 1.5, 1.5 }, { 1.5, 1.5 } });
            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixIdentity()
        {
            Matrix m = new Matrix(3, 3);
            m.Identity();
            Matrix expected = new Matrix(new double[,] { { 1.0, 0.0, 0.0 }, { 0.0, 1.0, 0.0 }, { 0.0, 0.0, 1.0 } });
            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixIdentityNotSquare()
        {
            Matrix m = new Matrix(2, 3);
            Assert.Throws<InvalidMatrixDimensionsException>(() => m.Identity());
        }

        [Fact]
        public void MatrixZeros()
        {
            Matrix m = new Matrix(2, 2);
            m.Fill(30.2);
            m.Zeros();
            Matrix expected = new Matrix(new double[,] { { 0.0, 0.0 }, { 0.0, 0.0 } });
            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixOnes()
        {
            Matrix m = new Matrix(2, 2);
            m.Ones();
            Matrix expected = new Matrix(new double[,] { { 1.0, 1.0 }, { 1.0, 1.0 } });
            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixRand()
        {
            int seed = 5;
            Matrix m1 = new Matrix(2, 2);
            Matrix m2 = new Matrix(2, 2);
            m1.Rand(seed);
            m2.Rand(seed);
            Assert.Equal(m1[0, 0], m2[0, 0]);
            Assert.Equal(m1[1, 1], m2[1, 1]);
        }
    }
}

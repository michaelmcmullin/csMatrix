

using System;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixPopulateTests
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

        [Theory]
        [InlineData(1, new double[] { 1 })]
        [InlineData(3, new double[] { 2, 7, 6, 9, 5, 1, 4, 3, 8 })]
        [InlineData(4, new double[] { 16, 2, 3, 13, 5, 11, 10, 8, 9, 7, 6, 12, 4, 14, 15, 1 })]
        [InlineData(5, new double[] { 9, 3, 22, 16, 15, 2, 21, 20, 14, 8, 25, 19, 13, 7, 1, 18, 12, 6, 5, 24, 11, 10, 4, 23, 17 })]
        public void MatrixIsMagic(int dimension, double[] data)
        {
            Matrix m = new Matrix(dimension, dimension, data);
            Assert.True(m.IsMagic);
        }

        [Theory]
        [InlineData(2, new double[] { 1, 2, 3, 4 })]
        [InlineData(3, new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
        [InlineData(4, new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 })]
        [InlineData(5, new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 })]
        public void MatrixIsNotMagic(int dimension, double[] data)
        {
            Matrix m = new Matrix(dimension, dimension, data);
            Assert.False(m.IsMagic);
        }

        [Fact]
        public void MatrixIsNotMagicAsDimensionsNotSquare()
        {
            Matrix m = Setup.GetTestMatrix1();
            Assert.False(m.IsMagic);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(8)]
        public void MatrixPopulateMagic(int dimension)
        {
            Matrix m = new Matrix(dimension);
            m.Magic();
            Assert.True(m.IsMagic);
        }

        [Theory]
        [InlineData(1,2)]
        [InlineData(2,3)]
        [InlineData(5,1)]
        [InlineData(10,9)]
        public void MatrixPopulateNonSquareMagic(int rows, int columns)
        {
            Matrix m = new Matrix(rows, columns);
            Assert.Throws<InvalidMatrixDimensionsException>(() => m.Magic());
        }
    }
}

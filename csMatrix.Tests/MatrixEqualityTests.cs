using System;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixEqualityTests
    {
        [Fact]
        public void MatrixEquals()
        {
            Matrix m1 = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix m2 = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Assert.True(m1.Equals(m2));
            Assert.True(m1 == m2);
            Assert.False(m1 != m2);
        }

        [Fact]
        public void MatrixEqualsSameValuesDifferentDimensions()
        {
            Matrix m1 = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix m2 = new Matrix(new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } });
            Assert.False(m1.Equals(m2));
            Assert.False(m1 == m2);
            Assert.True(m1 != m2);
        }

        [Fact]
        public void MatrixEqualsTransposed()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            m1.Transpose(true);
            Matrix m2 = Setup.GetTestMatrix1Transposed();

            Assert.True(m1.IsTransposed);
            Assert.False(m2.IsTransposed);
            Assert.True(m1.Equals(m2));
            Assert.True(m1 == m2);
            Assert.False(m1 != m2);
        }

        [Fact]
        public void NullCheck()
        {
            Matrix m = null;
            Assert.True(m == null);
        }

        [Fact]
        public void NullCheckNotEqual()
        {
            Matrix m = null;
            Assert.False(m != null);
        }
    }
}

using System;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixMethodsEqualityTests
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
    }
}

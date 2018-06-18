using System;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixArithmeticTests
    {
        Matrix m1, m2, m3;

        public MatrixArithmeticTests()
        {
            m1 = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 } });
            m2 = new Matrix(new double[,] { { 7.0, 8.0, 9.0 }, { 10.0, 11.0, 12.0 } });
            m3 = new Matrix(new double[,] { { 1.0, 2.0 }, { 3.0, 4.0 } });
        }

        #region Addition
        [Fact]
        public void MatrixArithmeticAddMatrixMatrix()
        {
            Matrix m = MatrixArithmetic.Add(m1, m2);
            Matrix expected = new Matrix(new double[,] { { 8.0, 10.0, 12.0 }, { 14.0, 16.0, 18.0 } });
            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixArithmeticAddMatrixMatrixInvalidDimensions()
        {
            Assert.Throws<InvalidMatrixDimensionsException>(() => MatrixArithmetic.Add(m1, m3));
        }

        [Fact]
        public void MatrixArithmeticAddMatrixNull()
        {
            Assert.Throws<NullReferenceException>(() => MatrixArithmetic.Add(m1, null));
        }

        [Fact]
        public void MatrixArithmeticAddNullMatrix()
        {
            Assert.Throws<NullReferenceException>(() => MatrixArithmetic.Add(null, m1));
        }

        [Fact]
        public void MatrixArithmeticAddMatrixScalar()
        {
            Matrix m = MatrixArithmetic.Add(m1, 3.0);
            Matrix expected = new Matrix(new double[,] { { 4.0, 5.0, 6.0 }, { 7.0, 8.0, 9.0 } });
            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixArithmeticAddNullScalar()
        {
            Assert.Throws<NullReferenceException>(() => MatrixArithmetic.Add(null, 3.0));
        }
        #endregion

        #region Subtraction/Negation
        [Fact]
        public void MatrixArithmeticNegate()
        {
            Matrix m = MatrixArithmetic.Negate(m1);
            Matrix expected = new Matrix(new double[,] { { -1.0, -2.0, -3.0 }, { -4.0, -5.0, -6.0 } });
            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixArithmeticNegateNull()
        {
            Assert.Throws<NullReferenceException>(() => MatrixArithmetic.Negate(null));
        }

        [Fact]
        public void MatrixArithmeticSubtractMatrixMatrix()
        {
            Matrix m = MatrixArithmetic.Subtract(m2, m1);
            Matrix expected = new Matrix(new double[,] { { 6.0, 6.0, 6.0 }, { 6.0, 6.0, 6.0 } });
            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixArithmeticSubtractMatrixMatrixInvalidDimensions()
        {
            Assert.Throws<InvalidMatrixDimensionsException>(() => MatrixArithmetic.Subtract(m1, m3));
        }

        [Fact]
        public void MatrixArithmeticSubtractMatrixNull()
        {
            Assert.Throws<NullReferenceException>(() => MatrixArithmetic.Subtract(m1, null));
        }

        [Fact]
        public void MatrixArithmeticSubtractNullMatrix()
        {
            Assert.Throws<NullReferenceException>(() => MatrixArithmetic.Subtract(null, m1));
        }

        [Fact]
        public void MatrixArithmeticSubtractMatrixScalar()
        {
            Matrix m = MatrixArithmetic.Subtract(m2, 3.0);
            Matrix expected = new Matrix(new double[,] { { 4.0, 5.0, 6.0 }, { 7.0, 8.0, 9.0 } });
            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixArithmeticSubtractNullScalar()
        {
            Assert.Throws<NullReferenceException>(() => MatrixArithmetic.Subtract(null, 3.0));
        }
        #endregion
    }
}
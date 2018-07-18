using System;
using System.Collections.Generic;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixArithmeticSubtractionTests
    {
        public static IEnumerable<object[]> GetObjects = Setup.GetIMatrixArithmetic;

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixNegate(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();

            Matrix m1 = Matrix.Negate(testMatrix1);
            Matrix m2 = new Matrix(testMatrix1); m2.Negate();
            Matrix m3 = -testMatrix1;

            Matrix expected = new Matrix(new double[,] { { -1.0, -2.0, -3.0 }, { -4.0, -5.0, -6.0 } });
            Assert.Equal(expected, m1);
            Assert.Equal(expected, m2);
            Assert.Equal(expected, m3);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixNegateNull(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Assert.Throws<NullReferenceException>(() => Matrix.Negate(null));
            Matrix m1 = null;
            Assert.Throws<NullReferenceException>(() => m1.Negate());
            Assert.Throws<NullReferenceException>(() => -m1);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixSubtractMatrixMatrix(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();
            Matrix testMatrix2 = Setup.GetTestMatrix2();

            Matrix m1 = Matrix.Subtract(testMatrix2, testMatrix1);
            Matrix m2 = new Matrix(testMatrix2); m2.Subtract(testMatrix1);
            Matrix m3 = testMatrix2 - testMatrix1;
            Matrix expected = new Matrix(new double[,] { { 6.0, 6.0, 6.0 }, { 6.0, 6.0, 6.0 } });
            Assert.Equal(expected, m1);
            Assert.Equal(expected, m2);
            Assert.Equal(expected, m3);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixSubtractMatrixMatrixFluent(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();

            Matrix m1 = new Matrix(testMatrix1);
            m1.Subtract(testMatrix1).Subtract(testMatrix1);

            Matrix m2 = new Matrix(testMatrix1);
            m2.Subtract(m2).Subtract(m2); // After the first subtraction, m2 is zero, so further subtractions have no effect

            Matrix m3 = Matrix.Subtract(testMatrix1, testMatrix1).Subtract(testMatrix1);

            Matrix expected1 = -testMatrix1;
            Matrix expected2 = new Matrix(testMatrix1).Zeros();

            Assert.Equal(expected1, m1);
            Assert.Equal(expected2, m2);
            Assert.Equal(expected1, m3);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixSubtractMatrixMatrixInvalidDimensions(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();
            Matrix testMatrix3 = Setup.GetTestMatrix3();

            Assert.Throws<InvalidMatrixDimensionsException>(() => Matrix.Subtract(testMatrix1, testMatrix3));
            Matrix m1 = new Matrix(testMatrix1);
            Assert.Throws<InvalidMatrixDimensionsException>(() => m1.Subtract(testMatrix3));
            Assert.Throws<InvalidMatrixDimensionsException>(() => testMatrix1 - testMatrix3);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixSubtractMatrixNull(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();

            Assert.Throws<NullReferenceException>(() => Matrix.Subtract(testMatrix1, null));
            Matrix m1 = new Matrix(testMatrix1);
            Assert.Throws<NullReferenceException>(() => m1.Subtract(null));
            Assert.Throws<NullReferenceException>(() => m1 - null);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixSubtractNullMatrix(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();

            Assert.Throws<NullReferenceException>(() => Matrix.Subtract(null, testMatrix1));
            Matrix m1 = null;
            Assert.Throws<NullReferenceException>(() => m1.Subtract(testMatrix1));
            Assert.Throws<NullReferenceException>(() => m1 - testMatrix1);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixSubtractMatrixScalar(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();

            double scalar = 3.0;
            Matrix m1 = Matrix.Subtract(testMatrix1, scalar);
            Matrix m2 = new Matrix(testMatrix1); m2.Subtract(scalar);
            Matrix m3 = testMatrix1 - scalar;
            Matrix m4 = scalar - testMatrix1;

            Matrix expected = new Matrix(new double[,] { { -2.0, -1.0, 0.0 }, { 1.0, 2.0, 3.0 } });
            Assert.Equal(expected, m1);
            Assert.Equal(expected, m2);
            Assert.Equal(expected, m3);
            Assert.Equal(expected, m4);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixSubtractNullScalar(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            double scalar = 3.0;
            Assert.Throws<NullReferenceException>(() => Matrix.Subtract(null, scalar));
            Matrix m1 = null;
            Assert.Throws<NullReferenceException>(() => m1.Subtract(scalar));
            Assert.Throws<NullReferenceException>(() => m1 - scalar);
            Assert.Throws<NullReferenceException>(() => scalar - m1);
        }
    }
}

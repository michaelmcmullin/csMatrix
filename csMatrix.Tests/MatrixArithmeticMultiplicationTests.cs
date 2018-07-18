using System;
using System.Collections.Generic;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixArithmeticMultiplicationTests
    {
        public static IEnumerable<object[]> GetObjects = Setup.GetIMatrixArithmetic;

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixMultiplyMatrixMatrix(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();
            Matrix testMatrix3 = Setup.GetTestMatrix3();

            Matrix m1 = Matrix.Multiply(testMatrix1, testMatrix3);
            Matrix m2 = testMatrix1 * testMatrix3;
            Matrix m3 = new Matrix(testMatrix1);
            m3.Multiply(testMatrix3);

            Matrix expected = new Matrix(new double[,] { { 22.0, 28.0 }, { 49.0, 64.0 } });

            Assert.Equal(expected, m1);
            Assert.Equal(expected, m2);
            Assert.Equal(expected, m3);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixMultiplyMatrixMatrixInvalidDimensions(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();
            Matrix testMatrix2 = Setup.GetTestMatrix2();

            Assert.Throws<InvalidMatrixDimensionsException>(() => Matrix.Multiply(testMatrix1, testMatrix2));
            Matrix m1 = new Matrix(testMatrix1);
            Assert.Throws<InvalidMatrixDimensionsException>(() => testMatrix1 * testMatrix2);
            Assert.Throws<InvalidMatrixDimensionsException>(() => testMatrix1.Multiply(testMatrix2));
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixMultiplyMatrixNull(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();

            Assert.Throws<NullReferenceException>(() => Matrix.Multiply(testMatrix1, null));
            Matrix m1 = null;
            Assert.Throws<NullReferenceException>(() => testMatrix1 * m1);
            Assert.Throws<NullReferenceException>(() => testMatrix1.Multiply(m1));
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixMultiplyNullMatrix(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();

            Assert.Throws<NullReferenceException>(() => Matrix.Multiply(null, testMatrix1));
            Matrix m1 = null;
            Assert.Throws<NullReferenceException>(() => m1 * testMatrix1);
            Assert.Throws<NullReferenceException>(() => m1.Multiply(testMatrix1));
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixMultiplyMatrixScalar(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();

            double scalar = 3.0;
            Matrix m1 = Matrix.Multiply(testMatrix1, scalar);
            Matrix m2 = new Matrix(testMatrix1); m2.Multiply(scalar);
            Matrix m3 = testMatrix1 * scalar;
            Matrix m4 = scalar * testMatrix1;
            Matrix expected = new Matrix(new double[,] { { 3.0, 6.0, 9.0 }, { 12.0, 15.0, 18.0 } });
            Assert.Equal(expected, m1);
            Assert.Equal(expected, m2);
            Assert.Equal(expected, m3);
            Assert.Equal(expected, m4);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixMultipyMatrixMatrixFluent(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();
            Matrix testMatrix3 = Setup.GetTestMatrix3();

            Matrix m1 = new Matrix(testMatrix1);
            Matrix m2 = new Matrix(new double[,] { { 22.0, 28.0 }, { 49.0, 64.0 } });
            Matrix expected = new Matrix(new double[,] { { 1856.0, 2408.0 }, { 4214.0, 5468.0 } });

            m1.Multiply(testMatrix3).Multiply(m2);
            Assert.Equal(expected, m1);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixMultiplyMatrixScalarFluent(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();

            double scalar = 3.0;
            Matrix m = new Matrix(testMatrix1);
            m.Multiply(scalar).Multiply(scalar);

            Matrix expected = testMatrix1 * (scalar * scalar);

            Assert.Equal(expected, m);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixMultiplyNullScalar(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            double scalar = 3.0;
            Assert.Throws<NullReferenceException>(() => Matrix.Multiply(null, scalar));
            Matrix m1 = null;
            Assert.Throws<NullReferenceException>(() => m1.Multiply(scalar));
            Assert.Throws<NullReferenceException>(() => m1 * scalar);
        }
    }
}

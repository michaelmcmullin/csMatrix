using System;
using System.Collections.Generic;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixArithmeticAdditionTests
    {
        public static IEnumerable<object[]> GetObjects = Setup.GetIMatrixArithmetic;

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixAddMatrixMatrix(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();
            Matrix testMatrix2 = Setup.GetTestMatrix2();

            Matrix m1 = Matrix.Add(testMatrix1, testMatrix2);
            Matrix m2 = new Matrix(testMatrix1);
            m2.Add(testMatrix2);
            Matrix m3 = testMatrix1 + testMatrix2;
            Matrix expected = new Matrix(new double[,] { { 8.0, 10.0, 12.0 }, { 14.0, 16.0, 18.0 } });
            Assert.Equal(expected, m1);
            Assert.Equal(expected, m2);
            Assert.Equal(expected, m3);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixAddMatrixMatrixFluent(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();

            Matrix m1 = new Matrix(testMatrix1);
            m1.Add(testMatrix1).Add(testMatrix1);

            Matrix m2 = new Matrix(testMatrix1);
            m2.Add(m2).Add(m2);

            Matrix m3 = Matrix.Add(testMatrix1, testMatrix1).Add(testMatrix1);

            Matrix expected1 = testMatrix1 * 3;
            Matrix expected2 = testMatrix1 * 4;

            Assert.Equal(expected1, m1);
            Assert.Equal(expected2, m2);
            Assert.Equal(expected1, m3);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixAddMatrixMatrixInvalidDimensions(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();
            Matrix testMatrix2 = Setup.GetTestMatrix2();
            Matrix testMatrix3 = Setup.GetTestMatrix3();

            Assert.Throws<InvalidMatrixDimensionsException>(() => Matrix.Add(testMatrix1, testMatrix3));
            Matrix m1 = new Matrix(testMatrix1);
            Assert.Throws<InvalidMatrixDimensionsException>(() => m1.Add(testMatrix3));
            Assert.Throws<InvalidMatrixDimensionsException>(() => testMatrix1 + testMatrix3);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixAddMatrixNull(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();

            Assert.Throws<NullReferenceException>(() => Matrix.Add(testMatrix1, null));
            Matrix m1 = new Matrix(testMatrix1);
            Assert.Throws<NullReferenceException>(() => m1.Add(null));
            Assert.Throws<NullReferenceException>(() => m1 + null);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixAddNullMatrix(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();

            Assert.Throws<NullReferenceException>(() => Matrix.Add(null, testMatrix1));
            Matrix m1 = null;
            Assert.Throws<NullReferenceException>(() => m1.Add(testMatrix1));
            Assert.Throws<NullReferenceException>(() => m1 + testMatrix1);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixAddMatrixScalar(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();

            double scalar = 3.0;
            Matrix m1 = Matrix.Add(testMatrix1, scalar);
            Matrix m2 = new Matrix(testMatrix1); m2.Add(scalar);
            Matrix m3 = testMatrix1 + scalar;
            Matrix m4 = scalar + testMatrix1;

            Matrix expected = new Matrix(new double[,] { { 4.0, 5.0, 6.0 }, { 7.0, 8.0, 9.0 } });
            Assert.Equal(expected, m1);
            Assert.Equal(expected, m2);
            Assert.Equal(expected, m3);
            Assert.Equal(expected, m4);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixAddMatrixScalarFluent(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();

            double scalar = 3.0;
            Matrix m = new Matrix(testMatrix1).Add(scalar).Add(scalar);
            Matrix expected = testMatrix1 + (scalar * 2);

            Assert.Equal(expected, m);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixAddNullScalar(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            double scalar = 3.0;
            Assert.Throws<NullReferenceException>(() => Matrix.Add(null, scalar));
            Matrix m1 = null;
            Assert.Throws<NullReferenceException>(() => m1.Add(scalar));
            Assert.Throws<NullReferenceException>(() => m1 + scalar);
            Assert.Throws<NullReferenceException>(() => scalar + m1);
        }
    }
}

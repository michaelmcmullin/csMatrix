using System;
using System.Collections.Generic;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixArithmeticDivisionTests
    {
        public static IEnumerable<object[]> GetObjects = Setup.GetIMatrixArithmetic;

        #region Division
        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixDivideMatrixScalar(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();
            double scalar = 2.0;

            Matrix m1 = Matrix.Divide(testMatrix1, scalar);
            Matrix m2 = new Matrix(testMatrix1); m2.Divide(scalar);
            Matrix m3 = testMatrix1 / scalar;
            Matrix expected = new Matrix(new double[,] { { 0.5, 1.0, 1.5 }, { 2.0, 2.5, 3.0 } });
            Assert.Equal(expected, m1);
            Assert.Equal(expected, m2);
            Assert.Equal(expected, m3);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixDivideMatrixScalarFluent(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();
            double scalar = 2.0;

            Matrix m = new Matrix(testMatrix1);
            m.Divide(scalar).Divide(scalar);
            Matrix expected = new Matrix(new double[,] { { 0.25, 0.5, 0.75 }, { 1.0, 1.25, 1.5 } });
            Assert.Equal(expected, m);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixDivideNullScalar(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            double scalar = 2.0;
            Assert.Throws<NullReferenceException>(() => Matrix.Divide(null, scalar));
            Matrix m = null;
            Assert.Throws<NullReferenceException>(() => m.Divide(scalar));
            Assert.Throws<NullReferenceException>(() => m / scalar);
        }
        #endregion
    }
}

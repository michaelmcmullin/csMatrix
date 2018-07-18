using System;
using System.Collections.Generic;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixMethodsElementOperationTests
    {
        public static IEnumerable<object[]> GetObjects = Setup.GetIMatrixArithmetic;

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixElementOperation(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix m1 = new Matrix(2, 2);
            m1.Fill(3.0);
            Matrix m2 = Matrix.ElementOperation(m1, a => Math.Sin(a));
            m1.ElementOperation(a => Math.Sin(a));
            double expectedElement = Math.Sin(3.0);
            Matrix expected = new Matrix(2, 2, expectedElement);
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixElementOperationScalar(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix m1 = new Matrix(2, 2);
            m1.Fill(3.0);
            Matrix m2 = Matrix.ElementOperation(m1, 2.0, (a, b) => Math.Pow(a, b));
            m1.ElementOperation(2.0, (a, b) => Math.Pow(a, b));
            double expectedElement = Math.Pow(3.0, 2.0);
            Matrix expected = new Matrix(2, 2, expectedElement);
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixElementOperationMatrix(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            double x = 3.0, y = 4.0;
            Matrix m1 = new Matrix(2, 2);
            Matrix m2 = new Matrix(2, 2);
            m1.Fill(x);
            m2.Fill(y);
            Matrix m3 = Matrix.ElementOperation(m1, m2, (a, b) => a + b);
            m1.ElementOperation(m2, (a, b) => a + b);
            Matrix expected = new Matrix(2, 2, x + y);
            Assert.True(expected == m1);
            Assert.True(expected == m3);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixElementOperationInvalidDimensions(IMatrixArithmetic arithmetic)
        {
            Matrix.Arithmetic = arithmetic;
            Matrix testMatrix1 = Setup.GetTestMatrix1();
            Matrix testMatrix3 = Setup.GetTestMatrix3();
            Assert.Throws<InvalidMatrixDimensionsException>(() => Matrix.ElementOperation(testMatrix1, testMatrix3, (a, b) => a + b));
            Matrix m1 = new Matrix(testMatrix1);
            Assert.Throws<InvalidMatrixDimensionsException>(() => m1.ElementOperation(testMatrix3, (a, b) => a + b));
        }
    }
}

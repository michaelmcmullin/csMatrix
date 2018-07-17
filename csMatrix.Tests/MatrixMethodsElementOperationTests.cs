using System;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixMethodsElementOperationTests
    {
        [Fact]
        public void MatrixElementOperation()
        {
            Matrix m1 = new Matrix(2, 2);
            m1.Fill(3.0);
            Matrix m2 = Matrix.ElementOperation(m1, a => Math.Sin(a));
            m1.ElementOperation(a => Math.Sin(a));
            double expectedElement = Math.Sin(3.0);
            Matrix expected = new Matrix(2, 2, expectedElement);
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Fact]
        public void MatrixElementOperationScalar()
        {
            Matrix m1 = new Matrix(2, 2);
            m1.Fill(3.0);
            Matrix m2 = Matrix.ElementOperation(m1, 2.0, (a, b) => Math.Pow(a, b));
            m1.ElementOperation(2.0, (a, b) => Math.Pow(a, b));
            double expectedElement = Math.Pow(3.0, 2.0);
            Matrix expected = new Matrix(2, 2, expectedElement);
            Assert.True(expected == m1);
            Assert.True(expected == m2);
        }

        [Fact]
        public void MatrixElementOperationMatrix()
        {
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

        [Fact]
        public void MatrixElementOperationInvalidDimensions()
        {
            Matrix testMatrix1 = Setup.GetTestMatrix1();
            Matrix testMatrix3 = Setup.GetTestMatrix3();
            Assert.Throws<InvalidMatrixDimensionsException>(() => Matrix.ElementOperation(testMatrix1, testMatrix3, (a, b) => a + b));
            Matrix m1 = new Matrix(testMatrix1);
            Assert.Throws<InvalidMatrixDimensionsException>(() => m1.ElementOperation(testMatrix3, (a, b) => a + b));
        }
    }
}

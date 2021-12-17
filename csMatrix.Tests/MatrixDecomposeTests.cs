using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixDecomposeTests
    {
        [Fact]
        public void MatrixDecomposeSquareMatrix()
        {
            Matrix m1 = new Matrix(new double[,] { { 3, 7, 2, 5 }, { 1, 8, 4, 2 }, { 2, 1, 9, 3 }, { 5, 4, 7, 1 } });
            Matrix expected = new Matrix(new double[,] { { 5, 4, 7, 1 }, { 1, 8, 4, 2 }, { 2, 1, 9, 3 }, { 3, 7, 2, 5 } });

            Matrix m2 = Matrix.Decompose(m1);
            m1.Decompose();
            Assert.Equal(expected, m1);
            Assert.Equal(expected, m2);
        }
    }
}

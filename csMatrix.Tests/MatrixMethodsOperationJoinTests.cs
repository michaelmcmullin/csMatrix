using System;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixMethodsOperationJoinTests
    {
        [Fact]
        public void MatrixOperationsJoinRows()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            m1.Join(Setup.GetTestMatrix1(), MatrixDimension.Rows);
            Matrix m2 = Matrix.Join(Setup.GetTestMatrix1(), Setup.GetTestMatrix1(), MatrixDimension.Rows);

            Matrix expected = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 }, { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 } });
            Assert.True(m1 == expected);
            Assert.True(m2 == expected);
        }

        [Fact]
        public void MatrixOperationsJoinColumns()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            m1.Join(Setup.GetTestMatrix1(), MatrixDimension.Columns);
            Matrix m2 = Matrix.Join(Setup.GetTestMatrix1(), Setup.GetTestMatrix1(), MatrixDimension.Columns);

            Matrix expected = new Matrix(new double[,] { { 1.0, 2.0, 3.0, 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0, 4.0, 5.0, 6.0 } });
            Assert.True(m1 == expected);
            Assert.True(m2 == expected);
        }

        [Fact]
        public void MatrixOperationsJoinAutoRows()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = new Matrix(new double[,] { { 1.0, 2.0, 3.0 } });
            m1.Join(m2, MatrixDimension.Auto);
            Matrix m3 = Matrix.Join(Setup.GetTestMatrix1(), m2, MatrixDimension.Auto);

            Matrix expected = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 }, { 1.0, 2.0, 3.0 } });
            Assert.True(m1 == expected);
            Assert.True(m3 == expected);
        }

        [Fact]
        public void MatrixOperationsJoinAutoColumns()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = new Matrix(new double[,] { { 1.0, 2.0 }, { 3.0, 4.0 } });
            m1.Join(m2, MatrixDimension.Auto);
            Matrix m3 = Matrix.Join(Setup.GetTestMatrix1(), m2, MatrixDimension.Auto);

            Matrix expected = new Matrix(new double[,] { { 1.0, 2.0, 3.0, 1.0, 2.0 }, { 4.0, 5.0, 6.0, 3.0, 4.0 } });
            Assert.True(m1 == expected);
            Assert.True(m3 == expected);
        }

        [Fact]
        public void MatrixOperationsJoinAutoSquare()
        {
            Matrix m1 = Setup.GetTestMatrix4();
            Matrix m2 = new Matrix(new double[,] { { 1.0, 2.0 }, { 3.0, 4.0 } });
            m1.Join(m2, MatrixDimension.Auto);
            Matrix m3 = Matrix.Join(Setup.GetTestMatrix4(), m2, MatrixDimension.Auto);

            Matrix expected = new Matrix(new double[,] { { 22.0, 28.0, 1.0, 2.0 }, { 49.0, 64.0, 3.0, 4.0 } });
            Assert.True(m1 == expected);
            Assert.True(m3 == expected);
        }

        [Fact]
        public void MatrixOperationsJoinRowsIncorrectDimensions()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Assert.Throws<InvalidMatrixDimensionsException>(() => m1.Join(Setup.GetTestMatrix3(), MatrixDimension.Rows));
            Assert.Throws<InvalidMatrixDimensionsException>(() => Matrix.Join(Setup.GetTestMatrix1(), Setup.GetTestMatrix3(), MatrixDimension.Rows));
        }

        [Fact]
        public void MatrixOperationsJoinColumnsIncorrectDimensions()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Assert.Throws<InvalidMatrixDimensionsException>(() => m1.Join(Setup.GetTestMatrix3(), MatrixDimension.Columns));
            Assert.Throws<InvalidMatrixDimensionsException>(() => Matrix.Join(Setup.GetTestMatrix1(), Setup.GetTestMatrix3(), MatrixDimension.Columns));
        }

        [Fact]
        public void MatrixOperationsJoinAutoIncorrectDimensions()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Assert.Throws<InvalidMatrixDimensionsException>(() => m1.Join(Setup.GetTestMatrix3(), MatrixDimension.Auto));
            Assert.Throws<InvalidMatrixDimensionsException>(() => Matrix.Join(Setup.GetTestMatrix1(), Setup.GetTestMatrix3(), MatrixDimension.Auto));
        }
    }
}

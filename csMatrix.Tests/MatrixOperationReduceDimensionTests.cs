using System;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixOperationReduceDimensionTests
    {
        [Fact]
        public void MatrixOperationsReduceColumnsAddTwoRows()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            m1.ReduceDimension(MatrixDimension.Columns, (x, y) => x + y);
            Matrix m2 = Matrix.ReduceDimension(Setup.GetTestMatrix1(), MatrixDimension.Columns, (x, y) => x + y);

            Matrix expected = new Matrix(new double[,] { { 5.0, 7.0, 9.0 } });
            Assert.True(m1 == expected);
            Assert.True(m2 == expected);
        }

        [Fact]
        public void MatrixOperationsReduceColumnsAddThreeRows()
        {
            Matrix m1 = Setup.GetTestMatrix3();
            m1.ReduceDimension(MatrixDimension.Columns, (x, y) => x + y);
            Matrix m2 = Matrix.ReduceDimension(Setup.GetTestMatrix3(), MatrixDimension.Columns, (x, y) => x + y);

            Matrix expected = new Matrix(new double[,] { { 9.0, 12.0 } });
            Assert.True(m1 == expected);
            Assert.True(m2 == expected);
        }

        [Fact]
        public void MatrixOperationsReduceRowsAddTwoColumns()
        {
            Matrix m1 = Setup.GetTestMatrix3();
            m1.ReduceDimension(MatrixDimension.Rows, (x, y) => x + y);
            Matrix m2 = Matrix.ReduceDimension(Setup.GetTestMatrix3(), MatrixDimension.Rows, (x, y) => x + y);

            Matrix expected = new Matrix(new double[,] { { 3.0 }, { 7.0 }, { 11.0 } });
            Assert.True(m1 == expected);
            Assert.True(m2 == expected);
        }

        [Fact]
        public void MatrixOperationsReduceRowsAddThreeColumns()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            m1.ReduceDimension(MatrixDimension.Rows, (x, y) => x + y);
            Matrix m2 = Matrix.ReduceDimension(Setup.GetTestMatrix1(), MatrixDimension.Rows, (x, y) => x + y);

            Matrix expected = new Matrix(new double[,] { { 6.0 }, { 15.0 } });
            Assert.Equal(6.0, m1[0]);
            Assert.True(m1 == expected);
            Assert.True(m2 == expected);
        }

        [Fact]
        public void MatrixOperationsReduceAutoAddTwoRows()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            m1.ReduceDimension(MatrixDimension.Auto, (x, y) => x + y);
            Matrix m2 = Matrix.ReduceDimension(Setup.GetTestMatrix1(), MatrixDimension.Auto, (x, y) => x + y);

            Matrix expected = new Matrix(new double[,] { { 5.0, 7.0, 9.0 } });
            Assert.True(m1 == expected);
            Assert.True(m2 == expected);
        }

        [Fact]
        public void MatrixOperationsReduceAutoAddSingleRow()
        {
            Matrix singleRow = new Matrix(new double[,] { { 1.0, 2.0, 3.0 } });
            Matrix m1 = new Matrix(singleRow);
            m1.ReduceDimension(MatrixDimension.Auto, (x, y) => x + y);
            Matrix m2 = Matrix.ReduceDimension(singleRow, MatrixDimension.Auto, (x, y) => x + y);

            Matrix expected = new Matrix(new double[,] { { 6.0 } });
            Assert.True(m1 == expected);
            Assert.True(m2 == expected);
        }

        [Fact]
        public void MatrixOperationsReduceAutoAddSingleColumn()
        {
            Matrix singleRow = new Matrix(new double[,] { { 1.0 }, { 2.0 }, { 3.0 } });
            Matrix m1 = new Matrix(singleRow);
            m1.ReduceDimension(MatrixDimension.Auto, (x, y) => x + y);
            Matrix m2 = Matrix.ReduceDimension(singleRow, MatrixDimension.Auto, (x, y) => x + y);

            Matrix expected = new Matrix(new double[,] { { 6.0 } });
            Assert.True(m1 == expected);
            Assert.True(m2 == expected);
        }

        [Fact]
        public void MatrixOperationsReduceColumnsMultiplyTwoRows()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            m1.ReduceDimension(MatrixDimension.Columns, (x, y) => x * y);
            Matrix m2 = Matrix.ReduceDimension(Setup.GetTestMatrix1(), MatrixDimension.Columns, (x, y) => x * y);

            Matrix expected = new Matrix(new double[,] { { 4.0, 10.0, 18.0 } });
            Assert.True(m1 == expected);
            Assert.True(m2 == expected);
        }

        [Fact]
        public void MatrixOperationsReduceRowsMultiplyThreeColumns()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            m1.ReduceDimension(MatrixDimension.Rows, (x, y) => x * y);
            Matrix m2 = Matrix.ReduceDimension(Setup.GetTestMatrix1(), MatrixDimension.Rows, (x, y) => x * y);

            Matrix expected = new Matrix(new double[,] { { 6.0 }, { 120.0 } });
            Assert.True(m1 == expected);
            Assert.True(m2 == expected);
        }
    }
}

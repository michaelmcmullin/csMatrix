using System;
using System.Linq;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixOperationStatisticalReduceTests
    {
        [Fact]
        public void MatrixOperationsStatisticalMeanColumns()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.StatisticalReduce(Setup.GetTestMatrix1(), MatrixDimension.Columns, x => x.Data.Average());
            m1.StatisticalReduce(MatrixDimension.Columns, x => x.Data.Average());

            Matrix expected = new Matrix(new double[,] { { 2.5, 3.5, 4.5 } });
            Assert.True(m1 == expected);
            Assert.True(m2 == expected);
        }

        [Fact]
        public void MatrixOperationsStatisticalMeanRows()
        {
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.StatisticalReduce(Setup.GetTestMatrix1(), MatrixDimension.Rows, x => x.Data.Average());
            m1.StatisticalReduce(MatrixDimension.Rows, x => x.Data.Average());

            Matrix expected = new Matrix(new double[,] { { 2.0 } , { 5.0 } });
            Assert.True(m1 == expected);
            Assert.True(m2 == expected);
        }
    }
}

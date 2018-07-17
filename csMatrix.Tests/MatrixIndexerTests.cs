using System;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixIndexerTests
    {
        [Fact]
        public void MatrixIndexRowColumn()
        {
            Matrix testMatrix1 = Setup.GetTestMatrix1();
            Assert.Equal(1.0, testMatrix1[0, 0]);
            Assert.Equal(2.0, testMatrix1[0, 1]);
            Assert.Equal(3.0, testMatrix1[0, 2]);
            Assert.Equal(4.0, testMatrix1[1, 0]);
            Assert.Equal(5.0, testMatrix1[1, 1]);
            Assert.Equal(6.0, testMatrix1[1, 2]);
        }

        [Fact]
        public void MatrixIndexSingle()
        {
            Matrix testMatrix1 = Setup.GetTestMatrix1();
            Assert.Equal(1.0, testMatrix1[0]);
            Assert.Equal(2.0, testMatrix1[1]);
            Assert.Equal(3.0, testMatrix1[2]);
            Assert.Equal(4.0, testMatrix1[3]);
            Assert.Equal(5.0, testMatrix1[4]);
            Assert.Equal(6.0, testMatrix1[5]);
        }

        [Fact]
        public void MatrixIndexRowColumnTransposeSwapDimensions()
        {
            Matrix testMatrix1 = Setup.GetTestMatrix1();
            Matrix m = new Matrix(testMatrix1);
            m.Transpose(true);
            Assert.Equal(1.0, m[0, 0]);
            Assert.Equal(2.0, m[1, 0]);
            Assert.Equal(3.0, m[2, 0]);
            Assert.Equal(4.0, m[0, 1]);
            Assert.Equal(5.0, m[1, 1]);
            Assert.Equal(6.0, m[2, 1]);
        }

        [Fact]
        public void MatrixIndexSingleTransposeSwapDimensions()
        {
            Matrix testMatrix1 = Setup.GetTestMatrix1();
            Matrix m = new Matrix(testMatrix1);
            m.Transpose(true);
            Assert.Equal(1.0, m[0]);
            Assert.Equal(4.0, m[1]);
            Assert.Equal(2.0, m[2]);
            Assert.Equal(5.0, m[3]);
            Assert.Equal(3.0, m[4]);
            Assert.Equal(6.0, m[5]);
        }

        [Fact]
        public void MatrixIndexRowColumnIndexOutOfRangeException()
        {
            Matrix testMatrix1 = Setup.GetTestMatrix1();
            int row = testMatrix1.Rows;
            int columns = testMatrix1.Columns;
            Assert.Throws<IndexOutOfRangeException>(() => testMatrix1[row, columns]);
        }

        [Fact]
        public void MatrixIndexSingleIndexOutOfRangeException()
        {
            Matrix testMatrix1 = Setup.GetTestMatrix1();
            int size = testMatrix1.Size;
            Assert.Throws<IndexOutOfRangeException>(() => testMatrix1[size]);
        }
    }
}

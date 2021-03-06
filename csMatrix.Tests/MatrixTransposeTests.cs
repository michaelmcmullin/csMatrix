﻿using System;
using System.Collections.Generic;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixTransposeTests
    {
        public static IEnumerable<object[]> GetObjects = Setup.GetIMatrixTranspose;

        [Fact]
        public void MatrixTransposeSwapDimensions()
        {
            Matrix m1 = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 } });
            Matrix m2 = new Matrix(m1);

            int rows = m1.Rows;
            int columns = m1.Columns;
            int size = m1.Size;

            m1.Transpose(true);
            Assert.Equal(columns, m1.Rows);
            Assert.Equal(rows, m1.Columns);
            Assert.Equal(size, m1.Size);
            Assert.Equal(4.0, m1[0, 1]);
            Assert.False(m1 == m2);

            m2.IsTransposed = true;
            Assert.True(m1 == m2);
        }

        [Fact]
        public void MatrixTransposeSwapDimensionsArithmetic()
        {
            Matrix m1 = new Matrix(new double[,] { { 1.0, 2.0 }, { 3.0, 4.0 } });
            Matrix m2 = new Matrix(new double[,] { { 1.0, 3.0 }, { 2.0, 4.0 } });
            Matrix m3 = new Matrix(m1);
            Matrix m4 = new Matrix(new double[,] { { 2.0, 6.0 }, { 4.0, 8.0 } });
            m1.Transpose(true);

            Assert.True(m1 == m2);
            m1.Add(m2);
            Assert.True(m1 == m4);
        }

        [Fact]
        public void MatrixTransposeSwapDimensionsIndexing()
        {
            Matrix m1 = new Matrix(new double[,] { { 1.0, 2.0 }, { 3.0, 4.0 }, { 5.0, 6.0 } });
            Matrix m2 = new Matrix(new double[,] { { 1.0, 3.0, 5.0 }, { 2.0, 4.0, 6.0 } });
            m1.Transpose(true);
            Assert.True(m1 == m2);
            Assert.Equal(1.0, m1[0, 0]); Assert.Equal(1.0, m1[0]);
            Assert.Equal(2.0, m1[1, 0]); Assert.Equal(2.0, m1[3]);
            Assert.Equal(3.0, m1[0, 1]); Assert.Equal(3.0, m1[1]);
            Assert.Equal(4.0, m1[1, 1]); Assert.Equal(4.0, m1[4]);
            Assert.Equal(5.0, m1[0, 2]); Assert.Equal(5.0, m1[2]);
            Assert.Equal(6.0, m1[1, 2]); Assert.Equal(6.0, m1[5]);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixTransposePermanentlyWideMatrix(IMatrixTransposeOperations transpose)
        {
            Matrix.TransposeOperations = transpose;
            Matrix testMatrix1 = Setup.GetTestMatrix1();
            Matrix testMatrix1Transposed = Setup.GetTestMatrix1Transposed();

            Matrix m1 = new Matrix(testMatrix1);
            Matrix m2 = Matrix.Transpose(m1);

            m1.Transpose(false);
            Assert.Equal(testMatrix1.Rows, m1.Columns);
            Assert.Equal(testMatrix1.Columns, m1.Rows);
            Assert.True(m1 == testMatrix1Transposed);
            Assert.Equal(1.0, m1[0, 0]); Assert.Equal(1.0, m1[0]);
            Assert.Equal(2.0, m1[1, 0]); Assert.Equal(2.0, m1[2]);
            Assert.Equal(3.0, m1[2, 0]); Assert.Equal(3.0, m1[4]);
            Assert.Equal(4.0, m1[0, 1]); Assert.Equal(4.0, m1[1]);
            Assert.Equal(5.0, m1[1, 1]); Assert.Equal(5.0, m1[3]);
            Assert.Equal(6.0, m1[2, 1]); Assert.Equal(6.0, m1[5]);

            Assert.True(m1 == m2);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixTransposePermanentlyTallMatrix(IMatrixTransposeOperations transpose)
        {
            Matrix.TransposeOperations = transpose;
            Matrix testMatrix1 = Setup.GetTestMatrix1();
            Matrix testMatrix1Transposed = Setup.GetTestMatrix1Transposed();

            Matrix m1 = new Matrix(testMatrix1Transposed);
            Matrix m2 = Matrix.Transpose(m1);

            m1.Transpose(false);
            Assert.Equal(testMatrix1Transposed.Rows, m1.Columns);
            Assert.Equal(testMatrix1Transposed.Columns, m1.Rows);
            Assert.True(m1 == testMatrix1);
            Assert.Equal(1.0, m1[0, 0]); Assert.Equal(1.0, m1[0]);
            Assert.Equal(2.0, m1[0, 1]); Assert.Equal(2.0, m1[1]);
            Assert.Equal(3.0, m1[0, 2]); Assert.Equal(3.0, m1[2]);
            Assert.Equal(4.0, m1[1, 0]); Assert.Equal(4.0, m1[3]);
            Assert.Equal(5.0, m1[1, 1]); Assert.Equal(5.0, m1[4]);
            Assert.Equal(6.0, m1[1, 2]); Assert.Equal(6.0, m1[5]);

            Assert.True(m1 == m2);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixTransposeComparePermanentWithSwappedDimensions(IMatrixTransposeOperations transpose)
        {
            Matrix.TransposeOperations = transpose;
            Matrix testMatrix1 = Setup.GetTestMatrix1();
            Matrix m1 = new Matrix(testMatrix1);
            Matrix m2 = new Matrix(testMatrix1);
            Matrix m3 = Matrix.Transpose(m1);

            m1.Transpose(true);
            m2.Transpose(false);

            Assert.Equal(m1.Rows, m2.Rows);
            Assert.Equal(m1.Columns, m2.Columns);
            Assert.True(m1.HasSameDimensions(m2));
            Assert.Equal(m1[0, 0], m2[0, 0]);
            Assert.Equal(m1[0, 1], m2[0, 1]);
            Assert.Equal(m1[1, 0], m2[1, 0]);
            Assert.Equal(m1[1, 1], m2[1, 1]);
            Assert.Equal(m1[2, 0], m2[2, 0]);
            Assert.Equal(m1[2, 1], m2[2, 1]);

            Assert.True(m1 == m2);
            Assert.True(m3 == m1);
            Assert.True(m3 == m2);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixMultiplyByTransposeMatrixMatrix(IMatrixTransposeOperations transpose)
        {
            Matrix.TransposeOperations = transpose;
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.Transpose(Setup.GetTestMatrix2());
            Matrix expected = m1 * m2;

            Matrix test1 = Matrix.MultiplyByTranspose(m1, Setup.GetTestMatrix2());
            Matrix test2 = Setup.GetTestMatrix1();
            test2.MultiplyByTranspose(Setup.GetTestMatrix2());

            Assert.True(test1 == expected);
            Assert.True(test2 == expected);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixMultiplyByTransposeMatrixSwapDimensionsMatrix(IMatrixTransposeOperations transpose)
        {
            Matrix.TransposeOperations = transpose;
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Setup.GetTestMatrix3();
            Matrix expected = m1 * m2;

            Matrix test1 = Matrix.MultiplyByTranspose(m1, Setup.GetTestMatrix3().Transpose(true));
            Matrix test2 = Setup.GetTestMatrix1();
            test2.MultiplyByTranspose(Setup.GetTestMatrix3().Transpose(true));

            Assert.True(test1 == expected);
            Assert.True(test2 == expected);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixMultiplyByTransposeMatrixMatrixInvalidDimensions(IMatrixTransposeOperations transpose)
        {
            Matrix.TransposeOperations = transpose;
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Setup.GetTestMatrix3();

            Assert.Throws<InvalidMatrixDimensionsException>(() => m1.MultiplyByTranspose(m2));
            Assert.Throws<InvalidMatrixDimensionsException>(() => Matrix.MultiplyByTranspose(m1, m2));
        }
        
        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixMultiplyByOwnTransposeMatrix(IMatrixTransposeOperations transpose)
        {
            Matrix.TransposeOperations = transpose;
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Setup.GetTestMatrix1Transposed();
            Matrix expected = m1 * m2;

            Matrix test1 = Setup.GetTestMatrix1().MultiplyByOwnTranspose();
            Matrix test2 = Matrix.MultiplyByOwnTranspose(Setup.GetTestMatrix1());

            Assert.True(expected == test1);
            Assert.True(expected == test2);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixMultiplyTransposeByMatrixMatrix(IMatrixTransposeOperations transpose)
        {
            Matrix.TransposeOperations = transpose;
            Matrix m1 = Matrix.Transpose(Setup.GetTestMatrix1());
            Matrix m2 = Setup.GetTestMatrix2();
            Matrix expected = m1 * m2;

            Matrix test1 = Matrix.MultiplyTransposeBy(Setup.GetTestMatrix1(), m2);
            Matrix test2 = Setup.GetTestMatrix1();
            test2.MultiplyTransposeBy(Setup.GetTestMatrix2());

            Assert.True(test1 == expected);
            Assert.True(test2 == expected);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixMultiplyTransposeByMatrixSwapDimensionsMatrix(IMatrixTransposeOperations transpose)
        {
            Matrix.TransposeOperations = transpose;
            Matrix m1 = Setup.GetTestMatrix3();
            Matrix m2 = Setup.GetTestMatrix1();
            Matrix expected = m1 * m2;

            Matrix test1 = Matrix.MultiplyTransposeBy(m1.Transpose(true), Setup.GetTestMatrix1());
            Matrix test2 = Setup.GetTestMatrix3().Transpose(true);
            test2.MultiplyTransposeBy(Setup.GetTestMatrix1());

            Assert.True(test1 == expected);
            Assert.True(test2 == expected);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixMultiplyTransposeByMatrixMatrixInvalidDimensions(IMatrixTransposeOperations transpose)
        {
            Matrix.TransposeOperations = transpose;
            Matrix m1 = Setup.GetTestMatrix3();
            Matrix m2 = Setup.GetTestMatrix1();

            Assert.Throws<InvalidMatrixDimensionsException>(() => m1.MultiplyTransposeBy(m2));
            Assert.Throws<InvalidMatrixDimensionsException>(() => Matrix.MultiplyTransposeBy(m1, m2));
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixMultiplyTransposeByMatrix(IMatrixTransposeOperations transpose)
        {
            Matrix.TransposeOperations = transpose;
            Matrix m1 = Matrix.Transpose(Setup.GetTestMatrix1());
            Matrix m2 = Setup.GetTestMatrix1();
            Matrix expected = m1 * m2;

            Matrix test1 = Matrix.MultiplyOwnTransposeBy(Setup.GetTestMatrix1());
            Matrix test2 = Setup.GetTestMatrix1();
            test2.MultiplyOwnTransposeBy();

            Assert.True(expected == test1);
            Assert.True(expected == test2);
        }

        [Theory]
        [MemberData(nameof(GetObjects))]
        public void MatrixMultiplyTransposeByMatrixSwapDimensions(IMatrixTransposeOperations transpose)
        {
            Matrix.TransposeOperations = transpose;
            Matrix m1 = Setup.GetTestMatrix1();
            Matrix m2 = Matrix.Transpose(Setup.GetTestMatrix1());
            Matrix expected = m1 * m2;

            Matrix test1 = Matrix.MultiplyOwnTransposeBy(Setup.GetTestMatrix1().Transpose(true));
            Matrix test2 = Setup.GetTestMatrix1().Transpose(true);
            test2.MultiplyOwnTransposeBy();

            Assert.True(expected == test1);
            Assert.True(expected == test2);
        }

        [Fact]
        public void MatrixTransposeToString()
        {
            Matrix m1 = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 } });
            m1.Transpose(true);
            String expected = "1.00 4.00 \n2.00 5.00 \n3.00 6.00 \n";
            Assert.Equal(expected, m1.ToString());
        }
    }
}

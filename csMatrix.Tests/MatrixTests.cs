using System;
using Xunit;

namespace csMatrix.Tests
{
    public class MatrixTests
    {
        Matrix testMatrix1, testMatrix2, testMatrix3, testMatrix4;
        Matrix testMatrix1Transposed;

        public MatrixTests()
        {
            testMatrix1 = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 } });
            testMatrix2 = new Matrix(new double[,] { { 7.0, 8.0, 9.0 }, { 10.0, 11.0, 12.0 } });
            testMatrix3 = new Matrix(new double[,] { { 1.0, 2.0 }, { 3.0, 4.0 }, { 5.0, 6.0 } });
            testMatrix4 = new Matrix(new double[,] { { 22.0, 28.0 }, { 49.0, 64.0 } });
            testMatrix1Transposed = new Matrix(new double[,] { { 1.0, 4.0 }, { 2.0, 5.0 }, { 3.0, 6.0 } });
        }

        #region Constructors
        [Fact]
        public void MatrixConstructorIntInt()
        {
            int rows = 3, columns = 5;
            Matrix m = new Matrix(rows, columns);
            Assert.Equal(rows, m.Rows);
            Assert.Equal(columns, m.Columns);
        }

        [Fact]
        public void MatrixConstructorIntIntDouble()
        {
            int rows = 3, columns = 5;
            double value = 3.14;
            Matrix m = new Matrix(rows, columns, value);
            Assert.Equal(rows, m.Rows);
            Assert.Equal(columns, m.Columns);
            Assert.Equal(value, m[0, 0]);
            Assert.Equal(value, m[rows - 1, columns - 1]);
            Assert.Equal(value, m[1, 1]);
        }

        [Fact]
        public void MatrixConstructorInt()
        {
            int rows = 3;
            Matrix m = new Matrix(rows, rows);
            Assert.Equal(rows, m.Rows);
            Assert.Equal(rows, m.Columns);
        }

        [Fact]
        public void MatrixConstructorIntDouble()
        {
            int rows = 3;
            double value = 3.14;
            Matrix m = new Matrix(rows, rows, value);
            Assert.Equal(rows, m.Rows);
            Assert.Equal(rows, m.Columns);
            Assert.Equal(value, m[0, 0]);
            Assert.Equal(value, m[rows - 1, rows - 1]);
            Assert.Equal(value, m[1, 1]);
        }

        [Fact]
        public void MatrixConstructorDoubleArray()
        {
            double[,] arr = { { 1, 2, 3 }, { 4, 5, 6 } };
            int rows = arr.GetLength(0);
            int columns = arr.GetLength(1);
            Matrix m = new Matrix(arr);
            Assert.Equal(rows, m.Rows);
            Assert.Equal(columns, m.Columns);
            Assert.Equal(arr[0, 0], m[0, 0]);
            Assert.Equal(arr[rows - 1, columns - 1], m[rows - 1, columns - 1]);
        }

        [Fact]
        public void MatrixConstructorDoubleArrayNull()
        {
            double[,] arr = null;
            Assert.Throws<NullReferenceException>(() => new Matrix(arr));
        }

        [Fact]
        public void MatrixConstructorMatrix()
        {
            int rows = 3, columns = 2;
            double value = 3.14;
            Matrix m1 = new Matrix(rows, columns, value);
            Matrix m2 = new Matrix(m1);
            Assert.Equal(rows, m2.Rows);
            Assert.Equal(columns, m2.Columns);
            Assert.Equal(value, m2[0, 0]);
            Assert.Equal(value, m2[rows - 1, columns - 1]);
            Assert.Equal(value, m2[1, 1]);
        }

        [Fact]
        public void MatrixConstructorMatrixNull()
        {
            Matrix m1 = null;
            Assert.Throws<NullReferenceException>(() => new Matrix(m1));
        }

        [Theory]
        [InlineData(-3, 5)]
        [InlineData(3, -5)]
        [InlineData(-3, -5)]
        public void MatrixConstructorNegativeIntInt(int rows, int columns)
        {
            Assert.Throws<ArgumentException>(() => new Matrix(rows, columns));
        }
        #endregion

        #region Properties
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(5, 5)]
        public void MatrixIsSquare(int rows, int columns)
        {
            Matrix m = new Matrix(rows, columns);
            Assert.True(m.IsSquare);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(5, 2)]
        public void MatrixIsNotSquare(int rows, int columns)
        {
            Matrix m = new Matrix(rows, columns);
            Assert.False(m.IsSquare);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(5, 2)]
        public void MatrixDimensions(int rows, int columns)
        {
            Matrix m = new Matrix(rows, columns);
            Assert.Equal(rows, m.Dimensions[0]);
            Assert.Equal(columns, m.Dimensions[1]);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(5, 2)]
        public void MatrixRowsColumns(int rows, int columns)
        {
            Matrix m = new Matrix(rows, columns);
            Assert.Equal(rows, m.Rows);
            Assert.Equal(columns, m.Columns);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(5, 2)]
        public void MatrixSize(int rows, int columns)
        {
            Matrix m = new Matrix(rows, columns);
            int size = rows * columns;
            Assert.Equal(size, m.Size);
        }
        #endregion

        #region Indexers / Enumerators
        [Fact]
        public void MatrixIndexRowColumn()
        {
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
            int row = testMatrix1.Rows;
            int columns = testMatrix1.Columns;
            Assert.Throws<IndexOutOfRangeException>(() => testMatrix1[row, columns]);
        }

        [Fact]
        public void MatrixIndexSingleIndexOutOfRangeException()
        {
            int size = testMatrix1.Size;
            Assert.Throws<IndexOutOfRangeException>(() => testMatrix1[size]);
        }

        [Fact]
        public void MatrixEnumerator()
        {
            double start = 1.0;
            foreach (double d in testMatrix1)
            {
                Assert.Equal(start++, d);
            }
        }

        [Fact]
        public void MatrixEnumeratorTranspose()
        {
            double start = 1.0;
            Matrix m = new Matrix(new double[,] { { 1.0, 3.0 }, { 2.0, 4.0 } });
            m.Transpose(true);

            foreach (double d in m)
            {
                Assert.Equal(start++, d);
            }
        }
        #endregion

        #region Methods
        #region Element Operations
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
            Assert.Throws<InvalidMatrixDimensionsException>(() => Matrix.ElementOperation(testMatrix1, testMatrix3, (a, b) => a + b));
            Matrix m1 = new Matrix(testMatrix1);
            Assert.Throws<InvalidMatrixDimensionsException>(() => m1.ElementOperation(testMatrix3, (a, b) => a + b));
        }
        #endregion

        #region Addition
        [Fact]
        public void MatrixAddMatrixMatrix()
        {
            Matrix m1 = Matrix.Add(testMatrix1, testMatrix2);
            Matrix m2 = new Matrix(testMatrix1);
            m2.Add(testMatrix2);
            Matrix m3 = testMatrix1 + testMatrix2;
            Matrix expected = new Matrix(new double[,] { { 8.0, 10.0, 12.0 }, { 14.0, 16.0, 18.0 } });
            Assert.Equal(expected, m1);
            Assert.Equal(expected, m2);
            Assert.Equal(expected, m3);
        }

        [Fact]
        public void MatrixAddMatrixMatrixFluent()
        {
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

        [Fact]
        public void MatrixAddMatrixMatrixInvalidDimensions()
        {
            Assert.Throws<InvalidMatrixDimensionsException>(() => Matrix.Add(testMatrix1, testMatrix3));
            Matrix m1 = new Matrix(testMatrix1);
            Assert.Throws<InvalidMatrixDimensionsException>(() => m1.Add(testMatrix3));
            Assert.Throws<InvalidMatrixDimensionsException>(() => testMatrix1 + testMatrix3);
        }

        [Fact]
        public void MatrixAddMatrixNull()
        {
            Assert.Throws<NullReferenceException>(() => Matrix.Add(testMatrix1, null));
            Matrix m1 = new Matrix(testMatrix1);
            Assert.Throws<NullReferenceException>(() => m1.Add(null));
            Assert.Throws<NullReferenceException>(() => m1 + null);
        }

        [Fact]
        public void MatrixAddNullMatrix()
        {
            Assert.Throws<NullReferenceException>(() => Matrix.Add(null, testMatrix1));
            Matrix m1 = null;
            Assert.Throws<NullReferenceException>(() => m1.Add(testMatrix1));
            Assert.Throws<NullReferenceException>(() => m1 + testMatrix1);
        }

        [Fact]
        public void MatrixAddMatrixScalar()
        {
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

        [Fact]
        public void MatrixAddMatrixScalarFluent()
        {
            double scalar = 3.0;
            Matrix m = new Matrix(testMatrix1).Add(scalar).Add(scalar);
            Matrix expected = testMatrix1 + (scalar * 2);

            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixAddNullScalar()
        {
            double scalar = 3.0;
            Assert.Throws<NullReferenceException>(() => Matrix.Add(null, scalar));
            Matrix m1 = null;
            Assert.Throws<NullReferenceException>(() => m1.Add(scalar));
            Assert.Throws<NullReferenceException>(() => m1 + scalar);
            Assert.Throws<NullReferenceException>(() => scalar + m1);
        }
        #endregion

        #region Subtraction/Negation
        [Fact]
        public void MatrixNegate()
        {
            Matrix m1 = Matrix.Negate(testMatrix1);
            Matrix m2 = new Matrix(testMatrix1); m2.Negate();
            Matrix m3 = -testMatrix1;

            Matrix expected = new Matrix(new double[,] { { -1.0, -2.0, -3.0 }, { -4.0, -5.0, -6.0 } });
            Assert.Equal(expected, m1);
            Assert.Equal(expected, m2);
            Assert.Equal(expected, m3);
        }

        [Fact]
        public void MatrixNegateNull()
        {
            Assert.Throws<NullReferenceException>(() => Matrix.Negate(null));
            Matrix m1 = null;
            Assert.Throws<NullReferenceException>(() => m1.Negate());
            Assert.Throws<NullReferenceException>(() => -m1);
        }

        [Fact]
        public void MatrixSubtractMatrixMatrix()
        {
            Matrix m1 = Matrix.Subtract(testMatrix2, testMatrix1);
            Matrix m2 = new Matrix(testMatrix2); m2.Subtract(testMatrix1);
            Matrix m3 = testMatrix2 - testMatrix1;
            Matrix expected = new Matrix(new double[,] { { 6.0, 6.0, 6.0 }, { 6.0, 6.0, 6.0 } });
            Assert.Equal(expected, m1);
            Assert.Equal(expected, m2);
            Assert.Equal(expected, m3);
        }

        [Fact]
        public void MatrixSubtractMatrixMatrixFluent()
        {
            Matrix m1 = new Matrix(testMatrix1);
            m1.Subtract(testMatrix1).Subtract(testMatrix1);

            Matrix m2 = new Matrix(testMatrix1);
            m2.Subtract(m2).Subtract(m2); // After the first subtraction, m2 is zero, so further subtractions have no effect

            Matrix m3 = Matrix.Subtract(testMatrix1, testMatrix1).Subtract(testMatrix1);

            Matrix expected1 = -testMatrix1;
            Matrix expected2 = new Matrix(testMatrix1).Zeros();

            Assert.Equal(expected1, m1);
            Assert.Equal(expected2, m2);
            Assert.Equal(expected1, m3);
        }

        [Fact]
        public void MatrixSubtractMatrixMatrixInvalidDimensions()
        {
            Assert.Throws<InvalidMatrixDimensionsException>(() => Matrix.Subtract(testMatrix1, testMatrix3));
            Matrix m1 = new Matrix(testMatrix1);
            Assert.Throws<InvalidMatrixDimensionsException>(() => m1.Subtract(testMatrix3));
            Assert.Throws<InvalidMatrixDimensionsException>(() => testMatrix1 - testMatrix3);
        }

        [Fact]
        public void MatrixSubtractMatrixNull()
        {
            Assert.Throws<NullReferenceException>(() => Matrix.Subtract(testMatrix1, null));
            Matrix m1 = new Matrix(testMatrix1);
            Assert.Throws<NullReferenceException>(() => m1.Subtract(null));
            Assert.Throws<NullReferenceException>(() => m1 - null);
        }

        [Fact]
        public void MatrixSubtractNullMatrix()
        {
            Assert.Throws<NullReferenceException>(() => Matrix.Subtract(null, testMatrix1));
            Matrix m1 = null;
            Assert.Throws<NullReferenceException>(() => m1.Subtract(testMatrix1));
            Assert.Throws<NullReferenceException>(() => m1 - testMatrix1);
        }

        [Fact]
        public void MatrixSubtractMatrixScalar()
        {
            double scalar = 3.0;
            Matrix m1 = Matrix.Subtract(testMatrix1, scalar);
            Matrix m2 = new Matrix(testMatrix1); m2.Subtract(scalar);
            Matrix m3 = testMatrix1 - scalar;
            Matrix m4 = scalar - testMatrix1;

            Matrix expected = new Matrix(new double[,] { { -2.0, -1.0, 0.0 }, { 1.0, 2.0, 3.0 } });
            Assert.Equal(expected, m1);
            Assert.Equal(expected, m2);
            Assert.Equal(expected, m3);
            Assert.Equal(expected, m4);
        }

        [Fact]
        public void MatrixSubtractNullScalar()
        {
            double scalar = 3.0;
            Assert.Throws<NullReferenceException>(() => Matrix.Subtract(null, scalar));
            Matrix m1 = null;
            Assert.Throws<NullReferenceException>(() => m1.Subtract(scalar));
            Assert.Throws<NullReferenceException>(() => m1 - scalar);
            Assert.Throws<NullReferenceException>(() => scalar - m1);
        }
        #endregion

        #region Multiplication
        [Fact]
        public void MatrixMultiplyMatrixMatrix()
        {
            Matrix m1 = Matrix.Multiply(testMatrix1, testMatrix3);
            Matrix m2 = testMatrix1 * testMatrix3;
            Matrix m3 = new Matrix(testMatrix1);
            m3.Multiply(testMatrix3);

            Matrix expected = new Matrix(new double[,] { { 22.0, 28.0 }, { 49.0, 64.0 } });

            Assert.Equal(expected, m1);
            Assert.Equal(expected, m2);
            Assert.Equal(expected, m3);
        }

        [Fact]
        public void MatrixMultiplyMatrixMatrixInvalidDimensions()
        {
            Assert.Throws<InvalidMatrixDimensionsException>(() => Matrix.Multiply(testMatrix1, testMatrix2));
            Matrix m1 = new Matrix(testMatrix1);
            Assert.Throws<InvalidMatrixDimensionsException>(() => testMatrix1 * testMatrix2);
            Assert.Throws<InvalidMatrixDimensionsException>(() => testMatrix1.Multiply(testMatrix2));
        }

        [Fact]
        public void MatrixMultiplyMatrixNull()
        {
            Assert.Throws<NullReferenceException>(() => Matrix.Multiply(testMatrix1, null));
            Matrix m1 = null;
            Assert.Throws<NullReferenceException>(() => testMatrix1 * m1);
            Assert.Throws<NullReferenceException>(() => testMatrix1.Multiply(m1));
        }

        [Fact]
        public void MatrixMultiplyNullMatrix()
        {
            Assert.Throws<NullReferenceException>(() => Matrix.Multiply(null, testMatrix1));
            Matrix m1 = null;
            Assert.Throws<NullReferenceException>(() => m1 * testMatrix1);
            Assert.Throws<NullReferenceException>(() => m1.Multiply(testMatrix1));
        }

        [Fact]
        public void MatrixMultiplyMatrixScalar()
        {
            double scalar = 3.0;
            Matrix m1 = Matrix.Multiply(testMatrix1, scalar);
            Matrix m2 = new Matrix(testMatrix1); m2.Multiply(scalar);
            Matrix m3 = testMatrix1 * scalar;
            Matrix m4 = scalar * testMatrix1;
            Matrix expected = new Matrix(new double[,] { { 3.0, 6.0, 9.0 }, { 12.0, 15.0, 18.0 } });
            Assert.Equal(expected, m1);
            Assert.Equal(expected, m2);
            Assert.Equal(expected, m3);
            Assert.Equal(expected, m4);
        }

        [Fact]
        public void MatrixMultipyMatrixMatrixFluent()
        {
            Matrix m1 = new Matrix(testMatrix1);
            Matrix m2 = new Matrix(new double[,] { { 22.0, 28.0 }, { 49.0, 64.0 } });
            Matrix expected = new Matrix(new double[,] { { 1856.0, 2408.0 }, { 4214.0, 5468.0 } });

            m1.Multiply(testMatrix3).Multiply(m2);
            Assert.Equal(expected, m1);
        }

        [Fact]
        public void MatrixMultiplyMatrixScalarFluent()
        {
            double scalar = 3.0;
            Matrix m = new Matrix(testMatrix1);
            m.Multiply(scalar).Multiply(scalar);

            Matrix expected = testMatrix1 * (scalar * scalar);

            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixMultiplyNullScalar()
        {
            double scalar = 3.0;
            Assert.Throws<NullReferenceException>(() => Matrix.Multiply(null, scalar));
            Matrix m1 = null;
            Assert.Throws<NullReferenceException>(() => m1.Multiply(scalar));
            Assert.Throws<NullReferenceException>(() => m1 * scalar);
        }
        #endregion

        #region Division
        [Fact]
        public void MatrixDivideMatrixScalar()
        {
            double scalar = 2.0;
            Matrix m1 = Matrix.Divide(testMatrix1, scalar);
            Matrix m2 = new Matrix(testMatrix1); m2.Divide(scalar);
            Matrix m3 = testMatrix1 / scalar;
            Matrix expected = new Matrix(new double[,] { { 0.5, 1.0, 1.5 }, { 2.0, 2.5, 3.0 } });
            Assert.Equal(expected, m1);
            Assert.Equal(expected, m2);
            Assert.Equal(expected, m3);
        }

        [Fact]
        public void MatrixDivideMatrixScalarFluent()
        {
            double scalar = 2.0;
            Matrix m = new Matrix(testMatrix1);
            m.Divide(scalar).Divide(scalar);
            Matrix expected = new Matrix(new double[,] { { 0.25, 0.5, 0.75 }, { 1.0, 1.25, 1.5 } });
            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixDivideNullScalar()
        {
            double scalar = 2.0;
            Assert.Throws<NullReferenceException>(() => Matrix.Divide(null, scalar));
            Matrix m = null;
            Assert.Throws<NullReferenceException>(() => m.Divide(scalar));
            Assert.Throws<NullReferenceException>(() => m / scalar);
        }
        #endregion

        #region Equality
        [Fact]
        public void MatrixEquals()
        {
            Matrix m1 = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix m2 = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Assert.True(m1.Equals(m2));
            Assert.True(m1 == m2);
            Assert.False(m1 != m2);
        }

        [Fact]
        public void MatrixEqualsSameValuesDifferentDimensions()
        {
            Matrix m1 = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Matrix m2 = new Matrix(new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } });
            Assert.False(m1.Equals(m2));
            Assert.False(m1 == m2);
            Assert.True(m1 != m2);
        }
        #endregion

        #region Row/Column operations
        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        [InlineData(5, 2)]
        public void MatrixGetEnumerator(int rows, int columns)
        {
            Matrix m = new Matrix(rows, columns);
            int size = 0;
            foreach (double element in m)
            {
                size++;
            }
            Assert.Equal(m.Size, size);
        }

        [Fact]
        public void MatrixSwapRowsFirstSecond()
        {
            Matrix m = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 }, { 7.0, 8.0, 9.0 } });
            Matrix expected = new Matrix(new double[,] { { 4.0, 5.0, 6.0 }, { 1.0, 2.0, 3.0 }, { 7.0, 8.0, 9.0 } });
            m.SwapRows(0, 1);
            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixSwapRowsLastSecondLast()
        {
            Matrix m = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 }, { 7.0, 8.0, 9.0 } });
            Matrix expected = new Matrix(new double[,] { { 4.0, 5.0, 6.0 }, { 1.0, 2.0, 3.0 }, { 7.0, 8.0, 9.0 } });
            m.SwapRows(0, 1);
            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixSwapRowsOutOfRange()
        {
            Matrix m = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 }, { 7.0, 8.0, 9.0 } });
            Assert.Throws<IndexOutOfRangeException>(() => m.SwapRows(m.Rows, 0));
        }

        [Fact]
        public void MatrixSwapColumnsFirstSecond()
        {
            Matrix m = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 }, { 7.0, 8.0, 9.0 } });
            Matrix expected = new Matrix(new double[,] { { 2.0, 1.0, 3.0 }, { 5.0, 4.0, 6.0 }, { 8.0, 7.0, 9.0 } });
            m.SwapColumns(0, 1);
            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixSwapColumnsLastSecondLast()
        {
            Matrix m = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 }, { 7.0, 8.0, 9.0 } });
            Matrix expected = new Matrix(new double[,] { { 1.0, 3.0, 2.0 }, { 4.0, 6.0, 5.0 }, { 7.0, 9.0, 8.0 } });
            m.SwapColumns(m.Columns - 1, m.Columns - 2);
            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixSwapColumnsOutOfRange()
        {
            Matrix m = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 }, { 7.0, 8.0, 9.0 } });
            Assert.Throws<IndexOutOfRangeException>(() => m.SwapColumns(m.Columns, 0));
        }
        #endregion

        #region Transpose
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

        [Fact]
        public void MatrixTransposePermanentlyWideMatrix()
        {
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

        [Fact]
        public void MatrixTransposePermanentlyTallMatrix()
        {
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

        [Fact]
        public void MatrixTransposeComparePermanentWithSwappedDimensions()
        {
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
        #endregion

        #region Inverse
        [Fact]
        public void MatrixInverseSquareMatrix()
        {
            Matrix m1 = new Matrix(new double[,] { { 1, 0, 5 }, { 2, 1, 6 }, { 3, 4, 0 } });
            Matrix expected = new Matrix(new double[,] { { -24, 20, -5 }, { 18, -15, 4 }, { 5, -4, 1 } });

            Matrix m2 = Matrix.Inverse(m1);
            m1.Inverse();
            Assert.Equal(expected, m1);
            //Assert.Equal(expected, m2);
        }

        [Fact]
        public void MatrixInverseSquareNonInvertibleMatrix()
        {
            Matrix m = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });
            Assert.Throws<NonInvertibleMatrixException>(() => m.Inverse());
            Assert.Throws<NonInvertibleMatrixException>(() => Matrix.Inverse(m));
        }

        [Fact]
        public void MatrixInverseNonSquareMatrix()
        {
            Matrix m = new Matrix(testMatrix1);
            Assert.Throws<InvalidMatrixDimensionsException>(() => m.Inverse());
            Assert.Throws<InvalidMatrixDimensionsException>(() => Matrix.Inverse(m));
        }
        #endregion

        #region Populate Matrix
        [Fact]
        public void MatrixFillDouble()
        {
            Matrix m = new Matrix(2, 2);
            m.Fill(1.5);
            Matrix expected = new Matrix(new double[,] { { 1.5, 1.5 }, { 1.5, 1.5 } });
            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixIdentity()
        {
            Matrix m = new Matrix(3, 3);
            m.Identity();
            Matrix expected = new Matrix(new double[,] { { 1.0, 0.0, 0.0 }, { 0.0, 1.0, 0.0 }, { 0.0, 0.0, 1.0 } });
            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixIdentityNotSquare()
        {
            Matrix m = new Matrix(2, 3);
            Assert.Throws<InvalidMatrixDimensionsException>(() => m.Identity());
        }

        [Fact]
        public void MatrixZeros()
        {
            Matrix m = new Matrix(2, 2);
            m.Fill(30.2);
            m.Zeros();
            Matrix expected = new Matrix(new double[,] { { 0.0, 0.0 }, { 0.0, 0.0 } });
            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixOnes()
        {
            Matrix m = new Matrix(2, 2);
            m.Ones();
            Matrix expected = new Matrix(new double[,] { { 1.0, 1.0 }, { 1.0, 1.0 } });
            Assert.Equal(expected, m);
        }

        [Fact]
        public void MatrixRand()
        {
            int seed = 5;
            Matrix m1 = new Matrix(2, 2);
            Matrix m2 = new Matrix(2, 2);
            m1.Rand(seed);
            m2.Rand(seed);
            Assert.Equal(m1[0, 0], m2[0, 0]);
            Assert.Equal(m1[1, 1], m2[1, 1]);
        }
        #endregion
        #endregion
    }
}

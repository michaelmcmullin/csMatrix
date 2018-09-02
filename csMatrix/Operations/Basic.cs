using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix.Operations
{
    /// <summary>
    /// A basic implementation of IMatrixOperations, used as a default in the Matrix
    /// class.
    /// </summary>
    public class Basic : IMatrixOperations
    {
        /// <summary>
        /// Joins two Matrix instances together.
        /// </summary>
        /// <param name="m1">The first Matrix to join.</param>
        /// <param name="m2">The second Matrix to join to the first.</param>
        /// <param name="dimension">Determines whether the second Matrix should be
        /// joined to this one by adding rows, columns, or automatically determine
        /// the most suitable dimension.</param>
        /// <returns>A Matrix that consists of both input Matrix instances joined
        /// into one single Matrix.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when the
        /// two Matrix instances don't share the correct number of elements along
        /// the specified dimension.</exception>
        public Matrix Join(Matrix m1, Matrix m2, MatrixDimension dimension)
        {
            Matrix result = null;
            switch (dimension)
            {
                case MatrixDimension.Columns:
                    if (m1.Rows != m2.Rows)
                        throw new InvalidMatrixDimensionsException("Matrix instances cannot be joined as they don't have the same number of rows.");
                    result = new Matrix(m1.Rows, m1.Columns + m2.Columns);
                    int index = 0, indexM1 = 0, indexM2 = 0;
                    for (int row = 0; row < m1.Rows; row++)
                    {
                        for (int column = 0; column < m1.Columns; column++)
                        {
                            result[index++] = m1[indexM1++];
                        }
                        for (int column = 0; column < m2.Columns; column++)
                        {
                            result[index++] = m2[indexM2++];
                        }
                    }
                    break;
                case MatrixDimension.Rows:
                    if (m1.Columns != m2.Columns)
                        throw new InvalidMatrixDimensionsException("Matrix instances cannot be joined as they don't have the same number of columns.");
                    result = new Matrix(m1.Rows + m2.Rows, m1.Columns);
                    for (int i = 0; i < m1.Size; i++)
                    {
                        result[i] = m1[i];
                    }
                    for (int i = 0; i < m2.Size; i++)
                    {
                        result[i + m1.Size] = m2[i];
                    }
                    break;
                case MatrixDimension.Auto:
                    if (m1.Rows == m2.Rows)
                        goto case MatrixDimension.Columns;
                    else
                        goto case MatrixDimension.Rows;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// Run a given operation on all elements in a particular dimension to reduce that dimension
        /// to a single row or column.
        /// </summary>
        /// <param name="m">The Matrix to operate on.</param>
        /// <param name="dimension">Indicate whether to operate on rows or columns.</param>
        /// <param name="op">The delegate method to operate with.</param>
        /// <returns>A Matrix populated with the results of performing the given operation.</returns>
        /// <remarks>If the current Matrix is a row or column vector, then a 1*1 Matrix
        /// will be returned, regardless of which dimension is chosen. If the dimension is
        /// set to 'Auto', then the first non-singleton dimension is chosen. If no singleton
        /// dimension exists, then columns are used as the default.</remarks>
        public Matrix ReduceDimension(Matrix m, MatrixDimension dimension, Func<double, double, double> op)
        {
            Matrix result = null;

            // Process calculations
            switch (dimension)
            {
                case MatrixDimension.Auto:
                    // Inspired by Octave, 'Auto' will process the first non-singleton dimension.
                    if (m.Rows == 1 || m.Columns == 1)
                    {
                        result = new Matrix(1, 1);
                        result[0] = m[0];
                        for (int i = 1; i < m.Size; i++)
                            result[0] = op(result[0], m[i]);
                        return result;
                    }
                    else
                    {
                        // No singleton case? Let's go with columns.
                        goto case MatrixDimension.Columns;
                    }
                case MatrixDimension.Columns:
                    result = new Matrix(1, m.Columns);
                    // Initialise the result with the first row of values
                    for (int i = 0; i < m.Columns; i++)
                    {
                        result[i] = m[i];
                    }
                    for (int rowIndex = m.Columns; rowIndex < m.Size; rowIndex += m.Columns)
                    {
                        for (int column = 0; column < m.Columns; column++)
                            result[column] = op(result[column], m[rowIndex + column]);
                    }
                    break;
                case MatrixDimension.Rows:
                    result = new Matrix(m.Rows, 1);
                    // Initialise the result with the first column of values
                    for (int i = 0; i < m.Rows; i++)
                    {
                        result[i] = m[i,0];
                    }
                    for (int row = 0; row < m.Rows; row++)
                        for (int column = 1; column < m.Columns; column++)
                            result[row] = op(result[row], m[row, column]);
                    break;
                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// Extract a new Matrix from an existing one, filling in each column sequentially.
        /// </summary>
        /// <param name="m">The Matrix to extract data from.</param>
        /// <param name="startingIndex">The zero-based starting index of the Matrix to start
        /// extracting data from.</param>
        /// <param name="rows">The number of rows in the reshaped Matrix.</param>
        /// <param name="columns">The number of columns in the reshaped Matrix.</param>
        /// <returns>A reference to this Matrix after reshaping.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when there are not
        /// enough elements to fill the new Matrix.</exception>
        public Matrix Extract(Matrix m, int startingIndex, int rows, int columns)
        {
            if (startingIndex < 0 || startingIndex >= m.Size)
                throw new IndexOutOfRangeException("startingIndex is not set to a valid index.");
            if (m.Size < (startingIndex + (rows * columns)))
                throw new InvalidMatrixDimensionsException("There are not enough elements in the Matrix to extract.");
            int startRow = startingIndex / m.Columns;
            int startColumn = startingIndex % m.Columns;
            if (startRow + rows > m.Rows)
                throw new InvalidMatrixDimensionsException("There are not enough rows in the Matrix to extract.");
            if (startColumn + columns > m.Columns)
                throw new InvalidMatrixDimensionsException("There are not enough columns in the Matrix to extract.");

            Matrix output = new Matrix(rows, columns);

            int dataIndex = startingIndex;
            int step = m.Columns - columns;
            for (int i = 0; i < columns; i++)
            {
                dataIndex = startingIndex + i;
                for (int j = 0; j < rows; j++)
                {
                    output[j, i] = m[dataIndex];
                    dataIndex += m.Columns;
                }
            }
            return output;
        }

        /// <summary>
        /// Run a set of operations on all elements in a particular dimension to reduce that dimension
        /// to a single row, and then perform an aggregate operation to produce a statistical result.
        /// </summary>
        /// <param name="m">The Matrix to operate on.</param>
        /// <param name="dimension">Indicate whether to operate on rows or columns.</param>
        /// <param name="op">The delegate method to operate with.</param>
        /// <remarks>If the current Matrix is a row or column vector, then a 1*1 Matrix
        /// will be returned, regardless of which dimension is chosen. If the dimension is
        /// set to 'Auto', then the first non-singleton dimension is chosen. If no singleton
        /// dimension exists, then columns are used as the default.</remarks>
        public Matrix StatisticalReduce(Matrix m, MatrixDimension dimension, Func<Matrix, double> op)
        {
            Matrix result = null;

            switch (dimension)
            {
                case MatrixDimension.Auto:
                    if (m.Rows == 1)
                    {
                        result = new Matrix(1, 1);
                        result[0] = op(m);
                        return result;
                    }
                    else if (m.Columns == 1)
                    {
                        result = new Matrix(1, 1);
                        result[0] = op(m);
                        return result;
                    }
                    else
                    {
                        // No singleton case? Let's go with columns.
                        goto case MatrixDimension.Columns;
                    }
                case MatrixDimension.Columns:
                    result = new Matrix(1, m.Columns);
                    for (int i = 0; i < m.Columns; i++)
                        result[i] = op(Matrix.ExtractColumns(m, i, 1));
                    break;
                case MatrixDimension.Rows:
                    result = new Matrix(m.Rows, 1);
                    for (int i = 0; i < m.Rows; i++)
                        result[i] = op(Matrix.ExtractRows(m, i, 1));
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}

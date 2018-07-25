using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix.Operations
{
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

        public Matrix ReduceDimension(Matrix m, MatrixDimension dimension, Func<double, double, double> op)
        {
            throw new NotImplementedException();
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

        public Matrix StatisticalReduce(Matrix m, MatrixDimension dimension, Func<Matrix, double> op)
        {
            throw new NotImplementedException();
        }
    }
}

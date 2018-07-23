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

        public Matrix Reshape(Matrix m, int startingIndex, int rows, int columns)
        {
            throw new NotImplementedException();
        }

        public Matrix StatisticalReduce(Matrix m, MatrixDimension dimension, Func<Matrix, double> op)
        {
            throw new NotImplementedException();
        }
    }
}

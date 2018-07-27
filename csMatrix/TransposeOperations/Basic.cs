using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix.TransposeOperations
{
    public class Basic : IMatrixTransposeOperations
    {
        /// <summary>
        /// Multiply one Matrix by the transpose of a second Matrix.
        /// </summary>
        /// <param name="m1">The Matrix to multiply.</param>
        /// <param name="m2">The Matrix to multiply the first Matrix by its transpose.</param>
        /// <returns>A new Matrix with the result of multiplying Matrix m1 by the transpose
        /// of Matrix m2.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when the two Matrix
        /// instances have incompatible dimensions for multiplication.</exception>
        public Matrix MultiplyByTranspose(Matrix m1, Matrix m2)
        {
            if (m1.Columns != m2.Columns)
            {
                throw new InvalidMatrixDimensionsException("Cannot multiply by transpose because the second Matrix has a different number of columns to the first.");
            }

            Matrix result = new Matrix(m1.Rows, m2.Rows);

            for (int row = 0; row < m1.Rows; row++)
            {
                for (int column = 0; column < m2.Rows; column++)
                {
                    double sum = 0;
                    for (int i = 0; i < m2.Columns; i++)
                    {
                        sum += (m1[row, i] * m2[column, i]);
                    }
                    result[row, column] = sum;
                }
            }

            return result;
        }

        /// <summary>
        /// Multiply a Matrix by its own transpose.
        /// </summary>
        /// <param name="m">The Matrix to multiply by its own transpose.</param>
        /// <returns>A new Matrix with the result of multiplying Matrix m by the transpose
        /// of itself.</returns>
        public Matrix MultiplyByTranspose(Matrix m)
        {
            Matrix result = new Matrix(m.Rows, m.Rows);

            for (int row = 0; row < m.Rows; row++)
            {
                for (int column = 0; column < m.Rows; column++)
                {
                    double sum = 0;
                    for (int i = 0; i < m.Columns; i++)
                    {
                        sum += (m[row, i] * m[column, i]);
                    }
                    result[row, column] = sum;
                }
            }

            return result;
        }

        public Matrix MultiplyTransposeBy(Matrix m1, Matrix m2)
        {
            throw new NotImplementedException();
        }

        public Matrix MultiplyTransposeBy(Matrix m1)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the transposed version of a Matrix (swap rows and columns)
        /// </summary>
        /// <param name="m">The Matrix to transpose.</param>
        public Matrix Transpose(Matrix m)
        {
            Matrix t;

            if (m.IsTransposed)
            {
                t = new Matrix(m);
                t.IsTransposed = false;
            }
            else
            {
                t = new Matrix(m.Columns, m.Rows);
                for (int index = 0; index < m.Size; index++)
                {
                    int i = index / m.Rows;
                    int j = index % m.Rows;
                    t[index] = m[(m.Columns * j) + i];
                }
            }
            return t;
        }
    }
}

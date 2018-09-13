using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace csMatrix.TransposeOperations
{
    /// <summary>
    /// Matrix Transpose operations that work in parallel
    /// </summary>
    public class ParallelOperations : IMatrixTransposeOperations
    {
        /// <summary>
        /// Multiply one Matrix by the transpose of a second Matrix.
        /// </summary>
        /// <param name="m1">The Matrix to multiply.</param>
        /// <param name="m2">The Matrix to multiply the first Matrix by its transpose.</param>
        /// <returns>A new Matrix with the result of multiplying Matrix m1 by the transpose
        /// of Matrix m2.</returns>
        /// <remarks>Assumes that m1.Columns == m2.Columns. The Matrix class checks this before
        /// calling this method.</remarks>
        public Matrix MultiplyByTranspose(Matrix m1, Matrix m2)
        {
            Matrix output = new Matrix(m1.Rows, m2.Rows);
            Parallel.For(0, m1.Rows, i => MultiplyByTransposedRow(i, m1, m2, ref output));
            return output;
        }

        /// <summary>
        /// Multiply a Matrix by its own transpose.
        /// </summary>
        /// <param name="m">The Matrix to multiply by its own transpose.</param>
        /// <returns>A new Matrix with the result of multiplying Matrix m by the transpose
        /// of itself.</returns>
        public Matrix MultiplyByTranspose(Matrix m)
        {
            return MultiplyByTranspose(m, m);
        }

        /// <summary>
        /// Multiply the transpose of one Matrix by a second Matrix.
        /// </summary>
        /// <param name="m1">The Matrix to transpose and multiply.</param>
        /// <param name="m2">The Matrix to multiply the transposed Matrix by.</param>
        /// <returns>A new Matrix with the result of multiplying the transpose of Matrix
        /// m1 by Matrix m2.</returns>
        /// <remarks>Assumes m1.Rows == m2.Rows. The Matrix class checks for this before
        /// calling this method.</remarks>
        public Matrix MultiplyTransposeBy(Matrix m1, Matrix m2)
        {
            Matrix output = new Matrix(m1.Columns, m2.Columns);
            Parallel.For(0, m1.Columns, i => MultiplyByTransposedColumn(i, m1, m2, ref output));
            return output;
        }

        /// <summary>
        /// Multiply the Transpose of a Matrix by the original Matrix.
        /// </summary>
        /// <param name="m">The Matrix to transpose, and multiply by itself.</param>
        /// <returns>The result of multiplying the Transpose of the given Matrix
        /// by itself.</returns>
        public Matrix MultiplyTransposeBy(Matrix m)
        {
            return MultiplyTransposeBy(m, m);
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
                Parallel.For(0, m.Size, index => {
                    int i = index / m.Rows;
                    int j = index % m.Rows;
                    t[index] = m[(m.Columns * j) + i];
                });
            }
            return t;
        }

        #region Utility Methods
        /// <summary>
        /// Calculate a single row result of multiplying two matrices.
        /// </summary>
        /// <param name="row">The zero-indexed row from m1 to calculate.</param>
        /// <param name="m1">The first Matrix to multiply.</param>
        /// <param name="m2">The second Matrix to multiply.</param>
        /// <param name="output">The Matrix to store the results in.</param>
        private static void MultiplyByTransposedRow(int row, Matrix m1, Matrix m2, ref Matrix output)
        {
            int m1_index = row * m1.Columns;
            int output_index = row * output.Columns;
            int m2_index = 0;

            for (int column = 0; column < output.Columns; column++)
            {
                double result = 0;

                for (int i = 0; i < m1.Columns; i++)
                {
                    result += m1[m1_index + i] * m2[m2_index++];
                }

                output[output_index++] = result;
            }
        }

        /// <summary>
        /// Calculate a single row result of multiplying row transposed into a column, by a corresponding
        /// row in a second Matrix.
        /// </summary>
        /// <param name="column">The zero-indexed row from m1 to transpose into a column.</param>
        /// <param name="m1">The first Matrix to multiply.</param>
        /// <param name="m2">The second Matrix to multiply.</param>
        /// <param name="output">The Matrix to store the results in.</param>
        private static void MultiplyByTransposedColumn(int column, Matrix m1, Matrix m2, ref Matrix output)
        {
            int output_index = column * output.Columns;
            int m1_index, m2_index;

            for (int m2Col = 0; m2Col < m2.Columns; m2Col++)
            {
                double result = 0;
                m1_index = column;
                m2_index = m2Col;

                for (int m2Row = 0; m2Row < m2.Rows; m2Row++)
                {
                    result += m1[m1_index] * m2[m2_index];
                    m1_index += m1.Columns;
                    m2_index += m2.Columns;
                }

                output[output_index++] = result;
            }
        }
        #endregion
    }
}

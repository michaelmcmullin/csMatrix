using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace csMatrix.Arithmetic
{
    /// <summary>
    /// Matrix arithmetic operations that work in parallel
    /// </summary>
    public class ParallelOperations : IMatrixArithmetic
    {
        /// <summary>
        /// Perform the given operation on each Matrix element.
        /// </summary>
        /// <param name="m">The Matrix to perform elementwise operations on.</param>
        /// <param name="op">The operation to perform on each Matrix element.</param>
        public void ElementOperation(Matrix m, Func<double, double> op)
        {
            Parallel.For(0, m.Rows, i => RowMatrixOperation(i, m, op));
        }

        /// <summary>
        /// Perform the given operation on each Matrix element.
        /// </summary>
        /// <param name="m1">The first Matrix to perform elementwise operations on.</param>
        /// <param name="m2">The second Matrix to perform elementwise operations with.</param>
        /// <param name="op">The operation to perform on each Matrix element.</param>
        /// <remarks>Assume m1 and m2 are not null, and have the same dimensions.
        /// These checks are currently carried out in the client application.</remarks>
        public void ElementOperation(Matrix m1, Matrix m2, Func<double, double, double> op)
        {
            Parallel.For(0, m1.Rows, i => RowMatrixOperation(i, m1, m2, op));
        }

        /// <summary>
        /// Perform the given operation on each Matrix element.
        /// </summary>
        /// <param name="m">The Matrix to perform elementwise operations on.</param>
        /// <param name="scalar">A scalar value to use as a second operand in each operation.</param>
        /// <param name="op">The operation to perform on each Matrix element.</param>
        public void ElementOperation(Matrix m, double scalar, Func<double, double, double> op)
        {
            Parallel.For(0, m.Rows, i => RowScalarOperation(i, m, scalar, op));
        }

        /// <summary>
        /// Compare one Matrix with a second Matrix by its element's values.
        /// </summary>
        /// <param name="m1">The first Matrix to compare.</param>
        /// <param name="m2">The second Matrix to compare.</param>
        /// <returns>True if both matrices contain the same elements and dimensions.</returns>
        public bool Equals(Matrix m1, Matrix m2)
        {
            if (m1 is null) return (m2 is null);
            if (ReferenceEquals(m1, m2)) return true;

            if (!m1.HasSameDimensions(m2)) return false;
            bool result = true;
            Parallel.For(0, m1.Size, (i, loopState) => {
                if (m1[i] != m2[i])
                {
                    loopState.Stop();
                    result = false;
                    return;
                }
            });
            return result;
        }

        /// <summary>
        /// Multiply two matrices together.
        /// </summary>
        /// <param name="m1">An n*m dimension Matrix.</param>
        /// <param name="m2">An m*p dimension Matrix.</param>
        /// <returns>An n*p Matrix that is the product of m1 and m2.</returns>
        /// <remarks>Client application ensures that the number of columns in m1
        /// matchs the number of rows in m2.</remarks>
        public Matrix Multiply(Matrix m1, Matrix m2)
        {
            Matrix output = new Matrix(m1.Rows, m2.Columns);
            Parallel.For(0, m1.Rows, i => MultiplyRow(i, m1, m2, ref output));
            return output;
        }

        #region Private utility methods
        /// <summary>
        /// Applies a scalar operation to every element in a single Matrix row
        /// </summary>
        /// <param name="row">The row index to apply the operation to.</param>
        /// <param name="m">The Matrix to operate on.</param>
        /// <param name="scalar">The scalar value to apply to each element.</param>
        /// <param name="op">The operation to execute on each Matrix row element.</param>
        private static void RowScalarOperation(int row, Matrix m, double scalar, Func<double, double, double> op)
        {
            int m_index = row * m.Columns;

            for (int i = m_index; i < m_index + m.Columns; i++)
            {
                m[i] = op(m[i], scalar);
            }
        }

        /// <summary>
        /// Calculate a single row result of operating on two matrices.
        /// </summary>
        /// <param name="row">The row index to process.</param>
        /// <param name="m1">The Matrix to operate on.</param>
        /// <param name="m2">The second Matrix to use elements against the first Matrix.</param>
        /// <param name="op">The operation to execute on each Matrix row element.</param>
        private static void RowMatrixOperation(int row, Matrix m1, Matrix m2, Func<double, double, double> op)
        {
            int m_index = row * m1.Columns;

            for (int i = m_index; i < m_index + m1.Columns; i++)
            {
                m1[i] = op(m1[i], m2[i]);
            }
        }

        /// <summary>
        /// Apply an operation to each element in a given Matrix row.
        /// </summary>
        /// <param name="row">The row index to process.</param>
        /// <param name="m">The Matrix to operate on.</param>
        /// <param name="op">The operation to execute on each Matrix row element.</param>
        private static void RowMatrixOperation(int row, Matrix m, Func<double, double> op)
        {
            int m_index = row * m.Columns;

            for (int i = m_index; i < m_index + m.Columns; i++)
            {
                m[i] = op(m[i]);
            }
        }

        /// <summary>
        /// Calculate a single row result of multiplying two matrices.
        /// </summary>
        /// <param name="row">The zero-indexed row to calculate.</param>
        /// <param name="m1">The first Matrix to multiply.</param>
        /// <param name="m2">The second Matrix to multiply.</param>
        /// <param name="output">The Matrix to store the results in.</param>
        private static void MultiplyRow(int row, Matrix m1, Matrix m2, ref Matrix output)
        {
            int m1_index = row * m1.Columns;
            int m2_index;

            for (int column = 0; column < output.Columns; column++)
            {
                double result = 0;
                m2_index = column;

                for (int i = 0; i < m1.Columns; i++)
                {
                    result += m1[m1_index + i] * m2[m2_index];
                    m2_index += m2.Columns;
                }

                output[row, column] = result;
            }
        }
        #endregion
    }
}

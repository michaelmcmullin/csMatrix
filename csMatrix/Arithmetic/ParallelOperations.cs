using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace csMatrix.Arithmetic
{
    /// <summary>
    /// Version of Matrix arithmetic operations that works in parallel
    /// </summary>
    public class ParallelOperations : IMatrixArithmetic
    {
        public void ElementOperation(Matrix m, Func<double, double> op)
        {
            throw new NotImplementedException();
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

        public bool Equals(Matrix m1, Matrix m2)
        {
            throw new NotImplementedException();
        }

        public Matrix Multiply(Matrix m1, Matrix m2)
        {
            throw new NotImplementedException();
        }

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

        private static void RowMatrixOperation(int row, Matrix m1, Matrix m2, Func<double, double, double> op)
        {
            int m_index = row * m1.Columns;

            for (int i = m_index; i < m_index + m1.Columns; i++)
            {
                m1[i] = op(m1[i], m2[i]);
            }
        }
    }
}

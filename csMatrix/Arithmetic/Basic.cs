using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix.Arithmetic
{
    /// <summary>
    /// Default implementation of Matrix arithmetic operations
    /// </summary>
    public class Basic : IMatrixArithmetic
    {
        /// <summary>
        /// Perform the given operation on each Matrix element.
        /// </summary>
        /// <param name="m">The Matrix to perform elementwise operations on.</param>
        /// <param name="op">The operation to perform on each Matrix element.</param>
        public void ElementOperation(Matrix m, Func<double, double> op)
        {
            for (int i = 0; i < m.Size; i++)
            {
                m[i] = op(m[i]);
            }
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
            for (int i = 0; i < m1.Size; i++)
            {
                m1[i] = op(m1[i], m2[i]);
            }
        }

        /// <summary>
        /// Perform the given operation on each Matrix element.
        /// </summary>
        /// <param name="m">The Matrix to perform elementwise operations on.</param>
        /// <param name="scalar">A scalar value to use as a second operand in each operation.</param>
        /// <param name="op">The operation to perform on each Matrix element.</param>
        public void ElementOperation(Matrix m, double scalar, Func<double, double, double> op)
        {
            for (int i = 0; i < m.Size; i++)
            {
                m[i] = op(m[i], scalar);
            }
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

            for (int row = 0; row < m1.Rows; row++)
            {
                for (int column = 0; column < m1.Columns; column++)
                {
                    if (m1[row, column] != m2[row, column]) return false;
                }
            }
            return true;
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
            Matrix result = new Matrix(m1.Rows, m2.Columns);
            for (int row = 0; row < m1.Rows; row++)
            {
                for (int column = 0; column < m2.Columns; column++)
                {
                    double sum = 0;
                    for (int i = 0; i < m2.Rows; i++)
                    {
                        sum += (m1[row, i] * m2[i, column]);
                    }
                    result[row, column] = sum;
                }
            }
            return result;
        }
    }
}

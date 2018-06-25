using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix.Arithmetic
{
    public class Basic : IMatrixArithmetic
    {
        /// <summary>
        /// Add one Matrix to another.
        /// </summary>
        /// <param name="m1">The Matrix to add to.</param>
        /// <param name="m2">The second Matrix to add to the first.</param>
        /// <remarks>Assume m1 and m2 are not null, and have the same dimensions.
        /// These checks are currently carried out in the client application.</remarks>
        public void Add(Matrix m1, Matrix m2)
        {
            for (int i = 0; i < m1.Size; i++)
            {
                m1[i] += m2[i];
            }
        }

        /// <summary>
        /// Add a number to each element in a given Matrix.
        /// </summary>
        /// <param name="m">The Matrix to add numbers to.</param>
        /// <param name="scalar">The number to add to each element in a Matrix.</param>
        public void Add(Matrix m, double scalar)
        {
            for (int i = 0; i < m.Size; i++)
            {
                m[i] += scalar;
            }
        }

        /// <summary>
        /// Scalar division of a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to apply division to.</param>
        /// <param name="scalar">The scalar value to divide each element of the Matrix by.</param>
        public void Divide(Matrix m, double scalar)
        {
            for (int i = 0; i < m.Size; i++)
            {
                m[i] /= scalar;
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

        /// <summary>
        /// Scalar multiplication of a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to apply multiplication to.</param>
        /// <param name="scalar">The scalar value to multiply each element of the Matrix by.</param>
        public void Multiply(Matrix m, double scalar)
        {
            for (int i = 0; i < m.Size; i++)
            {
                m[i] *= scalar;
            }
        }

        /// <summary>
        /// Unary negative operator, negates every element in a given Matrix.
        /// </summary>
        /// <param name="m">The Matrix to negate.</param>
        public void Negate(Matrix m)
        {
            for (int i = 0; i < m.Size; i++)
            {
                m[i] = -m[i];
            }
        }
        /// <summary>
        /// Subtract one Matrix from another.
        /// </summary>
        /// <param name="m1">The first Matrix to subtract from.</param>
        /// <param name="m2">The second Matrix to subtract from the first.</param>
        /// <remarks>Assume m1 and m2 are not null, and have the same dimensions.
        /// These checks are currently carried out in the client application.</remarks>
        public void Subtract(Matrix m1, Matrix m2)
        {
            for (int i = 0; i < m1.Size; i++)
            {
                m1[i] -= m2[i];
            }
        }

        /// <summary>
        /// Subtract a number from each element in a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to subtract the number from.</param>
        /// <param name="scalar">The number to subtract from each element in the Matrix.</param>
        public void Subtract(Matrix m, double scalar)
        {
            for (int i = 0; i < m.Size; i++)
            {
                m[i] -= scalar;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix
{
    /// <summary>
    /// Methods designed to mutate an existing Matrix. Useful in situations where
    /// creating a new Matrix is expensive.
    /// </summary>
    public class MatrixMutators
    {
        #region Arithmetic
        /// <summary>
        /// Add one Matrix to another.
        /// </summary>
        /// <param name="m1">The Matrix to add to.</param>
        /// <param name="m2">The second Matrix to add to the first.</param>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when both matrices have
        /// different dimensions.</exception>
        /// <exception cref="NullReferenceException">Thrown when either Matrix is null.</exception>
        public static void Add(Matrix m1, Matrix m2)
        {
            if (m1 == null || m2 == null) throw new NullReferenceException("Matrix cannot be null");
            if (!m1.HasSameDimensions(m2)) throw new InvalidMatrixDimensionsException("Cannot add Matrices with different dimensions");
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
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static void Add(Matrix m, double scalar)
        {
            if (m == null) throw new NullReferenceException("Matrix cannot be null");
            for (int i = 0; i < m.Size; i++)
            {
                m[i] += scalar;
            }
        }

        /// <summary>
        /// Unary negative operator, negates every element in a given Matrix.
        /// </summary>
        /// <param name="m">The Matrix to negate.</param>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static void Negate(Matrix m)
        {
            if (m == null) throw new NullReferenceException("Matrix cannot be null");
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
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when both matrices have
        /// different dimensions.</exception>
        /// <exception cref="NullReferenceException">Thrown when either Matrix is null.</exception>
        public static void Subtract(Matrix m1, Matrix m2)
        {
            if (m1 == null || m2 == null) throw new NullReferenceException("Matrix cannot be null");
            if (!m1.HasSameDimensions(m2)) throw new InvalidMatrixDimensionsException("Cannot subtract Matrices with different dimensions");
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
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static void Subtract(Matrix m, double scalar)
        {
            if (m == null) throw new NullReferenceException("Matrix cannot be null");
            for (int i = 0; i < m.Size; i++)
            {
                m[i] -= scalar;
            }
        }

        /// <summary>
        /// Scalar multiplication of a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to apply multiplication to.</param>
        /// <param name="scalar">The scalar value to multiply each element of the Matrix by.</param>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static void Multiply(Matrix m, double scalar)
        {
            if (m == null) throw new NullReferenceException("Matrix cannot be null");
            for (int i = 0; i < m.Size; i++)
            {
                m[i] *= scalar;
            }
        }

        /// <summary>
        /// Scalar division of a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to apply division to.</param>
        /// <param name="scalar">The scalar value to divide each element of the Matrix by.</param>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static void Divide(Matrix m, double scalar)
        {
            if (m == null) throw new NullReferenceException("Matrix cannot be null");
            for (int i = 0; i < m.Size; i++)
            {
                m[i] /= scalar;
            }
        }
        #endregion

        #region Row/Column operations
        /// <summary>
        /// Swap two rows in a given Matrix.
        /// </summary>
        /// <param name="m">The Matrix to operate on.</param>
        /// <param name="row1">The first row to swap.</param>
        /// <param name="row2">The second row to swap.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when called with non-existent rows.</exception>
        public static void SwapRows(Matrix m, int row1, int row2)
        {
            if (row1 >= m.Rows || row2 >= m.Rows)
                throw new IndexOutOfRangeException("SwapRow method called with non-existent rows.");

            if (row1 == row2) return;

            double[] tmp = new double[m.Columns];
            int indexRow1 = row1 * m.Columns;
            int indexRow2 = row2 * m.Columns;

            for (int i = 0; i < m.Columns; i++)
            {
                tmp[i] = m[indexRow1 + i];
                m[indexRow1 + i] = m[indexRow2 + i];
                m[indexRow2 + i] = tmp[i];
            }

        }

        /// <summary>
        /// Swap two columns in a given Matrix.
        /// </summary>
        /// <param name="m">The Matrix to operate on.</param>
        /// <param name="column1">The first column to swap.</param>
        /// <param name="column2">The second column to swap.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when called with non-existent columns.</exception>
        public static void SwapColumns(Matrix m, int column1, int column2)
        {
            if (column1 >= m.Columns || column2 >= m.Columns)
                throw new IndexOutOfRangeException("SwapColumns method called with non-existent columns.");

            if (column1 == column2) return;

            int indexColumn1 = column1, indexColumn2 = column2;
            double tmp;
            for (int i = 0; i < m.Rows; i++)
            {
                tmp = m[indexColumn1];
                m[indexColumn1] = m[indexColumn2];
                m[indexColumn2] = tmp;
                indexColumn1 += m.Columns;
                indexColumn2 += m.Columns;
            }
        }
        #endregion
    }
}

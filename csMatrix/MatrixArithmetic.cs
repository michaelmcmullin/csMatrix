using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix
{
    /// <summary>
    /// Methods for arithmetic operations on Matrices. All methods produce a new Matrix instance.
    /// </summary>
    public class MatrixArithmetic
    {
        /// <summary>
        /// Add two matrices together.
        /// </summary>
        /// <param name="m1">The first Matrix to add.</param>
        /// <param name="m2">The second Matrix to add.</param>
        /// <returns>The result of adding the two matrices together.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when both matrices have
        /// different dimensions.</exception>
        /// <exception cref="NullReferenceException">Thrown when either Matrix is null.</exception>
        public static Matrix Add(Matrix m1, Matrix m2)
        {
            if (m1 == null || m2 == null) throw new NullReferenceException("Matrix cannot be null");
            if (!m1.HasSameDimensions(m2)) throw new InvalidMatrixDimensionsException("Cannot add Matrices with different dimensions");
            Matrix result = new Matrix(m1);
            MatrixMutators.Add(result, m2);
            return result;
        }

        /// <summary>
        /// Add a number to each element in a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to add numbers to.</param>
        /// <param name="scalar">The number to add to each element in a Matrix.</param>
        /// <returns>The result of adding the number to each element in a Matrix.</returns>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static Matrix Add(Matrix m, double scalar)
        {
            if (m == null) throw new NullReferenceException("Matrix cannot be null");
            Matrix result = new Matrix(m);
            MatrixMutators.Add(result, scalar);
            return result;
        }

        /// <summary>
        /// Unary negative operator.
        /// </summary>
        /// <param name="m">The Matrix to negate.</param>
        /// <returns>The result of negating every element in the given Matrix.</returns>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static Matrix Negate(Matrix m)
        {
            if (m == null) throw new NullReferenceException("Matrix cannot be null");
            Matrix result = new Matrix(m);
            MatrixMutators.Negate(result);
            return result;
        }

        /// <summary>
        /// Subtract one Matrix from another.
        /// </summary>
        /// <param name="m1">The first Matrix to subtract from.</param>
        /// <param name="m2">The second Matrix to subtract from the first.</param>
        /// <returns>The result of subtracting the second Matrix from the first.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when both matrices have
        /// different dimensions.</exception>
        /// <exception cref="NullReferenceException">Thrown when either Matrix is null.</exception>
        public static Matrix Subtract(Matrix m1, Matrix m2)
        {
            if (m1 == null || m2 == null) throw new NullReferenceException("Matrix cannot be null");
            if (!m1.HasSameDimensions(m2)) throw new InvalidMatrixDimensionsException("Cannot subtract Matrices with different dimensions");
            Matrix result = new Matrix(m1);
            MatrixMutators.Subtract(result, m2);
            return result;
        }

        /// <summary>
        /// Subtract a number from each element in a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to subtract from the number.</param>
        /// <param name="scalar">The number to subtract from each element in a Matrix.</param>
        /// <returns>The result of subracting a given number from each element.</returns>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static Matrix Subtract(Matrix m, double scalar)
        {
            if (m == null) throw new NullReferenceException("Matrix cannot be null");
            Matrix result = new Matrix(m);
            MatrixMutators.Subtract(result, scalar);
            return result;
        }

        /// <summary>
        /// Multiply two matrices together.
        /// </summary>
        /// <param name="m1">An n*m dimension Matrix.</param>
        /// <param name="m2">An m*p dimension Matrix.</param>
        /// <returns>An n*p Matrix that is the product of m1 and m2.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when the number of columns in the
        /// first Matrix don't match the number of rows in the second Matrix.</exception>
        /// <exception cref="NullReferenceException">Thrown when either Matrix is null.</exception>
        public static Matrix Multiply(Matrix m1, Matrix m2)
        {
            if (m1 == null || m2 == null) throw new NullReferenceException("Matrix cannot be null");
            if (m1.Rows != m2.Columns) throw new InvalidMatrixDimensionsException("Matrices can only be multiplied if the number of columns in the first Matrix match the number of rows in the second Matrix");
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
        /// <returns>A Matrix representing the scalar multiplication of m * scalar.</returns>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static Matrix Multiply(Matrix m, double scalar)
        {
            if (m == null) throw new NullReferenceException("Matrix cannot be null");
            Matrix result = new Matrix(m);
            MatrixMutators.Multiply(result, scalar);
            return result;
        }

        /// <summary>
        /// Scalar division of a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to apply division to.</param>
        /// <param name="scalar">The scalar value to divide each element of the Matrix by.</param>
        /// <returns>A Matrix representing the scalar division of m / scalar.</returns>
        /// <exception cref="NullReferenceException">Thrown when Matrix is null.</exception>
        public static Matrix Divide(Matrix m, double scalar)
        {
            if (m == null) throw new NullReferenceException("Matrix cannot be null");
            Matrix result = new Matrix(m);
            MatrixMutators.Divide(result, scalar);
            return result;
        }
    }
}

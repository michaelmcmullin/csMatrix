using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix
{
    /// <summary>
    /// Methods for arithmetic operations on Matrices. All methods produce a new Matrix instance.
    /// </summary>
    class MatrixArithmetic
    {
        /// <summary>
        /// Add two matrices together.
        /// </summary>
        /// <param name="m1">The first Matrix to add.</param>
        /// <param name="m2">The second Matrix to add.</param>
        /// <returns>The result of adding the two matrices together.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when both matrices have
        /// different dimensions.</exception>
        public static Matrix Add(Matrix m1, Matrix m2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a number to each element in a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to add numbers to.</param>
        /// <param name="scalar">The number to add to each element in a Matrix.</param>
        /// <returns>The result of adding the number to each element in a Matrix.</returns>
        public static Matrix Add(Matrix m1, double scalar)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Unary negative operator.
        /// </summary>
        /// <param name="m1">The Matrix to negate.</param>
        /// <returns>The result of negating every element in the given Matrix.</returns>
        public static Matrix Negate(Matrix m1)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Subtract one Matrix from another.
        /// </summary>
        /// <param name="m1">The first Matrix to subtract from.</param>
        /// <param name="m2">The second Matrix to subtract from the first.</param>
        /// <returns>The result of subtracting the second Matrix from the first.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when both matrices have
        /// different dimensions.</exception>
        public static Matrix Subtract(Matrix m1, Matrix m2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Subtract a number from each element in a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to subtract from the number.</param>
        /// <param name="scalar">The number to subtract from each element in a Matrix.</param>
        /// <returns>The result of subracting a given number from each element.</returns>
        public static Matrix Subtract(Matrix m, double scalar)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Multiply two matrices together.
        /// </summary>
        /// <param name="m1">An n*m dimension Matrix.</param>
        /// <param name="m2">An m*p dimension Matrix.</param>
        /// <returns>An n*p Matrix that is the product of m1 and m2.</returns>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when the number of columns in the
        /// first Matrix don't match the number of rows in the second Matrix.</exception>
        public static Matrix Multiply(Matrix m1, Matrix m2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scalar multiplication of a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to apply multiplication to.</param>
        /// <param name="scalar">The scalar value to multiply each element of the Matrix by.</param>
        /// <returns>A Matrix representing the scalar multiplication of m * scalar.</returns>
        public static Matrix Multiply(Matrix m, double scalar)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scalar division of a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to apply division to.</param>
        /// <param name="scalar">The scalar value to divide each element of the Matrix by.</param>
        /// <returns>A Matrix representing the scalar division of m / scalar.</returns>
        public static Matrix Divide(Matrix m, double scalar)
        {
            throw new NotImplementedException();
        }

    }
}

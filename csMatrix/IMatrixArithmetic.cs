using System;

namespace csMatrix
{
    /// <summary>
    /// Interface that Matrix classes use for arithmetic operations.
    /// </summary>
    public interface IMatrixArithmetic
    {
        /// <summary>
        /// Multiply two matrices together.
        /// </summary>
        /// <param name="m1">An n*m dimension Matrix.</param>
        /// <param name="m2">An m*p dimension Matrix.</param>
        /// <returns>An n*p Matrix that is the product of m1 and m2.</returns>
        /// <remarks>Client application ensures that the number of columns in m1
        /// matchs the number of rows in m2.</remarks>
        Matrix Multiply(Matrix m1, Matrix m2);

        /// <summary>
        /// Compare one Matrix with a second Matrix by its element's values.
        /// </summary>
        /// <param name="m1">The first Matrix to compare.</param>
        /// <param name="m2">The second Matrix to compare.</param>
        /// <returns>True if both matrices contain the same elements and dimensions.</returns>
        bool Equals(Matrix m1, Matrix m2);

        /// <summary>
        /// Perform the given operation on each Matrix element.
        /// </summary>
        /// <param name="m">The Matrix to perform elementwise operations on.</param>
        /// <param name="op">The operation to perform on each Matrix element.</param>
        void ElementOperation(Matrix m, Func<double, double> op);

        /// <summary>
        /// Perform the given operation on each Matrix element.
        /// </summary>
        /// <param name="m1">The first Matrix to perform elementwise operations on.</param>
        /// <param name="m2">The second Matrix to perform elementwise operations with.</param>
        /// <param name="op">The operation to perform on each Matrix element.</param>
        /// <remarks>Assume m1 and m2 are not null, and have the same dimensions.
        /// These checks are currently carried out in the client application.</remarks>
        void ElementOperation(Matrix m1, Matrix m2, Func<double, double, double> op);

        /// <summary>
        /// Perform the given operation on each Matrix element.
        /// </summary>
        /// <param name="m">The Matrix to perform elementwise operations on.</param>
        /// <param name="scalar">A scalar value to use as a second operand in each operation.</param>
        /// <param name="op">The operation to perform on each Matrix element.</param>
        void ElementOperation(Matrix m, double scalar, Func<double, double, double> op);
    }
}

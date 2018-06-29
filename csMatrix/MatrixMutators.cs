using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix
{
    /// <summary>
    /// Methods designed to mutate an existing Matrix. Useful in situations where
    /// creating a new Matrix is expensive.
    /// </summary>
    internal class MatrixMutators
    {

        #region Populate
        /// <summary>
        /// Fills the Matrix with a given number.
        /// </summary>
        /// <param name="m">The Matrix to populate with the given number.</param>
        /// <param name="number">The number to assign to every element in the Matrix.</param>
        public static void Fill(Matrix m, double number)
        {
            for (int i = 0; i < m.Size; i++)
                m[i] = number;
        }
        #endregion
    }
}

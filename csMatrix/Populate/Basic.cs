using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix.Populate
{
    public class Basic : IMatrixPopulate
    {
        /// <summary>
        /// Fills the Matrix with a given number.
        /// </summary>
        /// <param name="m">The Matrix to populate with the given number.</param>
        /// <param name="number">The number to assign to every element in the Matrix.</param>
        public void Number(Matrix m, double number)
        {
            for (int i = 0; i < m.Size; i++)
                m[i] = number;
        }

        /// <summary>
        /// Populates an identity Matrix (all zeros, but ones along the main diagonal)
        /// </summary>
        /// <param name="m">A square Matrix to populate with the appropriate values.</param>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when Matrix is not
        /// square.</exception>
        public void Identity(Matrix m)
        {
            if (!m.IsSquare) throw new InvalidMatrixDimensionsException("An Identity matrix must be square.");
            int diagonal = 0;
            for (int i = 0; i < m.Size; i++)
            {
                if (i == diagonal)
                {
                    m[i] = 1;
                    diagonal += m.Columns + 1;
                }
                else
                {
                    m[i] = 0;
                }
            }
        }

        /// <summary>
        /// Fills the Matrix with random numbers between 0.0 (inclusive) and 1.0 (exclusive).
        /// </summary>
        /// <param name="m">The Matrix to populate with random numbers.</param>
        /// <param name="seed">The number used to calculate a starting value.</param>
        public void Rand(Matrix m, int seed)
        {
            Random random = new Random(seed);
            for (int i = 0; i < m.Size; i++)
            {
                m[i] = random.NextDouble();
            }
        }
    }
}

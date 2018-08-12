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

        /// <summary>
        /// Fills the Matrix with numbers such that the sum of each row, column and
        /// diagonal results in the same constant.
        /// </summary>
        /// <param name="m">The Matrix to populate as a Magic Square.</param>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown if the dimensions
        /// of the given Matrix is not square.</exception>
        public void Magic(Matrix m)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines if a given Matrix is a Magic Square (i.e. the sum of each row, column
        /// or diagonal results in the same constant).
        /// </summary>
        /// <param name="m">The Matrix to test.</param>
        /// <returns>Returns true if Matrix m is a Magic Square; otherwise false.</returns>
        /// <remarks>A Magic Square of order n contains the elements 1 to n squared.</remarks>
        public bool IsMagic(Matrix m)
        {
            if (m.Columns != m.Rows) return false;
            if (m.Columns == 2) return false;

            double n = m.Columns;
            double magicConstant = (n * ((n * n) + 1)) / 2;
            double diagonalSum = 0;
            double reverseDiagonalSum = 0;
            var usedElements = new Dictionary<double, double>();

            for (int i = 0; i < m.Rows; i++)
            {
                double rowSum = 0;
                double columnSum = 0;

                for (int j = 0; j < m.Columns; j++)
                {
                    double thisElement = m[i, j];

                    rowSum += thisElement;
                    columnSum += m[j, i];

                    if (!usedElements.ContainsKey(thisElement))
                        usedElements[thisElement] = 1;
                    else
                        return false; // This Matrix does not contain distinct numbers
                }
                if (rowSum != magicConstant || columnSum != magicConstant) return false;

                diagonalSum += m[i, i];
                reverseDiagonalSum += m[i, m.Columns - i - 1];
            }

            return reverseDiagonalSum == magicConstant && diagonalSum == magicConstant;
        }
    }
}

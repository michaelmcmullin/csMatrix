using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix.Populate
{
    /// <summary>
    /// A basic implementation of IMatrixPopulate, used as a default in the Matrix
    /// class.
    /// </summary>
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
        /// of the given Matrix is not square, or if they're of order 2.</exception>
        public void Magic(Matrix m)
        {
            if (m.Rows != m.Columns)
                throw new InvalidMatrixDimensionsException("Cannot fill a non-square Matrix as a Magic Square.");

            int n = m.Rows;
            if (n == 2)
                throw new InvalidMatrixDimensionsException("A Magic Square cannot be of the order n=2.");
            // Handle special cases first
            if (n == 1)
                m[0] = 1.0;

            // Different dimension types use various methods to populate them:
            if (n % 2 == 1)
            {
                // CASE 1: Populate where n is odd.
                //
                // Set the first value and initialize current position to
                // halfway across the first row.
                int startColumn = n >> 1;
                int startRow = 0;
                m[startRow, startColumn] = 1;

                // Keep moving up and to the right until all squares are filled
                int newRow, newColumn;

                for (int i = 2; i <= n * n; i++)
                {
                    newRow = startRow - 1; newColumn = startColumn + 1;
                    if (newRow < 0) newRow = n - 1;
                    if (newColumn >= n) newColumn = 0;

                    if (m[newRow, newColumn] > 0)
                    {
                        while (m[startRow, startColumn] > 0)
                        {
                            startRow++;
                            if (startRow >= n) startRow = 0;
                        }
                    }
                    else
                    {
                        startRow = newRow; startColumn = newColumn;
                    }
                    m[startRow, startColumn] = i;
                }
            }
            else if (n % 4 == 0)
            {
                // CASE 2: Populate where n is doubly-even (divisible by 4).
                //
                // Imagine 8 smaller squares, n/4 in size, running along each diagonal.
                // Place the numbers in sequence if they don't fall inside one of these
                // squares, otherwise place a number in reverse sequence (index2).
                double index1 = 1;
                double index2 = m.Size;

                int n2 = n / 2;
                int n4 = n / 4;
                for (int row = 0; row < n; row++)
                {
                    for (int column = 0; column < n; column++)
                    {
                        if (
                            ((row >= n4 && row < n4 + n2) && (column < n4 || column >= n4 + n2)) ||
                            ((column >= n4 && column < n4 + n2) && (row < n4 || row >= n4 + n2))
                            )
                        {
                            m[row, column] = index1;
                        }
                        else
                        {
                            m[row, column] = index2;
                        }
                        index1++;
                        index2--;
                    }
                }
            }
            else
            {
                // CASE 3: Populate where n is singly-even (divisible by 2, but not 4).
                //
                // Divide into 4 quadrants, creating magic square in each.
                int n2 = n / 2;
                Matrix quadrant = new Matrix(n2);
                quadrant.Magic();

                // Offsets for each quadrant
                double[] deltas = { 0, quadrant.Size, quadrant.Size * 2, quadrant.Size * 3 };

                // Initialise other variables
                int leftColumnCount = quadrant.Columns / 2;
                int rightColumnCount = leftColumnCount - 1;
                int midRow = leftColumnCount;

                // Initial population
                for (int row = 0; row < n2; row++)
                {
                    for (int column = 0; column < n2; column++)
                    {
                        m[row, column] = quadrant[row, column]; // Quadrant A (TL)
                        m[row + n2, column + n2] = quadrant[row, column] + deltas[1]; // Quadrant B (BR)
                        m[row, column + n2] = quadrant[row, column] + deltas[2]; // Quadrant C (TR)
                        m[row + n2, column] = quadrant[row, column] + deltas[3]; // Quadrant D (BL)
                    }
                }

                int swapRow, swapColumn;
                double tmp;
                for (int row = 0; row < n2; row++)
                {
                    swapRow = row + n2;
                    // Swap relevant elements in quadrants A and D
                    for (int column = 0; column < leftColumnCount; column++)
                    {
                        swapColumn = row == midRow ? column + 1 : column;
                        tmp = m[row, swapColumn];
                        m[row, swapColumn] = m[swapRow, swapColumn];
                        m[swapRow, swapColumn] = tmp;
                    }
                    // Swap relevant elements in quadrants C and B
                    for (int column = m.Columns - rightColumnCount; column < m.Columns; column++)
                    {
                        tmp = m[row, column];
                        m[row, column] = m[swapRow, column];
                        m[swapRow, column] = tmp;
                    }
                }
            }
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

            for (int i = 0; i < m.Rows; i++)
            {
                double rowSum = 0;
                double columnSum = 0;

                for (int j = 0; j < m.Columns; j++)
                {
                    double thisElement = m[i, j];

                    rowSum += thisElement;
                    columnSum += m[j, i];
                }
                if (rowSum != magicConstant || columnSum != magicConstant) return false;

                diagonalSum += m[i, i];
                reverseDiagonalSum += m[i, m.Columns - i - 1];
            }

            return reverseDiagonalSum == magicConstant && diagonalSum == magicConstant;
        }
    }
}

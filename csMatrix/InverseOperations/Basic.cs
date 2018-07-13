using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix.InverseOperations
{
    public class Basic : IMatrixInverseOperations
    {
        /// <summary>
        /// Calculate the inverse of a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to invert.</param>
        /// <returns>The inverse Matrix of the supplied Matrix.</returns>
        public Matrix Inverse(Matrix m)
        {
            if (!m.IsSquare)
                throw new InvalidMatrixDimensionsException("Inverse requires a Matrix to be square.");
            Matrix working = new Matrix(m);
            Matrix result = new Matrix(m.Rows);
            result.Identity();

            for (int diagonal = 0; diagonal < m.Rows; diagonal++)
            {
                double diagonalValue = working[diagonal, diagonal];

                // Ensure the diagonal value is not zero by swapping another row if necessary.
                if (diagonalValue == 0)
                {
                    for (int i = 0; i < m.Rows; i++)
                    {
                        if (i != diagonal && m[i, diagonal] != 0 && working[diagonal, i] != 0)
                        {
                            working.SwapRows(diagonal, i);
                            result.SwapRows(diagonal, i);
                            diagonalValue = working[diagonal, diagonal];
                            break;
                        }
                    }
                    if (diagonalValue == 0)
                        throw new NonInvertibleMatrixException("This Matrix is not invertible");
                }

                int lineValueIndex = diagonal;
                int itemIndex = 0;
                int diagonalIndex = diagonal * working.Columns;

                for (int row = 0; row < working.Rows; row++)
                {
                    if (row != diagonal)
                    {
                        double lineValue = working[lineValueIndex];
                        for (int column = 0; column < working.Columns; column++)
                        {
                            int diagonalColumnIndex = diagonalIndex + column;
                            working[itemIndex] = (working[itemIndex] * diagonalValue) - (working[diagonalColumnIndex] * lineValue);
                            result[itemIndex] = (result[itemIndex] * diagonalValue) - (result[diagonalColumnIndex] * lineValue);
                            itemIndex++;
                        }
                    }
                    else
                    {
                        itemIndex += working.Columns;
                    }
                    lineValueIndex += working.Columns;
                }
            }

            // By now all the rows should be filled in...
            int indexResult = 0;
            int indexThis = 0;

            for (int i = 0; i < working.Rows; i++)
            {
                double divisor = working[indexThis];
                indexThis += working.Columns + 1;

                for (int j = 0; j < working.Columns; j++)
                {
                    result[indexResult++] /= divisor;
                }
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix.DecompositionOperations
{
    public class BasicLU : IMatrixDecompositionOperations
    {
        public int Decompose(Matrix m, out Matrix result, out int[] perm)
        {
            // Adapted from https://jamesmccaffrey.wordpress.com/2012/12/20/matrix-decomposition/
            if (!m.IsSquare)
                throw new InvalidMatrixDimensionsException("Decomposition requires a Matrix to be square.");

            int toggle = 1;
            result = new Matrix(m);
            var n = m.Rows;
            perm = new int[n]; // set up row permutation result

            for (int i = 0; i < n; ++i) { perm[i] = i; }

            for (int column = 0; column < n - 1; column++)
            {
                double colMax = Math.Abs(result[column, column]);
                int pRow = column;
                for (int row = column + 1; row < n; row++)
                {
                    if (result[row, column] > colMax)
                    {
                        colMax = result[row, column];
                        pRow = row;
                    }
                }

                if (pRow != column) // if largest value not on pivot, swap rows
                {
                    result.SwapRows(pRow, column);
                    int tmp = perm[pRow]; // and swap perm info
                    perm[pRow] = perm[column];
                    perm[column] = tmp;
                    toggle = -toggle;
                }

                if (Math.Abs(result[column, column]) < 1.0E-20) // if diagonal after swap is zero . . .
                    throw new InvalidOperationException("Unable to decompose the Matrix");

                for (int i = column + 1; i < n; i++)
                {
                    result[i, column] /= result[column, column];
                    for (int k = column + 1; k < n; k++)
                    {
                        result[i, k] -= result[i, column] * result[column, k];
                    }
                }
            }

            return toggle;
        }
    }
}

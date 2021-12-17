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
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when the supplied Matrix is
        /// not square.</exception>
        /// <exception cref="NonInvertibleMatrixException">Thrown when the supplied Matrix is not
        /// invertible.</exception>
        /// <returns>The inverse Matrix of the supplied Matrix.</returns>
        public Matrix Inverse(Matrix m)
        {
            if (!m.IsSquare)
                throw new InvalidMatrixDimensionsException("Inverse requires a Matrix to be square.");

            int n = m.Rows;
            Matrix result = new Matrix(m);

            double determinant = Matrix.Decompose(m, out Matrix lum, out int[] perm);
            for (int i = 0; i < lum.Rows; i++)
                determinant *= lum[i, i];
            if (Math.Abs(determinant) < 1.0e-5)
                throw new NonInvertibleMatrixException("Unable to invert the supplied Matrix");

            // Matrix lum = Matrix.Decompose(m, out perm);

            double[] b = new double[n];
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                    if (i == perm[j])
                        b[j] = 1.0;
                    else
                        b[j] = 0.0;

                double[] x = Reduce(lum, b); // 
                for (int j = 0; j < n; ++j)
                    result[j, i] = x[j];
            }
            return result;
        }

        protected static double[] Reduce(Matrix luMatrix, double[] b) // helper
        {
            int n = luMatrix.Rows;
            double[] x = new double[n];

            for (int i = 0; i < n; ++i)
                x[i] = b[i];

            for (int i = 1; i < n; ++i)
            {
                double sum = x[i];
                for (int j = 0; j < i; ++j)
                    sum -= luMatrix[i, j] * x[j];
                x[i] = sum;
            }

            x[n - 1] /= luMatrix[n - 1, n - 1];
            for (int i = n - 2; i >= 0; --i)
            {
                double sum = x[i];
                for (int j = i + 1; j < n; ++j)
                    sum -= luMatrix[i, j] * x[j];
                x[i] = sum / luMatrix[i, i];
            }

            return x;
        }

    }
}

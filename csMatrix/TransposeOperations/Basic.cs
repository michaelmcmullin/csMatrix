using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix.TransposeOperations
{
    public class Basic : IMatrixTransposeOperations
    {
        public Matrix MultiplyByTranspose(Matrix m1, Matrix m2)
        {
            throw new NotImplementedException();
        }

        public Matrix MultiplyByTranspose(Matrix m)
        {
            throw new NotImplementedException();
        }

        public Matrix MultiplyTransposeBy(Matrix m1, Matrix m2)
        {
            throw new NotImplementedException();
        }

        public Matrix MultiplyTransposeBy(Matrix m1)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the transposed version of a Matrix (swap rows and columns)
        /// </summary>
        /// <param name="m">The Matrix to transpose.</param>
        public Matrix Transpose(Matrix m)
        {
            Matrix t;

            if (m.IsTransposed)
            {
                t = new Matrix(m);
                t.IsTransposed = false;
            }
            else
            {
                t = new Matrix(m.Columns, m.Rows);
                for (int index = 0; index < m.Size; index++)
                {
                    int i = index / m.Rows;
                    int j = index % m.Rows;
                    t[index] = m[(m.Columns * j) + i];
                }
            }
            return t;
        }
    }
}

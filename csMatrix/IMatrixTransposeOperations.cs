using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix
{
    public interface IMatrixTransposeOperations
    {
        Matrix Transpose(Matrix m);
        Matrix MultiplyByTranspose(Matrix m1, Matrix m2);
        Matrix MultiplyByTranspose(Matrix m);
        Matrix MultiplyTransposeBy(Matrix m1, Matrix m2);
        Matrix MultiplyTransposeBy(Matrix m1);
    }
}

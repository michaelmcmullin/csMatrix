using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix
{
    /// <summary>
    /// Methods to populate a Matrix in different ways.
    /// </summary>
    public interface IMatrixPopulate
    {
        void Number(Matrix m, double number);
        void Ones(Matrix m);
        void Zeros(Matrix m);
        void Rand(Matrix m, int seed);
        void Identity(Matrix m);
    }
}

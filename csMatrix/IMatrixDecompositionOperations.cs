using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix
{
    public interface IMatrixDecompositionOperations
    {
        int Decompose(Matrix m, out Matrix decomposed, out int[] perm);
    }
}

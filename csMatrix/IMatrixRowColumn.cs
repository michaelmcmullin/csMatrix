using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix
{
    /// <summary>
    /// General Matrix operations.
    /// </summary>
    public interface IMatrixRowColumn
    {
        void SwapColumns(Matrix m, int column1, int column2);
        void SwapRows(Matrix m, int row1, int row2);
    }
}

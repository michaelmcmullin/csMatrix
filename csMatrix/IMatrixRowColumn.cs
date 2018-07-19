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
        Matrix AddColumns(Matrix m, int column, int count, double value);
        Matrix AddRows(Matrix m, int row, int count, double value);
        Matrix GetRows(Matrix m, int ro1, int count);
        Matrix GetColumns(Matrix m, int column, int count);
        Matrix RemoveRows(Matrix m, int row, int count);
        Matrix RemoveColumns(Matrix m, int column, int count);
    }
}

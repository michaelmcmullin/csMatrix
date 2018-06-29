using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix.RowColumnOperations
{
    public class Basic : IMatrixRowColumn
    {
        /// <summary>
        /// Swap two columns in a given Matrix.
        /// </summary>
        /// <param name="m">The Matrix to operate on.</param>
        /// <param name="column1">The first column to swap.</param>
        /// <param name="column2">The second column to swap.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when called with non-existent columns.</exception>
        public void SwapColumns(Matrix m, int column1, int column2)
        {
            if (column1 >= m.Columns || column2 >= m.Columns)
                throw new IndexOutOfRangeException("SwapColumns method called with non-existent columns.");

            if (column1 == column2) return;

            int indexColumn1 = column1, indexColumn2 = column2;
            double tmp;
            for (int i = 0; i < m.Rows; i++)
            {
                tmp = m[indexColumn1];
                m[indexColumn1] = m[indexColumn2];
                m[indexColumn2] = tmp;
                indexColumn1 += m.Columns;
                indexColumn2 += m.Columns;
            }
        }

        /// <summary>
        /// Swap two rows in a given Matrix.
        /// </summary>
        /// <param name="m">The Matrix to operate on.</param>
        /// <param name="row1">The first row to swap.</param>
        /// <param name="row2">The second row to swap.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when called with non-existent rows.</exception>
        public void SwapRows(Matrix m, int row1, int row2)
        {
            if (row1 >= m.Rows || row2 >= m.Rows)
                throw new IndexOutOfRangeException("SwapRow method called with non-existent rows.");

            if (row1 == row2) return;

            double[] tmp = new double[m.Columns];
            int indexRow1 = row1 * m.Columns;
            int indexRow2 = row2 * m.Columns;

            for (int i = 0; i < m.Columns; i++)
            {
                tmp[i] = m[indexRow1 + i];
                m[indexRow1 + i] = m[indexRow2 + i];
                m[indexRow2 + i] = tmp[i];
            }
        }
    }
}

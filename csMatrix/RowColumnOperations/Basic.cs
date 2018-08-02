using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix.RowColumnOperations
{
    public class Basic : IMatrixRowColumn
    {
        /// <summary>
        /// Inserts a number of additional columns to a Matrix, populating them with a given value.
        /// </summary>
        /// <param name="m">The Matrix to add columns to.</param>
        /// <param name="column">The column index to insert the new columns at.</param>
        /// <param name="count">The number of columns to insert.</param>
        /// <param name="value">The default value to add to the newly added columns.</param>
        /// <returns>A new Matrix with the additional columns added.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when attempting to add columns outside
        /// the range of valid column indices (i.e. 0 to m.Columns).</exception>
        public Matrix AddColumns(Matrix m, int column, int count, double value)
        {
            if (column > m.Columns || column < 0)
                throw new IndexOutOfRangeException($"Cannot add a column at index {column}.");
            if (count == 0)
                return new Matrix(m);

            Matrix result = new Matrix(m.Rows, m.Columns + count);
            int index = 0, resultIndex = 0, columnIndex = 0;

            while(resultIndex < result.Size)
            {
                columnIndex = 0;
                while (columnIndex < result.Columns)
                {
                    if (columnIndex == column)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            result[resultIndex++] = value;
                            columnIndex++;
                        }
                    }
                    else
                    {
                        result[resultIndex++] = m[index++];
                        columnIndex++;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Inserts a number of additional rows to a Matrix, populating them with a given value.
        /// </summary>
        /// <param name="m">The Matrix to add rows to.</param>
        /// <param name="row">The row index to insert the new columns at.</param>
        /// <param name="count">The number of rows to insert.</param>
        /// <param name="value">The default value to add to the newly added rows.</param>
        /// <returns>A new Matrix with the additional rows added.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when attempting to add rows outside
        /// the range of valid row indices (i.e. 0 to m.Rows).</exception>
        public Matrix AddRows(Matrix m, int row, int count, double value)
        {
            if (row > m.Rows || row < 0)
                throw new IndexOutOfRangeException($"Cannot add a row at index {row}.");
            if (count == 0)
                return new Matrix(m);

            Matrix result = new Matrix(m.Rows + count, m.Columns);
            int index = 0, resultIndex = 0, rowIndex = row * m.Columns;

            while (resultIndex < result.Size)
            {
                if (index == rowIndex)
                {
                    for (int i = 0; i < count * m.Columns; i++)
                    {
                        result[resultIndex++] = value;
                    }
                    rowIndex = -1;
                }
                else
                {
                    result[resultIndex++] = m[index++];
                }
            }

            return result;
        }

        /// <summary>
        /// Extract a number of columns from a Matrix, discarding the rest.
        /// </summary>
        /// <param name="m">The Matrix to extract columns from.</param>
        /// <param name="column">The column index to start extraction from.</param>
        /// <param name="count">The number of columns to extract.</param>
        /// <returns>A new Matrix containing the extracted columns.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when attempting to extract columns outside
        /// the range of valid column indices (i.e. 0 to m.Columns).</exception>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when attempting to extract zero
        /// columns.</exception>
        public Matrix ExtractColumns(Matrix m, int column, int count)
        {
            if (column > m.Columns - 1 || column < 0)
                throw new IndexOutOfRangeException($"Cannot extract a column from index {column}.");
            if ((column + count) > (m.Columns))
                throw new IndexOutOfRangeException("Number of columns requested is out of range.");
            if (count <= 0)
                throw new InvalidMatrixDimensionsException($"Cannot extract a Matrix with {count} columns.");

            Matrix result = new Matrix(m.Rows, count);
            int resultIndex = 0;
            for (int r = 0; r < m.Rows; r++)
            {
                for (int c = column; c < (column + count); c++)
                {
                    result[resultIndex++] = m[r, c];
                }
            }

            return result;
        }

        /// <summary>
        /// Extract a number of rows from this Matrix, discarding the rest.
        /// </summary>
        /// <param name="m">The Matrix to extract rows from.</param>
        /// <param name="row">The row index to start extraction from.</param>
        /// <param name="count">The number of rows to extract.</param>
        /// <returns>A new Matrix containing the extracted rows.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when attempting to remove rows outside
        /// the range of valid row indices (i.e. 0 to m.Rows).</exception>
        /// <exception cref="InvalidMatrixDimensionsException">Thrown when attempting to extract zero
        /// rows.</exception>
        public Matrix ExtractRows(Matrix m, int row, int count)
        {
            if (row > m.Rows - 1 || row < 0)
                throw new IndexOutOfRangeException($"Cannot extract a row from index {row}.");
            if ((row + count) > (m.Rows))
                throw new IndexOutOfRangeException("Number of rows requested is out of range.");
            if (count <= 0)
                throw new InvalidMatrixDimensionsException($"Cannot extract a Matrix with {count} rows.");
            Matrix result = new Matrix(count, m.Columns);
            int resultIndex = 0;
            for (int i = row * m.Columns; resultIndex < result.Size; i++)
            {
                result[resultIndex++] = m[i];
            }

            return result;
        }

        /// <summary>
        /// Removes a number of columns from a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to remove columns from.</param>
        /// <param name="column">The column index to start removal from.</param>
        /// <param name="count">The number of columns to remove.</param>
        /// <returns>A new Matrix with the specified columns removed.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when attempting to remove columns outside
        /// the range of valid column indices (i.e. 0 to m.Columns).</exception>
        public Matrix RemoveColumns(Matrix m, int column, int count)
        {
            if (column > m.Columns - 1 || column < 0)
                throw new IndexOutOfRangeException($"Cannot remove a column at index {column}.");
            Matrix result = new Matrix(m.Rows, m.Columns - count);
            int index = 0, resultIndex = 0, columnIndex;
            while (resultIndex < result.Size)
            {
                columnIndex = 0;
                while (columnIndex < m.Columns)
                {
                    if (columnIndex++ < column || columnIndex > column + count)
                    {
                        result[resultIndex++] = m[index++];
                    }
                    else
                    {
                        index++;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Removes a number of rows from a Matrix.
        /// </summary>
        /// <param name="m">The Matrix to remove rows from.</param>
        /// <param name="row">The row index to start removal from.</param>
        /// <param name="count">The number of rows to remove.</param>
        /// <returns>A new Matrix with the specified rows removed.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when attempting to remove rows outside
        /// the range of valid row indices (i.e. 0 to m.Rows).</exception>
        public Matrix RemoveRows(Matrix m, int row, int count)
        {
            if (row > m.Rows - 1 || row < 0)
                throw new IndexOutOfRangeException($"Cannot remove a row at index {row}.");
            Matrix result = new Matrix(m.Rows - count, m.Columns);


            return result;
        }

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

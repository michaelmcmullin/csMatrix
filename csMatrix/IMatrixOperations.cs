using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix
{
    public interface IMatrixOperations
    {
        Matrix Join(Matrix m1, Matrix m2, MatrixDimension dimension);
        Matrix ReduceDimension(Matrix m, MatrixDimension dimension, Func<double, double, double> op);
        Matrix StatisticalReduce(Matrix m, MatrixDimension dimension, Func<Matrix, double> op);
        Matrix Reshape(Matrix m, int startingIndex, int rows, int columns);
    }
}

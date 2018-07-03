using System;

namespace csMatrix
{
    /// <summary>
    /// Interface that Matrix classes use for arithmetic operations.
    /// </summary>
    public interface IMatrixArithmetic
    {
        Matrix Multiply(Matrix m1, Matrix m2);
        bool Equals(Matrix m1, Matrix m2);
        void ElementOperation(Matrix m, Func<double, double> op);
        void ElementOperation(Matrix m1, Matrix m2, Func<double, double, double> op);
        void ElementOperation(Matrix m, double scalar, Func<double, double, double> op);
    }
}

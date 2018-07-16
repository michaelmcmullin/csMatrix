using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix.Tests
{
    public class Setup
    {
        public static Matrix GetTestMatrix1()
        {
            return new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 } });
        }
        public static Matrix GetTestMatrix2()
        {
            return new Matrix(new double[,] { { 7.0, 8.0, 9.0 }, { 10.0, 11.0, 12.0 } });
        }
        public static Matrix GetTestMatrix3()
        {
            return new Matrix(new double[,] { { 1.0, 2.0 }, { 3.0, 4.0 }, { 5.0, 6.0 } });
        }
        public static Matrix GetTestMatrix4()
        {
            return new Matrix(new double[,] { { 22.0, 28.0 }, { 49.0, 64.0 } });
        }
    }
}

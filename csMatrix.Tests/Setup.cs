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
        public static Matrix GetTestMatrix5()
        {
            return new Matrix(new double[,] { { 1.0 }, { 2.0 }, { 3.0 } });
        }

        public static Matrix GetTestInvertibleMatrix1()
        {
            return new Matrix(new double[,]
            {
                { 1032.00, 35976.00, 30576.14, 30835.51, -259.37, 6.29, 0.93 },
                { 35976.00, 1273498.00, 1023772.21, 1030262.18, -6489.97, 333.19, 40.46 },
                { 30576.14, 1023772.21, 1055052.71, 1050596.12, 4456.59, -320.59, 0.24 },
                { 30835.51, 1030262.18, 1050596.12, 1096797.05, -46200.93, -376.67, -11.14 },
                { -259.37, -6489.97, 4456.59, -46200.93, 50657.53, 56.08, 11.38 },
                { 6.29, 333.19, -320.59, -376.67, 56.08, 22.21, 0.13 },
                { 0.93, 40.46, 0.24, -11.14, 11.38, 0.13, 1.36 }
            });
        }

        public static Matrix GetExpectedMatrix1Inverted()
        {
            return new Matrix(new double[,]
            {
                { 0.29583527221162214212, -0.0065816496446706526065, -0.0013081773334921961623, -0.00088831383747760571786, 0.00000000000000028505113281973511156, -0.018925198872888635633, -0.011731497256929492369 },
                { -0.0065816496446706526064, 0.00015031618269241636251, 0.000026276981527006512801, 0.000018776305061887772727, -0.0000000000000000046019962271218606893, 0.00030580833721523174576, 0.00014871168881879512158 },
                { -0.0013081773335085588031, 0.000026276981527007504572, 100.00002843505437335, -100.00001509728951642, -100.00000000025, 0.00013085131001986273671, -0.000028366026470284040671 },
                { -0.00088831383746124307714, 0.000018776305061886780959, -100.00001509728951643, 100.00002275920876377, 100.00000000025, 0.00013664293682569307004, 0.00022488020174614191185 },
                { 0.000000000000016647692478107772367, -0.0000000000000000055937770831794329396, -100.00000000025, 100.00000000025, 100.00000000025, -0.00000000000000090013287543843381729, -0.00000000000000044731916030951319242 },
                { -0.018925198872888635634, 0.00030580833721523174577, 0.0001308513100197126505, 0.00013664293682584315624, -0.00000000000000075004662292549528607, 0.050002039751465799055, 0.00016026651633183682664 },
                { -0.011731497256929492373, 0.00014871168881879512165, -0.00002836602647065488335, 0.00022488020174651275456, -0.000000000000000076476391580905715319, 0.00016026651633183682687, 0.74072393835736971942 }
            });
        }

        public static Matrix GetTestMatrix1Transposed()
        {
            return new Matrix(new double[,] { { 1.0, 4.0 }, { 2.0, 5.0 }, { 3.0, 6.0 } });
        }

        public static IEnumerable<object[]> GetIMatrixArithmetic
        {
            get
            {
                return new[]
                {
                    new IMatrixArithmetic[] { new Arithmetic.Basic() },
                    new IMatrixArithmetic[] { new Arithmetic.ParallelOperations() }
                };
            }
        }

        public static IEnumerable<object[]> GetIMatrixTranspose
        {
            get
            {
                return new[]
                {
                    new IMatrixTransposeOperations[] { new TransposeOperations.Basic() },
                    new IMatrixTransposeOperations[] { new TransposeOperations.ParallelOperations() }
                };
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix.Tests
{
    public static class Extensions
    {
        public static bool NearlyEqual(this Matrix a, Matrix b, double tolerance)
        {
            if (a.Size != b.Size)
            {
                return false;
            }

            for (int i = 0; i < a.Size; i++)
            {
                if (Math.Abs(a.Data[i] - b.Data[i]) > tolerance)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

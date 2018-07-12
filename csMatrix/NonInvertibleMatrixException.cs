using System;
using System.Collections.Generic;
using System.Text;

namespace csMatrix
{
    public class NonInvertibleMatrixException : InvalidOperationException
    {
        public NonInvertibleMatrixException()
        {
        }

        public NonInvertibleMatrixException(string message)
            : base(message)
        {
        }

        public NonInvertibleMatrixException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

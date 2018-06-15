using System;

namespace csMatrix
{
    /// <summary>
    /// Custom exception for Matrix operations using incorrect dimensions.
    /// </summary>
    public class InvalidMatrixDimensionsException : InvalidOperationException
    {
        public InvalidMatrixDimensionsException()
        {
        }

        public InvalidMatrixDimensionsException(string message)
            : base(message)
        {
        }

        public InvalidMatrixDimensionsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

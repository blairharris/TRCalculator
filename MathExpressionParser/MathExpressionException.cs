using System;

namespace MathExpressionParser
{
    public class MathExpressionException : ArgumentException
    {
        public MathExpressionException(string message) : base(message)
        {
        }
    }
}

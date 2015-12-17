using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExpressionEvaluator
{
    internal interface IPostfixConverter
    {
        string InfixToPostfix(string infixMathExpression);
    }
}

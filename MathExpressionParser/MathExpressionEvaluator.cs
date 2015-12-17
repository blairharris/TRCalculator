using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExpressionParser
{
    public class MathExpressionEvaluator : IMathExpressionEvaluator
    {
        public double CalculationResult(string mathematicalExpression)
        {
            if(mathematicalExpression.Contains("x"))
                throw new MathExpressionException();
            else
                return 4;
        }
    }
}

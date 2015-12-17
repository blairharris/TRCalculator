using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExpressionEvaluator
{
    public interface IMathExpressionEvaluator
    {
        double CalculationResult(string mathematicalExpression);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExpressionParser
{
    public class MathExpressionEvaluator : IMathExpressionEvaluator
    {
        private readonly IPostfixConverter _converter;
        private readonly IPostFixEvaluator _evaluator;

        public MathExpressionEvaluator(IPostfixConverter converter, IPostFixEvaluator evaluator)
        {
            this._converter = converter;
            this._evaluator = evaluator;
        }

        public int CalculationResult(string mathematicalExpression)
        {
            var postFixExpression = _converter.InfixToPostfix(mathematicalExpression);
            var result = _evaluator.CalculationResult(postFixExpression);

            return result;
        }
    }
}

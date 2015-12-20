namespace MathExpressionParser
{
    public class MathExpressionEvaluator : IMathExpressionEvaluator
    {
        private readonly IPostfixConverter _converter;
        private readonly IPostFixEvaluator _evaluator;

        public MathExpressionEvaluator(IPostfixConverter converter, IPostFixEvaluator evaluator)
        {
            _converter = converter;
            _evaluator = evaluator;
        }

        public int CalculationResult(string mathematicalExpression)
        {
            var postFixExpression = _converter.InfixToPostfix(mathematicalExpression);
            var result = _evaluator.CalculationResult(postFixExpression);

            return result;
        }
    }
}

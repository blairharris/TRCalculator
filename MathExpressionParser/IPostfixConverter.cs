namespace MathExpressionParser
{
    public interface IPostfixConverter
    {
        string InfixToPostfix(string infixMathExpression);
    }
}

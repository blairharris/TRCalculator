using System.Linq;

namespace MathExpressionParser
{
    public class Token
    {
        private static readonly string SupportedOperators = @"+-*/" + MathOperator.UnaryMinusAliasSymbol;
        private const string SupportedParenthesis = "()";
        private const string SupportedOperands = "0123456789";


        public char Symbol { get; }

        public Token(char c)
        {
            Symbol = c;
            Validate();
        }

        private void Validate()
        {
            string validCharacters = SupportedOperators + SupportedParenthesis + SupportedOperands;
            if (validCharacters.Contains(Symbol) == false)
                throw new MathExpressionException($"Unsupported character '{Symbol}' found");
        }

        public bool IsNumber() => char.IsNumber(Symbol);
        public bool IsOperator() => SupportedOperators.Contains(Symbol);
        public bool IsUnaryMinus() => Symbol == MathOperator.UnaryMinusAliasSymbol;
        public bool IsLeftParenthesis() => Symbol == '(';
        public bool IsRightParenthesis() => Symbol == ')';

        public bool DetermineIfUnaryMinus(char prev)
        {
            return Symbol == '-' && (prev == char.MinValue || SupportedOperators.Contains(prev));
        }
        public bool IsParenthesis() => Symbol == '(' || Symbol == ')';
    }
}

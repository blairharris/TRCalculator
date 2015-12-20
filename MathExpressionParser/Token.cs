using System.Linq;

namespace MathExpressionParser
{
    public class Token
    {
        private const string SupportedOperators = @"+-*/#";
        private const string SupportedParenthesis = "()";
        private const string SupportedOperands = "0123456789";


        public const char UnaryMinus = '#';
        public char Symbol { get; }

        public Token(char c)
        {
            Symbol = c;
            Validate();
        }

        private void Validate()
        {
            const string validCharacters = SupportedOperators + SupportedParenthesis + SupportedOperands;
            if (validCharacters.Contains(Symbol) == false)
                throw new MathExpressionException("Unsupported token character");
        }

        public bool IsNumber() => char.IsNumber(Symbol);
        public bool IsOperator() => SupportedOperators.Contains(Symbol);
        public bool IsUnaryMinus() => Symbol == UnaryMinus;
        public bool IsLeftParenthesis() => Symbol == '(';
        public bool IsRightParenthesis() => Symbol == ')';

        public static bool IsNumber(char c) => char.IsNumber(c);
        public static bool IsUnaryMinus(char prev, char curr)
        {
            return curr == '-' && (prev == char.MinValue || SupportedOperators.Contains(prev));
        }
        public static bool IsOperator(char c) => SupportedOperators.Contains(c);
        public static bool IsParenthesis(char c) => c == '(' || c == ')';
    }
}

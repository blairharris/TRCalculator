using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MathExpressionParser
{
    public class Token
    {
        private const char UnaryMinus = '#';
        private const string SupportedOperators = @"+-*/#";
        private const string SupportedParenthesis = "()";
        private const string SupportedOperands = "0123456789";
        public char Symbol { get; }

        public Token(char c)
        {
            this.Symbol = c;
            Validate();
        }

        private void Validate()
        {
            string validCharacters = SupportedOperators + SupportedParenthesis + SupportedOperands;
            if (validCharacters.Contains(Symbol) == false)
                throw new MathExpressionException("Unsupported token character");
        }

        public bool IsUnaryMinus(char previous)
        {
            return this.Symbol == '-' && (previous == Char.MinValue || SupportedOperators.Contains(previous));
        }
        public bool IsNumber() => char.IsNumber(Symbol);
        public bool IsOperator() => SupportedOperators.Contains(Symbol);
        public bool IsUnaryMinus() => Symbol == UnaryMinus;
        public bool IsLeftParenthesis() => Symbol == '(';
        public bool IsRightParenthesis() => Symbol == ')';
    }
}

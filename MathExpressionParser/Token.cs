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
        private readonly string _supportedOperators = @"+-*/#";
        private readonly string _supportedParenthesis = "()";
        private readonly string _supportedOperands = "0123456789";
        public char Symbol { get; }

        public Token(char c)
        {
            this.Symbol = c;
            Validate();
        }

        private void Validate()
        {
            string validCharacters = _supportedOperators + _supportedParenthesis + _supportedOperands;
            if (validCharacters.Contains(Symbol) == false)
                throw new MathExpressionException("Unsupported token character");
        }

        public bool IsUnaryMinus(char previous)
        {
            return this.Symbol == '-' && (previous == Char.MinValue || _supportedOperators.Contains(previous));
        }
        public bool IsNumber() => char.IsNumber(Symbol);
        public bool IsOperator() => _supportedOperators.Contains(Symbol);
        public bool IsUnaryMinus() => Symbol == '#';
        public bool IsLeftParenthesis() => Symbol == '(';
        public bool IsRightParenthesis() => Symbol == ')';
    }
}

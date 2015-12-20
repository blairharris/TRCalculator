using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExpressionParser
{
    public class PostFixConverter : IPostfixConverter
    {
        private Stack<Token> _operatorStack;
        private StringBuilder _output;

        /// <summary>
        /// Takes an Infix math expression e.g. 2+2 and converts to postFix/reverse-polish-notation e.g. 22+
        /// See https://en.wikipedia.org/wiki/Shunting-yard_algorithm
        /// </summary>
        /// <param name="infixMathExpression"></param>
        /// <returns>Expression converted to PostFix notation</returns>
        public string InfixToPostfix(string infixMathExpression)
        {
            _operatorStack = new Stack<Token>();
            _output = new StringBuilder();

            string infixMathExpressionWithUnaryMinusReplacement = ReplaceUnaryMinusWithHashSymbol(infixMathExpression);

            foreach (var c in infixMathExpressionWithUnaryMinusReplacement)
            {
                var token = new Token(c);

                if (token.IsNumber())
                {
                    AddToOutput(token);

                    if (ThereIsAUnaryMinusAtTheTopOfTheStack())
                        PopStackAndAddToOutput();
                }
                else if (token.IsOperator())
                {
                    var currentOperator = new MathOperator(token.Symbol);

                    while (ThereIsAMathOperatorAtTheTopOfTheStack())
                    {
                        if (currentOperator.IsLeftAssociativeAndPrecendenceIsLessThanOrEqualTo(OperatorAtTopOfStack) ||
                            currentOperator.IsRightAssociativeAndPrecendenceIsGreaterThan(OperatorAtTopOfStack))
                        {
                            PopStackAndAddToOutput(); ;
                        }
                        else
                        {
                            break;
                        }
                    }
                    
                    _operatorStack.Push(token);
                }
                else if (token.IsLeftParenthesis())
                {
                    _operatorStack.Push(token);
                }
                else if (token.IsRightParenthesis())
                {
                    PopFromStackUntilLeftParenthesisFound();
                }
            }

            while (_operatorStack.Count > 0)
            {
                PopStackAndAddToOutput();
            }

            return _output.ToString();
        }



        public string ReplaceUnaryMinusWithHashSymbol(string infixMathExpression)
        {
            var output = new StringBuilder();

            char prevChar = Char.MinValue;
            foreach(char c in infixMathExpression)
            {
                var token = new Token(c);
                if (token.IsUnaryMinus(prevChar))
                    output.Append('#');
                else
                    output.Append(c);

                prevChar = c;
            }

            return output.ToString();
        }

        private void PopStackAndAddToOutput()
        {
            _output.Append(_operatorStack.Pop().Symbol);
        }

        private void AddToOutput(Token token)
        {
            _output.Append(token.Symbol);
        }

        private void PopFromStackUntilLeftParenthesisFound()
        {
            while (_operatorStack.Count > 0)
            {
                if (_operatorStack.Peek().IsLeftParenthesis() == false)
                    PopStackAndAddToOutput();
                else
                {
                    _operatorStack.Pop();
                    break;
                }
            }
        }

        private bool ThereIsAUnaryMinusAtTheTopOfTheStack()
        {
            return _operatorStack.Count == 0 ? false : _operatorStack.Peek().Symbol == '#';
        }

        private bool ThereIsAMathOperatorAtTheTopOfTheStack()
        {
            return _operatorStack.Count == 0 ? false : _operatorStack.Peek().IsOperator();
        }

        private MathOperator OperatorAtTopOfStack => new MathOperator(_operatorStack.Peek().Symbol);
    }
}

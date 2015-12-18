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

            foreach (var c in infixMathExpression)
            {
                var token = new Token(c);

                if (token.IsNumber())
                {
                    _output.Append(token.Symbol);
                }
                else if (token.IsOperator())
                {
                    var currentOperator = new MathOperator(token.Symbol);
                    if (_operatorStack.Count > 0)
                    {
                        while (ThereIsAMathOperatorAtTheTopOfTheStack())
                        {
                            if (currentOperator.IsLeftAssociativeAndPrecendenceIsLessThanOrEqualTo(OperatorAtTopOfStack) ||
                                currentOperator.IsRightAssociativeAndPrecendenceIsGreaterThan(OperatorAtTopOfStack))
                            {
                                _output.Append(_operatorStack.Pop().Symbol);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    _operatorStack.Push(token);
                    if (_output.Length == 0 && token.Symbol == '-')
                    {
                        _output.Append(_operatorStack.Pop().Symbol);
                    }
                }
                else if (token.IsLeftParenthesis())
                {
                    _operatorStack.Push(token);
                }
                else if (token.IsRightParenthesis())
                {
                    PopFromStackUntilLeftParenthesisFound();
                }
                else
                {
                    throw new MathExpressionException();
                }

            }

            while (_operatorStack.Count > 0)
            {
                _output.Append(_operatorStack.Pop().Symbol);
            }

            return _output.ToString();
        }

        private void PopFromStackUntilLeftParenthesisFound()
        {
            while (_operatorStack.Count > 0)
            {
                if (_operatorStack.Peek().IsLeftParenthesis() == false)
                    _output.Append(_operatorStack.Pop().Symbol);
                else
                {
                    _operatorStack.Pop();
                    break;
                }
            }
        }

        private bool ThereIsAMathOperatorAtTheTopOfTheStack()
        {
            if (_operatorStack.Count == 0)
                return false;
            else
            {
                return _operatorStack.Peek().IsOperator();
            }
        }

        private MathOperator OperatorAtTopOfStack => new MathOperator(_operatorStack.Peek().Symbol);
    }
}

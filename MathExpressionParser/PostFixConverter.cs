using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExpressionParser
{
    public class PostFixConverter : IPostfixConverter
    {
        private readonly string _supportedOperators = @"+-*/";
        private Stack<char> _operatorStack;

        /// <summary>
        /// Takes an Infix math expression e.g. 2+2 and converts to postFix/reverse-polish-notation e.g. 22+
        /// See https://en.wikipedia.org/wiki/Shunting-yard_algorithm
        /// </summary>
        /// <param name="infixMathExpression"></param>
        /// <returns>Expression converted to PostFix notation</returns>
        public string InfixToPostfix(string infixMathExpression)
        {
            _operatorStack = new Stack<char>();
            var postFixExpression = new StringBuilder(); 

            foreach (var c in infixMathExpression)
            {
                if (IsNumber(c))
                {
                    postFixExpression.Append(c);
                }
                else if (IsOperator(c))
                {


                    var currentOperator = new MathOperator(c);
                    if (_operatorStack.Count > 0)
                    {
                        while (ThereIsAMathOperatorAtTheTopOfTheStack())
                        {
                            if (currentOperator.IsLeftAssociativeAndPrecendenceIsLessThanOfEqualTo(OperatorAtTopOfStack) ||
                                currentOperator.IsRightAssociativeAndPrecendenceIsGreaterThan(OperatorAtTopOfStack))
                            {
                                postFixExpression.Append(_operatorStack.Pop());
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    _operatorStack.Push(c);
                    if (postFixExpression.Length == 0 && c == '-')
                    {
                        postFixExpression.Append(_operatorStack.Pop());
                    }
                }
                else if (c == '(')
                {
                    _operatorStack.Push(c);
                }
                else if (c == ')')
                {
                    PopFromStackUntilLeftParenthesisFound(postFixExpression);
                }

            }

            while (_operatorStack.Count > 0)
            {
                postFixExpression.Append(_operatorStack.Pop());
            }

            return postFixExpression.ToString();
        }

        private void PopFromStackUntilLeftParenthesisFound(StringBuilder postFixExpression)
        {
            while (_operatorStack.Count > 0)
            {
                if (_operatorStack.Peek() != '(')
                    postFixExpression.Append(_operatorStack.Pop());
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
                return IsOperator(_operatorStack.Peek());
            }
        }

        private MathOperator OperatorAtTopOfStack => new MathOperator(_operatorStack.Peek());

        private bool IsOperator(char c) => _supportedOperators.Contains(c);

        private bool IsNumber(char c) => char.IsNumber(c);
    }
}

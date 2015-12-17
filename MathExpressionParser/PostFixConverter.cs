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
        private Stack<char> OperatorStack; 

        public string InfixToPostfix(string infixMathExpression)
        {
            OperatorStack = new Stack<char>();
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
                    if (OperatorStack.Count > 0)
                    {
                        while (ThereIsAMathOperatorAtTheTopOfTheStack())
                        {
                            if (currentOperator.IsLeftAssociativeAndPrecendenceIsLessThanOfEqualTo(OperatorAtTopOfStack) ||
                                currentOperator.IsRightAssociativeAndPrecendenceIsGreaterThan(OperatorAtTopOfStack))
                            {
                                postFixExpression.Append(OperatorStack.Pop());
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    OperatorStack.Push(c);
                    if (postFixExpression.Length == 0 && c == '-')
                    {
                        postFixExpression.Append(OperatorStack.Pop());
                    }
                }
                else if (c == '(')
                {
                    OperatorStack.Push(c);
                }
                else if (c == ')')
                {
                    while (OperatorStack.Count > 0)
                    {
                        if (OperatorStack.Peek() != '(')
                            postFixExpression.Append(OperatorStack.Pop());
                        else
                        {
                            OperatorStack.Pop();
                            break;
                        }
                    }
                    
                }

            }

            while (OperatorStack.Count > 0)
            {
                postFixExpression.Append(OperatorStack.Pop());
            }

            return postFixExpression.ToString();
        }

        private bool ThereIsAMathOperatorAtTheTopOfTheStack()
        {
            if (OperatorStack.Count == 0)
                return false;
            else
            {
                bool notBrackets = (OperatorStack.Peek() != '(' && OperatorStack.Peek() != ')');
                return notBrackets;
            }
        }

        private MathOperator OperatorAtTopOfStack
        {
            get { return new MathOperator(OperatorStack.Peek()); }
        }

        private bool IsOperator(char c)
        {
            return _supportedOperators.Contains(c);
        }

        private bool IsNumber(char c)
        {
            return char.IsNumber(c);
        }
    }
}

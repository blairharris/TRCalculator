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

        public string InfixToPostfix(string infixMathExpression)
        {
            var operatorStack = new Stack<char>();
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
                    if (operatorStack.Count > 0)
                    {
                        while (ThereIsAMathOperatorAtTheTopOfTheStack(operatorStack))
                        {
                            if(currentOperator.IsLeftAssociativeAndPrecendenceIsLessThanOfEqualTo(OperatorAtTopOfStack(operatorStack)) ||
                               currentOperator.IsRightAssociativeAndPrecendenceIsGreaterThan(OperatorAtTopOfStack(operatorStack)))
                                postFixExpression.Append(operatorStack.Pop());
                        }
                    }

                    operatorStack.Push(c);
                }
                else if (c == '(')
                {
                    operatorStack.Push(c);
                }
                else if (c == ')')
                {
                    while (operatorStack.Count > 0)
                    {
                        if (operatorStack.Peek() != '(')
                            postFixExpression.Append(operatorStack.Pop());
                        else
                        {
                            operatorStack.Pop();
                            break;
                        }
                    }
                    
                }

            }

            while (operatorStack.Count > 0)
            {
                postFixExpression.Append(operatorStack.Pop());
            }

            return postFixExpression.ToString();
        }

        private bool ThereIsAMathOperatorAtTheTopOfTheStack(Stack<char> operatorStack)
        {
            if (operatorStack.Count == 0)
                return false;
            else
            {
                bool notBrackets = (operatorStack.Peek() != '(' && operatorStack.Peek() != ')');
                return notBrackets;
            }
        }

        private bool OperatorStackContainsMoreThanParenthesis(Stack<char> operatorStack)
        {
            foreach (var c in operatorStack.ToArray())
            {
                if (c != '(' || c != ')') return true;
            }
            return false;
        }

        private MathOperator OperatorAtTopOfStack(Stack<char> operatorStack)
        {
            foreach (var c in operatorStack.ToArray())
            {
                if (c != '(' && c != ')') return new MathOperator(c);
            }

            throw new MathExpressionException();
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

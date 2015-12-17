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
                        var operatorAtTopOfStack = new MathOperator(operatorStack.Peek());

                        while (operatorStack.Count > 0 &&
                               currentOperator.IsLeftAssociativeAndPrecendenceIsLessThanOfEqualTo(operatorAtTopOfStack) ||
                               currentOperator.IsRightAssociativeAndPrecendenceIsGreaterThan(operatorAtTopOfStack))
                        {
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
                    
                }

            }

            while (operatorStack.Count > 0)
            {
                postFixExpression.Append(operatorStack.Pop());
            }

            return postFixExpression.ToString();
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

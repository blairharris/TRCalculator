using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExpressionParser
{
    public class PostFixEvaluator : IPostFixEvaluator
    {
        public int CalculationResult(string postFixMathExpression)
        {
            var stack = new Stack<string>();

            foreach (char c in postFixMathExpression)
            {
                var token = new Token(c);

                if( token.IsNumber() )
                {
                    stack.Push(token.Symbol.ToString());
                }
                else if(token.IsOperator())
                {
                    int leftOperand = Convert.ToInt32(stack.Pop());
                    int rightOperand = Convert.ToInt32(stack.Pop());

                    switch(token.Symbol)
                    {
                        case '+':
                            stack.Push( (leftOperand + rightOperand).ToString() );
                            break;

                        case '-':
                            stack.Push((leftOperand - rightOperand).ToString());
                            break;

                        case '*':
                            stack.Push((leftOperand * rightOperand).ToString());
                            break;

                        case '/':
                            stack.Push((leftOperand / rightOperand).ToString());
                            break;

                        default:
                            throw new MathExpressionException();
                    }
                }
                else
                {
                    throw new MathExpressionException();
                }
            }

            return Int32.Parse(stack.Peek());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExpressionParser
{
    public class PostFixEvaluator : IPostFixEvaluator
    {
        private readonly string _supportedOperators = @"+-*/";


        public int CalculationResult(string postFixMathExpression)
        {
            var stack = new Stack<string>();

            foreach (char c in postFixMathExpression)
            {
                if( IsNumber(c) )
                {
                    stack.Push(c.ToString());
                }
                else if( IsOperator(c))
                {
                    int leftOperand = Convert.ToInt32(stack.Pop());
                    int rightOperand = Convert.ToInt32(stack.Pop());
                    int result = 0;
                    switch(c)
                    {
                        case '+':
                            result = leftOperand + rightOperand;
                            break;

                        case '-':
                            result = leftOperand - rightOperand;
                            break;

                        case '*':
                            result = leftOperand * rightOperand;
                            break;

                        case '/':
                            result = leftOperand / rightOperand;
                            break;

                        default:
                            throw new MathExpressionException();
                    }

                    stack.Push(result.ToString());
                }
                else
                {
                    throw new MathExpressionException();
                }
            }

            return Int32.Parse(stack.Peek());
        }

        private bool IsOperator(char c) => _supportedOperators.Contains(c);

        private bool IsNumber(char c) => char.IsNumber(c);
    }
}

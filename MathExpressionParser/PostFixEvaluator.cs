using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExpressionParser
{
    public class PostFixEvaluator : IPostFixEvaluator
    {
        private Stack<String> _stack;

        public int CalculationResult(string postFixMathExpression)
        {
            _stack = new Stack<string>();

            foreach (char c in postFixMathExpression)
            {
                var token = new Token(c);

                if( token.IsNumber() )
                {
                    PushToStack(token);
                }
                else if(token.IsOperator())
                {
                    if (token.IsUnaryMinus())
                        PopStackMakeNegativeAndPushBackAgain();
                    else
                        PopStackTwiceApplyBinaryOpAndPushBackAgain(token);
                }
            }

            return Int32.Parse(_stack.Peek());
        }


        private void PushToStack(Token token)
        {
            _stack.Push(token.Symbol.ToString());
        }

        private void PopStackMakeNegativeAndPushBackAgain()
        {
            int leftOperand = Convert.ToInt32(_stack.Pop());
            _stack.Push((-leftOperand).ToString());
        }

        private void PopStackTwiceApplyBinaryOpAndPushBackAgain(Token token)
        {
            int leftOperand = Convert.ToInt32(_stack.Pop());
            int rightOperand = Convert.ToInt32(_stack.Pop());

            switch (token.Symbol)
            {
                case '+':
                    _stack.Push((leftOperand + rightOperand).ToString());
                    break;

                case '-':
                    _stack.Push((leftOperand - rightOperand).ToString());
                    break;

                case '*':
                    _stack.Push((leftOperand * rightOperand).ToString());
                    break;

                case '/':
                    _stack.Push((leftOperand / rightOperand).ToString());
                    break;


                default:
                    throw new MathExpressionException("Unsupported math operator");
            }
        }
    }
}

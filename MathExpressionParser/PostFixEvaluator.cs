using System;
using System.Collections.Generic;

namespace MathExpressionParser
{
    public class PostFixEvaluator : IPostFixEvaluator
    {
        private Stack<string> _stack;

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

            return int.Parse(_stack.Peek());
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

            var mathOperator = new MathOperator(token.Symbol);
            int result = mathOperator.ActOn(leftOperand, rightOperand);

            _stack.Push(result.ToString());
        }
    }
}

namespace MathExpressionParser
{
    public class MathOperator
    {
        public MathOperator(char c)
        {
            Symbol = c;

            switch (c)
            {
                case Token.UnaryMinus:
                    Precedence = 4;
                    Associativity = Associativity.Left;
                    break;

                case '*':
                case '/':
                    Precedence = 3;
                    Associativity = Associativity.Left;
                    break;

                case '+':
                case '-':
                    Precedence = 2;
                    Associativity = Associativity.Left;
                    break;


                default:
                    throw new MathExpressionException("Unsupported math operator symbol");
            }
        }

        public int ActOn(int leftOperand, int rightOperand)
        {
            int result;

            switch (Symbol)
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
                    throw new MathExpressionException("Unsupported math operator");
            }

            return result;
        }

        public char Symbol { get; set; }
        public int Precedence { get; set; }
        public Associativity Associativity { get; set; }

        public bool IsLeftAssociativeAndPrecendenceIsLessThanOrEqualTo(MathOperator other)
        {
            return Associativity == Associativity.Left && (Precedence <= other.Precedence);
        }

        internal bool IsRightAssociativeAndPrecendenceIsGreaterThan(MathOperator other)
        {
            return Associativity == Associativity.Right && (Precedence > other.Precedence);
        }
    }

    public enum Associativity
    {
        Right,
        Left
    }
}

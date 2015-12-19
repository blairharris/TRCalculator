using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathExpressionParser
{
    public class MathOperator
    {
        private const char UnaryMinus = '#';

        public MathOperator(char c)
        {
            Symbol = c;

            switch (c)
            {
                case UnaryMinus:
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

        public char Symbol { get; set; }
        public int Precedence { get; set; }
        public Associativity Associativity { get; set; }

        public bool IsLeftAssociativeAndPrecendenceIsLessThanOrEqualTo(MathOperator other)
        {
            return (this.Associativity == Associativity.Left && (this.Precedence <= other.Precedence));
        }

        internal bool IsRightAssociativeAndPrecendenceIsGreaterThan(MathOperator other)
        {
            return (this.Associativity == Associativity.Right && (this.Precedence > other.Precedence));
        }
    }

    public enum Associativity
    {
        Right,
        Left
    }
}

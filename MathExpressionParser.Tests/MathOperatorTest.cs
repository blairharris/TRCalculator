using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace MathExpressionParser.Tests
{
    [TestClass]
    public class MathOperatorTest
    {
        internal MathOperator Add = new MathOperator('+');
        internal MathOperator Subtract = new MathOperator('-');
        internal MathOperator Multiply = new MathOperator('*');
        internal MathOperator Divide = new MathOperator('/');

        [TestMethod]
        public void MulitplyPlusComparison()
        {
            Add.IsLeftAssociativeAndPrecendenceIsLessThanOrEqualTo(Multiply).Should().BeTrue();
        }
    }
}

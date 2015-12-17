using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathExpressionParser;

namespace MathExpressionParser.Tests
{
    [TestClass]
    public class PostFixConverterTest
    {
        readonly IPostfixConverter _converter = new PostFixConverter();

        [TestMethod]
        public void ValidExpression_TwoPlusTwo()
        {
            var result = _converter.InfixToPostfix("2+2");
            result.Should().Be("22+");
        }

        [TestMethod]
        public void ValidExpression_TwoPlusTwoTimes5()
        {
            var result = _converter.InfixToPostfix("2+2*5");
            result.Should().Be("225*+");
        }

        [TestMethod]
        public void ValidExpression_ComplexWithBrackets()
        {
            var result = _converter.InfixToPostfix("(5+5)*(3+2)");
            result.Should().Be("55+32+*");
        }
    }
}

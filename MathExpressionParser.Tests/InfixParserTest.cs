using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace MathExpressionParser.Tests
{
    [TestClass]
    public class InfixParserTest
    {
        readonly PostFixConverter _converter = new PostFixConverter();

        [TestMethod]
        public void InvalidExpression_DoubleDigitInt()
        {
            Action a = () => _converter.Parse("12+3");
            a.ShouldThrow<MathExpressionException>();
        }

        [TestMethod]
        public void UnaryMinus1()
        {
            string result = TokenListAsString(_converter.Parse("-1+2") );
            result.Should().Be("#1+2");
        }

        [TestMethod]
        public void UnaryMinus2()
        {
            string result = TokenListAsString(_converter.Parse("3+-4"));
            result.Should().Be("3+#4");
        }

        [TestMethod]
        public void UnaryMinus3()
        {
            string result = TokenListAsString(_converter.Parse("-1+2*-(1+3)"));
            result.Should().Be("#1+2*#(1+3)");
        }

        private string TokenListAsString(List<Token> tokenList)
        {
            var output = new StringBuilder();
            foreach (Token token in tokenList)
            {
                output.Append(token.Symbol);
            }
            return output.ToString();
        }
    }
}

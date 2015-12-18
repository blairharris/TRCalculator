using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace MathExpressionParser.Tests
{
    [TestClass]
    public class TokenTest
    {
        [TestMethod]
        public void Number()
        {
            var token = new Token('2');
            token.IsNumber().Should().BeTrue();
            token.IsOperator().Should().BeFalse();
            token.IsLeftParenthesis().Should().BeFalse();
            token.IsRightParenthesis().Should().BeFalse();
        }

        [TestMethod]
        public void Operator()
        {
            var token = new Token('-');
            token.IsNumber().Should().BeFalse();
            token.IsOperator().Should().BeTrue();
            token.IsLeftParenthesis().Should().BeFalse();
            token.IsRightParenthesis().Should().BeFalse();
        }

        [TestMethod]
        public void UnsupportedOperator()
        {
            Action construct = () => { var token = new Token('^'); };
            construct.ShouldThrow<MathExpressionException>();
        }

        [TestMethod]
        public void LeftParenthesis()
        {
            var token = new Token('(');
            token.IsNumber().Should().BeFalse();
            token.IsOperator().Should().BeFalse();
            token.IsLeftParenthesis().Should().BeTrue();
            token.IsRightParenthesis().Should().BeFalse();
        }

        [TestMethod]
        public void RightParenthesis()
        {
            var token = new Token(')');
            token.IsNumber().Should().BeFalse();
            token.IsOperator().Should().BeFalse();
            token.IsLeftParenthesis().Should().BeFalse();
            token.IsRightParenthesis().Should().BeTrue();
        }

        [TestMethod]
        public void UnsupportedCharacter()
        {
            Action construct = () => { var token = new Token('A'); };
            construct.ShouldThrow<MathExpressionException>();
        }
    }
}

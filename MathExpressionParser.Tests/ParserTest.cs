using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathExpressionParser;
using FluentAssertions;

namespace MathExpressionParser.Tests
{
    [TestClass]
    public class ParserTest
    {
        readonly IMathExpressionEvaluator _evaluator = new MathExpressionEvaluator();

        [TestMethod]
        public void ValidExpression_TwoPlusTwo()
        {
            double result = _evaluator.CalculationResult("2+2");
            result.Should().Be(4);
        }

        [TestMethod]
        public void InvalidExpression_CharacterAddition()
        {
            Action a = () => _evaluator.CalculationResult("2+x");
            a.ShouldThrow<MathExpressionException>();
        }

    }
}

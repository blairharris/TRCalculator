using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace MathExpressionParser.Tests
{
    [TestClass]
    public class PostFixEvaluatorTest
    {
        readonly IPostFixEvaluator _evaluator = new PostFixEvaluator();

        [TestMethod]
        public void TwoPlusTwo()
        {
            var result = _evaluator.CalculationResult("22+");
            result.Should().Be(4);
        }

        [TestMethod]
        public void TwoPlusTwoTimes5()
        {
            var result = _evaluator.CalculationResult("225*+");
            result.Should().Be(12);
        }
    }
}

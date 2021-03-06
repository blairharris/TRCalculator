﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var result = _evaluator.EvaluatePostFix("22+");
            result.Should().Be(4);
        }

        [TestMethod]
        public void TwoPlusTwoTimes5()
        {
            var result = _evaluator.EvaluatePostFix("225*+");
            result.Should().Be(12);
        }

        [TestMethod]
        public void TwoPlusTwoTimesMinus5()
        {
            var result = _evaluator.EvaluatePostFix("225#*+");
            result.Should().Be(-8);
        }

        [TestMethod]
        public void TwoPlusMinusTwoTimesMinus5()
        {
            var result = _evaluator.EvaluatePostFix("22#5#*+");
            result.Should().Be(12);
        }

        [TestMethod]
        public void MinusOnePlusFive()
        {
            var result = _evaluator.EvaluatePostFix("1#5+");
            result.Should().Be(4);
        }
    }
}

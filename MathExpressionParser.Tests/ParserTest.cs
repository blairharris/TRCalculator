using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathExpressionParser;
using FluentAssertions;
using Ninject;

namespace MathExpressionParser.Tests
{
    [TestClass]
    public class ParserTest
    {
        private IMathExpressionEvaluator _evaluator;

        [TestInitialize]
        public void StartUp()
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            _evaluator = kernel.Get<IMathExpressionEvaluator>();
        }

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

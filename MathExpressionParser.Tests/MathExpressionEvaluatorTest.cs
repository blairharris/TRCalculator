using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathExpressionParser;
using FluentAssertions;
using Ninject;

namespace MathExpressionParser.Tests
{
    [TestClass]
    public class MathExpressionEvaluatorTest
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
            int result = _evaluator.CalculationResult("2+2");
            result.Should().Be(4);
        }

        [TestMethod]
        public void InvalidExpression_CharacterAddition()
        {
            Action a = () => _evaluator.CalculationResult("2+x");
            a.ShouldThrow<MathExpressionException>();
        }

        [TestMethod]
        public void ValidExpression_SimpleBrackets1()
        {
            int result = _evaluator.CalculationResult("(1+2)*(2+2)");
            result.Should().Be(12);
        }

        [TestMethod]
        public void ValidExpression_NestedBrackets1()
        {
            int result = _evaluator.CalculationResult("((1+2)*(2+2))");
            result.Should().Be(12);
        }

        [TestMethod]
        public void ValidExpression_ExerciseExample()
        {
            int result = _evaluator.CalculationResult("(5+5)*(3+2)+1");
            result.Should().Be(51);
        }

    }
}

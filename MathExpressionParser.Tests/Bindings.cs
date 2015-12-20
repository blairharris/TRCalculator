using Ninject.Modules;

namespace MathExpressionParser.Tests
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IMathExpressionEvaluator>().To<MathExpressionEvaluator>();
            Bind<IPostfixConverter>().To<PostFixConverter>();
            Bind<IPostFixEvaluator>().To<PostFixEvaluator>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using MathExpressionParser;

namespace CalculatorUI
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

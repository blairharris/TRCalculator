using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MathExpressionParser;
using Ninject;
using System.Reflection;

namespace CalculatorUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IMathExpressionEvaluator _evaluator;

        public MainWindow()
        {
            InitializeComponent();

            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            _evaluator = kernel.Get<IMathExpressionEvaluator>();
        }

        private void evalueuateButton_Click(object sender, RoutedEventArgs e)
        {
            Evaluate();
        }

        private void inputTextBox_OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                Evaluate();
        }

        private void Evaluate()
        {
            try
            {
                int result = _evaluator.CalculationResult(inputTextBox.Text);
                outputTextBox.Text = result.ToString();
            }
            catch (MathExpressionException mathEx)
            {
                outputTextBox.Text = mathEx.Message;
            }
            catch (Exception generalEx)
            {
                outputTextBox.Text = generalEx.Message;
            }
        }
    }
}

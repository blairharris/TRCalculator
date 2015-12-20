using System;
using System.Windows;
using System.Windows.Input;
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
        private readonly IMathExpressionEvaluator _evaluator;

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
                int result = _evaluator.CalculationResult(InputTextBox.Text);
                OutputTextBox.Text = result.ToString();
            }
            catch (MathExpressionException mathEx)
            {
                OutputTextBox.Text = mathEx.Message;
            }
            catch (Exception generalEx)
            {
                OutputTextBox.Text = generalEx.Message;
            }
        }
    }
}

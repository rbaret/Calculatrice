using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace Calculatrice
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StringBuilder currentNumberSB = new StringBuilder();
        //private StringBuilder historyStringSB = new StringBuilder();
        private double operande1 = double.NaN;
        private double operande2 = double.NaN;
        private string curNumberString = "0";
        //private string history = "";
        private double result = 0;
        private bool currentNumberIsDecimal=false;
        private readonly string[] numericKeys = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        private enum Operator {none,add,sub,mult,div};
        private Operator currentOperator = Operator.none;
        private bool operatorIsArmed=false;
        private bool isFinalResult = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalcBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string buttonValue = btn.Content.ToString();
            ParseButton(buttonValue);
        }

        private void ParseButton(string buttonValue)
        {
            if (numericKeys.Contains(buttonValue))
            {
                BuildCurrentNumber(buttonValue);
            }
            else if (buttonValue == "." && !currentNumberIsDecimal)
            {
                currentNumberSB.Append(',');
                curNumberString = currentNumberSB.ToString();
                currentNumberIsDecimal = true;
            }
            else if (buttonValue == "=")
            {
                DoTheMaths();
                isFinalResult = true;
            }
            else if (buttonValue == "+")
            {
                textBlockOperator.Text = "+";
                ParseOperator(Operator.add);
            }
            else if (buttonValue == "-")
            {
                textBlockOperator.Text = "-";
                ParseOperator(Operator.sub);
            }
            else if (buttonValue == "*")
            {
                textBlockOperator.Text = "*";
                ParseOperator(Operator.mult);
            }
            else if (buttonValue == "/")
            {
                textBlockOperator.Text = "/";
                ParseOperator(Operator.div);
            }
        }

        private void DoTheMaths()
        {
            if (!String.IsNullOrEmpty(curNumberString))
            {
                operande2 = double.Parse(curNumberString,CultureInfo.InvariantCulture);
            }
            textBlockOp1.Text = operande1.ToString(CultureInfo.InvariantCulture);
            textBlockOp2.Text = operande2.ToString(CultureInfo.InvariantCulture);
            if (double.IsNaN(operande1) && !String.IsNullOrEmpty(curNumberString))
            {
                result = double.Parse(curNumberString,CultureInfo.InvariantCulture);
            }
            else if (!double.IsNaN(operande1) && currentOperator != Operator.none)
            {
                if (double.IsNaN(operande2))
                {
                    result = ExecOperation(operande1, operande1, currentOperator);
                }
                else
                {
                    result = ExecOperation(operande1, operande2, currentOperator);
                }
                textBoxResult.Text = result.ToString(CultureInfo.InvariantCulture);
            }
            else if (!double.IsNaN(operande2) && currentOperator != Operator.none && !String.IsNullOrEmpty(curNumberString))
            {
                operande1 = double.Parse(curNumberString,CultureInfo.InvariantCulture);
                result = ExecOperation(operande1, operande2, currentOperator);
                textBoxResult.Text = result.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                result = double.Parse(curNumberString,CultureInfo.InvariantCulture);
                textBoxResult.Text = result.ToString(CultureInfo.InvariantCulture);
            }
            operande1 = result;
            textBoxResult.Text = result.ToString(CultureInfo.InvariantCulture);
            operatorIsArmed = false;
            currentNumberSB.Clear();
            curNumberString = "";
        }

        private void ParseOperator(Operator curOperator)
        {
            isFinalResult = false;
            if (!operatorIsArmed)
            {
                if (double.IsNaN(operande1))
                {
                    operande1 = double.Parse(curNumberString,CultureInfo.InvariantCulture);
                    currentOperator = curOperator;
                    operatorIsArmed = true;
                    textBlockOp1.Text = operande1.ToString(CultureInfo.InvariantCulture);
                    textBlockOp2.Text = operande2.ToString(CultureInfo.InvariantCulture);
                }
                else if (!double.IsNaN(operande1) && currentOperator != Operator.none && !String.IsNullOrEmpty(curNumberString))
                {
                    operande2 = double.Parse(curNumberString,CultureInfo.InvariantCulture);
                    result = ExecOperation(operande1, operande2, currentOperator);
                    operande1 = result;
                    textBoxResult.Text = result.ToString(CultureInfo.InvariantCulture);
                    textBlockOp1.Text = operande1.ToString(CultureInfo.InvariantCulture);
                    textBlockOp2.Text = operande2.ToString(CultureInfo.InvariantCulture);
                    operatorIsArmed = true;
                }
                else
                {
                    currentOperator = curOperator;
                    operatorIsArmed = true;
                    textBlockOp1.Text = operande1.ToString(CultureInfo.InvariantCulture);
                    textBlockOp2.Text = operande2.ToString(CultureInfo.InvariantCulture);
                }
                currentNumberSB.Clear();
                curNumberString = "";
            }
            else
            {
                currentOperator = curOperator;
                operatorIsArmed = true;
            }
        }

        private double ExecOperation(double a, double b, Operator op)
        {
            switch (op)
            {
                case Operator.add:
                    {
                        return a + b;
                    }
                case Operator.sub:
                    {
                        return a - b;
                    }
                case Operator.mult:
                    {
                        return a * b;
                    }
                case Operator.div:
                    {
                        if (a == 0)
                        {
                            System.Windows.MessageBox.Show("You can't divide by zero");
                            return result;
                        }
                        else
                        {
                            return a / b;
                        }
                    }
                default:
                    return result;
            }
        }

        private void BuildCurrentNumber(String number)
        {
            operatorIsArmed = false;
            if (isFinalResult)
            {
                operande1 = double.NaN;
                isFinalResult = false;
            }
            if (!(currentNumberSB.Length == 1 && currentNumberSB.ToString() == "0"))
            {
                currentNumberSB.Append(number);
                curNumberString = currentNumberSB.ToString();
            }
            textBoxResult.Text = curNumberString;
            textBlockOp1.Text = operande1.ToString(CultureInfo.InvariantCulture);
            textBlockOp2.Text = operande2.ToString(CultureInfo.InvariantCulture);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            BuildInputStringFromKeyboard(e.Key);
        }

        private void BuildInputStringFromKeyboard(Key inputKey) {
            switch (inputKey) {
                case Key.NumPad0:
                    BuildCurrentNumber("0");
                    break;
                case Key.NumPad1:
                    BuildCurrentNumber("1");
                    break;
                case Key.NumPad2:
                    BuildCurrentNumber("2");
                    break;
                case Key.NumPad3:
                    BuildCurrentNumber("3");
                    break;
                case Key.NumPad4:
                    BuildCurrentNumber("4");
                    break;
                case Key.NumPad5:
                    BuildCurrentNumber("5");
                    break;
                case Key.NumPad6:
                    BuildCurrentNumber("6");
                    break;
                case Key.NumPad7:
                    BuildCurrentNumber("7");
                    break;
                case Key.NumPad8:
                    BuildCurrentNumber("8");
                    break;
                case Key.NumPad9:
                    BuildCurrentNumber("9");
                    break;
                case Key.OemComma:
                    currentNumberSB.Append(',');
                    currentNumberIsDecimal = true;
                    break;
                case Key.Decimal:
                    currentNumberSB.Append(',');
                    currentNumberIsDecimal = true;
                    break;
                case Key.OemPeriod:
                    currentNumberSB.Append(',');
                    currentNumberIsDecimal = true;
                    break;
                case Key.Add:
                    textBlockOperator.Text = "+";
                    ParseOperator(Operator.add);
                    break;
                case Key.Subtract:
                    textBlockOperator.Text = "-";
                    ParseOperator(Operator.sub);
                    break;
                case Key.Divide:
                    textBlockOperator.Text = "/";
                    ParseOperator(Operator.div);
                    break;
                case Key.Multiply:
                    textBlockOperator.Text = "*";
                    ParseOperator(Operator.mult);
                    break;
                case Key.Enter:
                    DoTheMaths();
                    textBoxResult.Text = result.ToString();
                    break;
            }
        }
    }
}

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

namespace Calculatrice
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StringBuilder currentNumberSB = new StringBuilder();
        private StringBuilder historyStringSB = new StringBuilder();
        private double operande1 = Double.NaN;
        private double operande2 = Double.NaN;
        private string curNumberString = "0";
        private string history = "";
        private double result = 0;
        private bool currentNumberIsDecimal=false;
        private readonly string[] numericKeys = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        private enum Operator {add,sub,mult,div,none};
        private Operator currentOperator = Operator.none;
        private bool operatorIsArmed=false;
        private bool isFinalResult = false;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void calcBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string buttonValue = btn.Content.ToString();
            parseButton(buttonValue);
        }

        private void parseButton(string buttonValue)
        {
            if (numericKeys.Contains(buttonValue))
            {
                buildCurrentNumber(buttonValue);
            }
            else if (buttonValue == "." && !currentNumberIsDecimal)
            {
                currentNumberSB.Append(',');
                curNumberString = currentNumberSB.ToString();
                currentNumberIsDecimal = true;
            }
            else if (buttonValue == "=")
            {
                doTheMaths();
                isFinalResult = true;
            }
            else if (buttonValue == "+")
            {
                textBlockOperator.Text = "+";
                parseOperator(Operator.add);
            }
            else if (buttonValue == "-")
            {
                textBlockOperator.Text = "-";
                parseOperator(Operator.sub);
            }
            else if (buttonValue == "*")
            {
                textBlockOperator.Text = "*";
                parseOperator(Operator.mult);
            }
            else if (buttonValue == "/")
            {
                textBlockOperator.Text = "/";
                parseOperator(Operator.div);
            }
        }
        private void doTheMaths()
        {
            if (!String.IsNullOrEmpty(curNumberString))
            {
                operande2 = Double.Parse(curNumberString);
            }
            textBlockOp1.Text = operande1.ToString();
            textBlockOp2.Text = operande2.ToString();
            if (Double.IsNaN(operande1) && !String.IsNullOrEmpty(curNumberString))
            {
                result = Double.Parse(curNumberString);
            }
            else if (!Double.IsNaN(operande1) && currentOperator != Operator.none)
            {
                if (Double.IsNaN(operande2))
                {
                    result = execOperation(operande1, operande1, currentOperator);
                }
                else
                {
                    result = execOperation(operande1, operande2, currentOperator);
                }
                textBoxResult.Text = result.ToString();
            }
            else if (!Double.IsNaN(operande2) && currentOperator != Operator.none && !String.IsNullOrEmpty(curNumberString))
            {
                operande1 = Double.Parse(curNumberString);
                result = execOperation(operande1, operande2, currentOperator);
                textBoxResult.Text = result.ToString();
            }
            else
            {
                result = Double.Parse(curNumberString);
                textBoxResult.Text = result.ToString();
            }
            operande1 = result;
            textBoxResult.Text = result.ToString();
            operatorIsArmed = false;
            currentNumberSB.Clear();
            curNumberString = "";
        }
        private void parseOperator(Operator curOperator)
        {
            isFinalResult = false;
            if (!operatorIsArmed)
            {
                if (double.IsNaN(operande1))
                {
                    operande1 = Double.Parse(curNumberString);
                    currentOperator = curOperator;
                    operatorIsArmed = true;
                    textBlockOp1.Text = operande1.ToString();
                    textBlockOp2.Text = operande2.ToString();
                }
                else if (!double.IsNaN(operande1) && currentOperator != Operator.none && !String.IsNullOrEmpty(curNumberString))
                {
                    operande2 = Double.Parse(curNumberString);
                    result = execOperation(operande1, operande2, currentOperator);
                    operande1 = result;
                    textBoxResult.Text = result.ToString();
                    textBlockOp1.Text = operande1.ToString();
                    textBlockOp2.Text = operande2.ToString();
                    operatorIsArmed = true;
                }
                else
                {
                    currentOperator = curOperator;
                    operatorIsArmed = true;
                    textBlockOp1.Text = operande1.ToString();
                    textBlockOp2.Text = operande2.ToString();
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
        private double execOperation(double a, double b, Operator op)
        {
            switch (op)
            {
                case Operator.add:
                    {
                        return (a + b);
                    }
                case Operator.sub:
                    {
                        return (a - b);
                    }
                case Operator.mult:
                    {
                        return (a * b);
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
                            return (a / b);
                        }
                    }
                default:
                    return result;
            }
        }
        private void buildCurrentNumber(String number)
        {
            operatorIsArmed = false;
            if (isFinalResult)
            {
                operande1 = Double.NaN;
                isFinalResult = false;
            }
            if (!(currentNumberSB.Length == 1 && currentNumberSB.ToString() == "0"))
            {
                currentNumberSB.Append(number);
                curNumberString = Double.Parse(currentNumberSB.ToString()).ToString();
            }
            textBoxResult.Text = curNumberString;
            textBlockOp1.Text = operande1.ToString();
            textBlockOp2.Text = operande2.ToString();
        }
        private void onKeyDown(object sender, KeyEventArgs e)
        {
            buildInputStringFromKeyboard(e.Key);
        }

        void buildInputStringFromKeyboard(Key inputKey) {
            switch (inputKey) {
                case Key.NumPad0:
                    buildCurrentNumber("0");
                    break;
                case Key.NumPad1:
                    buildCurrentNumber("1");
                    break;
                case Key.NumPad2:
                    buildCurrentNumber("2");
                    break;
                case Key.NumPad3:
                    buildCurrentNumber("3");
                    break;
                case Key.NumPad4:
                    buildCurrentNumber("4");
                    break;
                case Key.NumPad5:
                    buildCurrentNumber("5");
                    break;
                case Key.NumPad6:
                    buildCurrentNumber("6");
                    break;
                case Key.NumPad7:
                    buildCurrentNumber("7");
                    break;
                case Key.NumPad8:
                    buildCurrentNumber("8");
                    break;
                case Key.NumPad9:
                    buildCurrentNumber("9");
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
                    parseOperator(Operator.add);
                    break;
                case Key.Subtract:
                    textBlockOperator.Text = "-";
                    parseOperator(Operator.sub);
                    break;
                case Key.Divide:
                    textBlockOperator.Text = "/";
                    parseOperator(Operator.div);
                    break;
                case Key.Multiply:
                    textBlockOperator.Text = "*";
                    parseOperator(Operator.mult);
                    break;
                case Key.Enter:
                    doTheMaths();
                    textBoxResult.Text = result.ToString();
                    break;
            }
        }
    }
}

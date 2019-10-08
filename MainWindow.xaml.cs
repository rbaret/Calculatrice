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
        public MainWindow()
        {
            InitializeComponent();
        }
        private void calcBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string buttonValue = btn.Content.ToString();
            if (numericKeys.Contains(buttonValue))
            {
                operatorIsArmed = false;
                if (!((currentNumberSB.Length == 0) && (buttonValue == "0")))
                {
                    currentNumberSB.Append(buttonValue);
                    curNumberString = currentNumberSB.ToString();
                }
                textBoxResult.Text = curNumberString;
            }
            else if (buttonValue == "=")
            {
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
            }
            else if (buttonValue == "+" && !String.IsNullOrEmpty(curNumberString))
            {
                if (!operatorIsArmed)
                {
                    if (double.IsNaN(operande1))
                    {
                        operande1 = Double.Parse(curNumberString);
                        currentOperator = Operator.add;
                        operatorIsArmed = true;
                    }
                    else if (!double.IsNaN(operande1) && currentOperator != Operator.none)
                    {
                        operande2 = Double.Parse(curNumberString);
                        result = execOperation(operande1, operande2, currentOperator);
                        textBoxResult.Text = result.ToString();
                    }
                    else
                    {
                        currentOperator = Operator.add;
                        operatorIsArmed = true;
                    }
                    currentNumberSB.Clear();
                    curNumberString = "";
                }
                else
                {
                    currentOperator = Operator.add;
                    operatorIsArmed = true;
                }
                
            }
            else if (buttonValue == "-")
            {
                currentOperator = Operator.sub;
            }
            else if (buttonValue == "*")
            {
                currentOperator = Operator.mult;
            }
            else if (buttonValue == "/")
            {
                currentOperator = Operator.div;
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

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            buildInputStringFromKeyboard(e.Key);
        }

        void buildInputStringFromKeyboard(Key inputKey) {
            switch (inputKey) {
                case Key.NumPad0:
                    if (!String.IsNullOrEmpty(curNumberString))
                        currentNumberSB.Append('0');
                    break;
                case Key.NumPad1:
                    currentNumberSB.Append('1');
                    break;
                case Key.NumPad2:
                    currentNumberSB.Append('2');
                    break;
                case Key.NumPad3:
                    currentNumberSB.Append('3');
                    break;
                case Key.NumPad4:
                    currentNumberSB.Append('4');
                    break;
                case Key.NumPad5:
                    currentNumberSB.Append('5');
                    break;
                case Key.NumPad6:
                    currentNumberSB.Append('6');
                    break;
                case Key.NumPad7:
                    currentNumberSB.Append('7');
                    break;
                case Key.NumPad8:
                    currentNumberSB.Append('8');
                    break;
                case Key.NumPad9:
                    currentNumberSB.Append('9');
                    break;
                case Key.Separator:
                    currentNumberSB.Append('.');
                    currentNumberIsDecimal = true;
                    break;
            }
            curNumberString = currentNumberSB.ToString();
            textBoxResult.Text = curNumberString;
        }
            /*
            private void calcBtn_Click(object sender, RoutedEventArgs e)
            {
                Button btn = sender as Button;
                string buttonValue = btn.Content.ToString();
                if (numericKeys.Contains(buttonValue))
                {
                    currentNumber.Append(buttonValue);
                    curNumberString = currentNumber.ToString();
                    textBoxHistory.Text = history + curNumberString;
                }
                if (buttonValue == "=")
                {
                    history += currentNumber.ToString();
                    execOperation(parseCurNumberString(currentNumber.ToString()),currentOperator);
                    currentOperator = Operators.none;
                    currentNumberIsDecimal = false;
                    textBoxHistory.Text = history;
                    history.Append('\n');
                    textBoxResult.Text = result.ToString();
                    currentNumber.Clear();
                }
                if (buttonValue == "+")
                {
                    if (currentOperator!=Operators.none)
                    {
                        if (String.IsNullOrEmpty(currentNumber.ToString()))
                            currentNumber.Append(result.ToString());
                        String.Concat(history, currentNumber.ToString(),buttonValue);
                        execOperation(parseCurNumberString(currentNumber.ToString()), currentOperator);
                        currentNumberIsDecimal = false;
                        textBoxResult.Text = result.ToString();
                    }
                    else
                        currentOperator = Operators.add;
                    String.Concat(history,'+'); 
                    textBoxHistory.Text = history;
                }
                if (buttonValue == "-")
                {
                    if (currentOperator != Operators.none)
                    {
                        if (String.IsNullOrEmpty(currentNumber.ToString()))
                            currentNumber.Append(result.ToString());
                        String.Concat(history, currentNumber.ToString(),"-");
                        currentNumberIsDecimal = false;
                        execOperation(parseCurNumberString(currentNumber.ToString()), currentOperator);
                        textBoxResult.Text = result.ToString();
                    }
                    else
                        currentOperator = Operators.sub;
                    String.Concat(history, '-');
                    textBoxHistory.Text = history;
                }
                if (buttonValue == "*")
                {
                    if (currentOperator != Operators.none)
                    {
                        if (String.IsNullOrEmpty(currentNumber.ToString()))
                            currentNumber.Append(result.ToString());
                        String.Concat(history, currentNumber.ToString(), "*");
                        currentNumberIsDecimal = false;
                        execOperation(parseCurNumberString(currentNumber.ToString()), currentOperator);
                        textBoxResult.Text = result.ToString();
                    }
                    else
                        currentOperator = Operators.mult;
                    String.Concat(history, '*');
                    textBoxHistory.Text = history;
                }
                if (buttonValue == "/")
                {
                    if (currentOperator != Operators.none)
                    {
                        if (String.IsNullOrEmpty(currentNumber.ToString()))
                            currentNumber.Append(result.ToString());
                        String.Concat(history, currentNumber.ToString(),"/");
                        currentNumberIsDecimal = false;
                        execOperation(parseCurNumberString(currentNumber.ToString()), currentOperator);
                        textBoxResult.Text = result.ToString();
                    }
                    else
                        currentOperator = Operators.div;
                    String.Concat(history, '/');
                    textBoxHistory.Text = history;
                }
                if (buttonValue == ".")
                {
                    if (!currentNumberIsDecimal)
                    {
                        currentNumberIsDecimal = true;
                        currentNumber.Append(buttonValue);
                        curNumberString = currentNumber.ToString();
                    }

                }
            }
            */

    }
}

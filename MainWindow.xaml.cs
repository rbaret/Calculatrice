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
        private string curNumberString = "";
        private string history = "";
        private double result = 0;
        private bool currentNumberIsDecimal=false;
        private readonly string[] numericKeys = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        private enum Operator {add,sub,mult,div,none};
        private Operator currentOperator = Operator.none;
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
                currentNumberSB.Append(buttonValue);
                curNumberString = currentNumberSB.ToString();
                textBoxResult.Text = curNumberString;
            }
            if (buttonValue == "=")
            {
                if (Double.IsNaN(operande1) && !String.IsNullOrEmpty(curNumberString))
                {
                    result = Double.Parse(curNumberString);
                }
                else if(!Double.IsNaN(operande1) && currentOperator != Operator.none)
                {
                    if (Double.IsNaN(operande2))
                    {
                        result = execOperation(operande1, operande1, currentOperator);
                    }
                    else
                    {
                        result = execOperation(operande1, operande2, currentOperator);
                    }
                }
                else if (!Double.IsNaN(operande2) && currentOperator != Operator.none && !String.IsNullOrEmpty(curNumberString))
                {
                    operande1 = 
                    result = execOperation(operande1, operande1, currentOperator);
                }
                else
                    result = Double.Parse(curNumberString);
                operande1 = result;
            }
            if (buttonValue == "+" && !)
            {
                currentOperator = Operator.add;
            }
            if (buttonValue == "-")
            {
                currentOperator = Operator.sub;
            }
            if (buttonValue == "*")
            {
                currentOperator = Operator.mult;
            }
            if (buttonValue == "/")
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
            /*public double parseCurNumberString(string strToParse)
            {
                if (!String.IsNullOrEmpty(strToParse))
                    return double.Parse(strToParse);
                else
                    return (double)0.0;
            }

            private void onKeyDown(object sender, KeyEventArgs e)
            {
                buildInputStringFromKeyboard(e.Key);
            }

            public void buildInputStringFromKeyboard(Key inputKey) {
                switch (inputKey) {
                    case Key.NumPad0:
                        if (!String.IsNullOrEmpty(curNumberString))
                            currentNumber.Append('0');
                        break;
                    case Key.NumPad1:
                        currentNumber.Append('1');
                        break;
                    case Key.NumPad2:
                        currentNumber.Append('2');
                        break;
                    case Key.NumPad3:
                        currentNumber.Append('3');
                        break;
                    case Key.NumPad4:
                        currentNumber.Append('4');
                        break;
                    case Key.NumPad5:
                        currentNumber.Append('5');
                        break;
                    case Key.NumPad6:
                        currentNumber.Append('6');
                        break;
                    case Key.NumPad7:
                        currentNumber.Append('7');
                        break;
                    case Key.NumPad8:
                        currentNumber.Append('8');
                        break;
                    case Key.NumPad9:
                        currentNumber.Append('9');
                        break;
                    case Key.Separator:
                        currentNumber.Append('.');
                        currentNumberIsDecimal = true;
                        break;
                }
                curNumberString = currentNumber.ToString();
                textBoxHistory.Text = history + curNumberString;
            }

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
}

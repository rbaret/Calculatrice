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
        private StringBuilder currentNumber = new StringBuilder();
        private string validatedInputs = "";
        private string currentOperator = "";
        private string curNumberString = "";
        private double result = 0;
        private readonly string[] numericKeys = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public MainWindow()
        {
            InitializeComponent();
            textBoxResult.Text = "0";
        }

        public double parseCurNumberString(string strToParse)
        {
                return double.Parse(strToParse);
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            buildInputStringFromKeyboard(e.Key);
        }

        private void updateDisplay()
        {

        }
        public void buildInputStringFromKeyboard(Key inputKey) {
            switch (inputKey) {
                case Key.D0:
                    if (!String.IsNullOrEmpty(curNumberString))
                        currentNumber.Append('0');
                    break;
                case Key.D1:
                    currentNumber.Append('1');
                    break;
                case Key.D2:
                    currentNumber.Append('2');
                    break;
                case Key.D3:
                    currentNumber.Append('3');
                    break;
                case Key.D4:
                    currentNumber.Append('4');
                    break;
                case Key.D5:
                    currentNumber.Append('5');
                    break;
                case Key.D6:
                    currentNumber.Append('6');
                    break;
                case Key.D7:
                    currentNumber.Append('7');
                    break;
                case Key.D8:
                    currentNumber.Append('8');
                    break;
                case Key.D9:
                    currentNumber.Append('9');
                    break;
                case Key.Separator:
                    currentNumber.Append('.');
                    break;
            }
            curNumberString = currentNumber.ToString();

            foreach (char c in curNumberString)
            {
                textBoxHistory.Text.Append(c);
            }
        }

        private void calcBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string buttonValue = btn.Content.ToString();
            if (numericKeys.Contains(buttonValue))
            {
                currentNumber.Append(buttonValue);
                curNumberString = currentNumber.ToString();
                textBoxHistory.Text = curNumberString;
            }
            if (buttonValue == "=")
            {
                execOperation(result, parseCurNumberString(currentNumber.ToString()),currentOperator);
                currentOperator = "";
            }
            if (buttonValue == "+")
            {
                if (!String.IsNullOrEmpty(currentOperator))
                {
                    execOperation(result, parseCurNumberString(currentNumber.ToString()), currentOperator);
                }
                else
                    currentOperator = "add";
            }
            if (buttonValue == "-")
            {
                if (!String.IsNullOrEmpty(currentOperator))
                {
                    execOperation(result, parseCurNumberString(currentNumber.ToString()), currentOperator);
                }
                else
                    currentOperator = "sub";
            }
            if (buttonValue == "*")
            {
                if (!String.IsNullOrEmpty(currentOperator))
                {
                    execOperation(result, parseCurNumberString(currentNumber.ToString()), currentOperator);
                }
                else
                    currentOperator = "mult";
            }
            if (buttonValue == "/")
            {
                if (!String.IsNullOrEmpty(currentOperator))
                {
                    execOperation(result, parseCurNumberString(currentNumber.ToString()), currentOperator);
                }
                else
                    currentOperator = "div";
            }
        }

        private void execOperation(double result, double num, string op)
        {
            if (op=="add")
            {
                result += num;
                textBoxHistory.Text.Append('+');
                foreach(char c in curNumberString)
                {
                    textBoxHistory.Text.Append(c);
                }
                currentNumber.Clear();
            }
            else if (op=="sub")
            {
                result -= num;
                textBoxHistory.Text.Append('-');
                foreach (char c in curNumberString)
                {
                    textBoxHistory.Text.Append(c);
                }
                currentNumber.Clear();
            }
            else if (op == "mult")
            {
                result *= num;
                textBoxHistory.Text.Append('*');
                foreach (char c in curNumberString)
                {
                    textBoxHistory.Text.Append(c);
                }
                currentNumber.Clear();
            }
            else if (op == "div")
            {
                if (num == 0)
                {
                    System.Windows.MessageBox.Show("You can't divide by zero");
                }
                else
                {
                    result /= num;
                    textBoxHistory.Text.Append('+');
                    foreach (char c in curNumberString)
                    {
                        textBoxHistory.Text.Append(c);
                    }
                    currentNumber.Clear();
                }
            }
        }
    }
}

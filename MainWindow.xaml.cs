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
        private StringBuilder inputString = new StringBuilder();
        private string finalString;
        private ulong inputULong = 0;
        private double inputDouble = 0;
        private double result = 0;
        private bool isDecimal = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void parseInputString()
        {
            if (isDecimal)
            {
                inputDouble = double.Parse(finalString);
            }
            else
                inputULong = ulong.Parse(finalString);

        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            buildInputStringFromKeyboard(e.Key);
        }
        private void inputString_textChanged()
        {

        }
        private void updateDisplay()
        {

        }
        public void buildInputStringFromKeyboard(Key inputKey) {
            switch (inputKey) {
                case Key.D0:
                    if (!String.IsNullOrEmpty(finalString))
                        inputString.Append("0");
                    break;
                case Key.D1:
                    inputString.Append('1');
                    break;
                case Key.D2:
                    inputString.Append('2');
                    break;
                case Key.D3:
                    inputString.Append('3');
                    break;
                case Key.D4:
                    inputString.Append('4');
                    break;
                case Key.D5:
                    inputString.Append('5');
                    break;
                case Key.D6:
                    inputString.Append('6');
                    break;
                case Key.D7:
                    inputString.Append('7');
                    break;
                case Key.D8:
                    inputString.Append('8');
                    break;
                case Key.D9:
                    inputString.Append('9');
                    break;
            }
            finalString = inputString.ToString();
            textBoxHistory.Text = finalString;
        }
        public void buildInputStringFromButton(char buttonContent)
        {
            inputString.Append(buttonContent);
            finalString = inputString.ToString();
            textBoxHistory.Text = finalString;
        }
        private void button9_Click(object sender, RoutedEventArgs e)
        {
            buildInputStringFromButton('9');
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            buildInputStringFromButton('9');
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            buildInputStringFromButton('9');
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            buildInputStringFromButton('9');
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            buildInputStringFromButton('9');
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            buildInputStringFromButton('9');
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button0_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CalcPiAlgoritm;
using System.Threading;

namespace SyncCalcPi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void miFileExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            // Update statusbar
            sbiStatus.Content = "Calculating...";

            // New thread for calculating           
            Thread calcThread = new Thread(new ParameterizedThreadStart(CalcPi));
            calcThread.Start(int.Parse(tbxDigits.Text));          
        }

        void CalcPi(object rawdigits)
        {
            int digits = (int) rawdigits;

            StringBuilder pi = new StringBuilder("3", digits + 2);

            // Show progress
            ShowProgress(pi.ToString(), digits, 0);

            if (digits > 0)
            {
                pi.Append(".");

                for (int i = 0; i < digits; i += 9)
                {
                    int nineDigits = NineDigitsOfPi.StartingAt(i + 1);
                    int digitCount = Math.Min(digits - i, 9);
                    string ds = string.Format("{0:D9}", nineDigits);
                    pi.Append(ds.Substring(0, digitCount));

                    // Show progress
                    ShowProgress(pi.ToString(), digits, i + digitCount);
                }
            }
        }

        void ShowProgress(string pi, int totalDigits, int digitsSoFar)
        {
            // Display progress in UI
            Dispatcher.BeginInvoke(new Action(() => { tblkResults.Text = pi; }));
            Dispatcher.BeginInvoke(new Action(() => { progressBar.Maximum = totalDigits; }));
            Dispatcher.BeginInvoke(new Action(() => { progressBar.Value = digitsSoFar; }));
            Dispatcher.BeginInvoke(new Action(() => { progressBar.Visibility = Visibility.Visible; }));                                    

            if (digitsSoFar == totalDigits)
            {
                // Reset UI
                Dispatcher.BeginInvoke(new Action(() => { sbiStatus.Content = "Ready"; }));
                Dispatcher.BeginInvoke(new Action(() => { progressBar.Visibility = Visibility.Hidden; }));                                   
            }
        }

    }
}

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
using System.ComponentModel;

namespace SyncCalcPi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker bw = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();

            bw.DoWork += CalcPi;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            bw.WorkerReportsProgress = true;            
        }
        private void miFileExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            // Update statusbar
            sbiStatus.Content = "Calculating...";

            // Convert control's decimal Value to an integer

            bw.RunWorkerAsync(int.Parse(tbxDigits.Text));

            //CalcPi((int.Parse(this.tbxDigits.Text)));

            // Update statusbar
            sbiStatus.Content = "Ready";
        }

        void CalcPi(object rawdigits, DoWorkEventArgs args)
        {
            int digits = (int)args.Argument;
            StringBuilder pi = new StringBuilder("3", digits + 2);

            // Show progress
            //ShowProgress(pi.ToString(), digits, 0);
            bw.ReportProgress(0, pi);

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
                    //ShowProgress(pi.ToString(), digits, i + digitCount);
                    double progress = ((i + digitCount)/digits)*100;
                    //bw.ReportProgress((int)progress, pi);

                }
            }
        }

        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Running on a UI thread
            tblkResults.Text = e.UserState.ToString();
            progressBar.Maximum = 100;
            progressBar.Value = e.ProgressPercentage;
            progressBar.Visibility = Visibility.Visible;


        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Running on a UI thread
            sbiStatus.Content = "Ready";
            progressBar.Visibility = Visibility.Hidden;
        }


/*
        void ShowProgress(string pi, int totalDigits, int digitsSoFar)
        {
            // Display progress in UI
            tblkResults.Text = pi;
            progressBar.Maximum = totalDigits;
            progressBar.Value = digitsSoFar;
            progressBar.Visibility = Visibility.Visible;

            if (digitsSoFar == totalDigits)
            {
                // Reset UI
                sbiStatus.Content = "Ready";
                progressBar.Visibility = Visibility.Hidden;
            }
        }
 */

    }
}

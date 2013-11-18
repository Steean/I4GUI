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

namespace Part_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            if (Properties.Settings.Default.CallUpgrade)
            {
                Properties.Settings.Default.Upgrade(); 
                Properties.Settings.Default.CallUpgrade = false;
            }   
            
            TextBlockName.Text = Properties.Settings.Default.Name;
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Reset();
        }

        private void UndoBtn_Click(object sender, RoutedEventArgs e)
        {        
            Properties.Settings.Default.Reload();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

    }
}

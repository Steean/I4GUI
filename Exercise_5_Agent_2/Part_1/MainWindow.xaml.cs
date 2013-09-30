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
using System.ComponentModel;
using System.Windows.Threading;

namespace Part_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dt;
   
        public MainWindow()
        {
            InitializeComponent();

            dt = new DispatcherTimer();
            dt.Tick += dt_Tick;
            dt.Interval = new TimeSpan(0,0,1);
            dt.Start();
        }

        void dt_Tick(object sender, EventArgs e)
        {
            AgentClock.Text = DateTime.Now.ToString("F");
        }
    }
}

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
using System.IO;

namespace Part_3
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FileStream fs = new FileStream(@"C:\Users\Stian\Documents\IHA\4.semester\GUI\Exercise_2\02 deltagerliste.csv",
                               FileMode.Open, FileAccess.Read);

            string[] tokens;
            string line = "";
            char[] separators = {';'};
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            StringBuilder sb = new StringBuilder();
            var index = 0;

            while ((line = sr.ReadLine()) != null)
            {
                tokens = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                
                foreach (string i in tokens)
                {
                    index++;
                    sb.Append(i.PadRight(25));
                    if(index == 4)
                    {
                        lb.Items.Add(sb.ToString());
                        sb.Clear();
                        index = 0;
                    }
                }
                
            }
            fs.Close();
        }
    }
}
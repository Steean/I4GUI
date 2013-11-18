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
using MvvmFoundation.Wpf;

namespace Part_4
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

        private void MIExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

/*


        private void ButtonAddNew_Click(object sender, RoutedEventArgs e)
        {
            Agents newAgentList = (Agents) this.FindResource("MyAgentList");
            newAgentList.Add(new Agent("0", "Name", "Speciality", "Assignment"));
            LbxAgents.SelectedIndex = LbxAgents.Items.Count - 1;
            CodeNameBox.Focus();
        }

        private void ButtonPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (LbxAgents.SelectedIndex > 0)
                LbxAgents.SelectedIndex--;
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (LbxAgents.SelectedIndex < LbxAgents.Items.Count - 1)
                LbxAgents.SelectedIndex++;
        }
 */
    }
}


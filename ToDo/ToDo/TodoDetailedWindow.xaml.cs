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
using System.Windows.Shapes;

namespace ToDo
{
    /// <summary>
    /// Interaction logic for TodoDetailedWindow.xaml
    /// </summary>
    public partial class TodoDetailedWindow : Window
    {
        public TodoDetailedWindow()
        {
            InitializeComponent();
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(AddTitleBox.Text))
            {
                MessageBoxResult res =
                    MessageBox.Show(
                        "You need to enter a title",
                        "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                if (res == MessageBoxResult.OK)
                {
                    return;
                }
            }
            
            if (String.IsNullOrWhiteSpace(AddDueDateBox.Text))
            {
                MessageBoxResult res =
                    MessageBox.Show(
                        "You need to enter a due date",
                        "Warning", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                if (res == MessageBoxResult.OK)
                {
                    return;
                }
            }

            DialogResult = true;
        }
    }
}

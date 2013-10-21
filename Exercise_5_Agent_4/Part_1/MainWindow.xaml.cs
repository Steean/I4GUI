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
            dt.Interval = new TimeSpan(0, 0, 1);
            dt.Start();
        }

        void dt_Tick(object sender, EventArgs e)
        {
            AgentClock.Text = DateTime.Now.ToString("F");
        }

        private void RedBg_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Background = (Brush)Application.Current.Resources["redBrush"];
        }

        private void GreenBg_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Background = (Brush)Application.Current.Resources["greenBrush"];
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbi = e.AddedItems[0] as ComboBoxItem;
            string newSortOrder;
            if (cbi != null)
            {
                if (cbi.Tag == null)
                    newSortOrder = "None";
                else
                    newSortOrder = cbi.Tag.ToString();

                SortDescription sortDesc = new SortDescription(newSortOrder, ListSortDirection.Ascending);
                ICollectionView cv = CollectionViewSource.GetDefaultView(DataContext);
                if (cv != null)
                {
                    cv.SortDescriptions.Clear();
                    if (newSortOrder != "None")
                        cv.SortDescriptions.Add(sortDesc);
                }
            }
        }

        private void DataGrid_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string temp = e.Column.Header.ToString();

            if (temp == "Assignment")
            {
                e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }
    }
}

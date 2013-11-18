using System;
using System.Collections.Generic;
using System.IO;
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

namespace Exercise_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<BabyName> babyContatiner = new List<BabyName>();
        private Dictionary<int, string> dict = new Dictionary<int, string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListDecadeTopNames_Loaded(object sender, RoutedEventArgs e)
        {
            int countRanks = 0;
            string topnames = "";
            string line = "";
            var path = AppDomain.CurrentDomain.BaseDirectory;
            FileStream fs = new FileStream(@path + "04-BabyNames.txt", FileMode.Open);

            StreamReader sr = new StreamReader(fs, false);
        
            while ((line = sr.ReadLine()) != null)
            {
                babyContatiner.Add(new BabyName(line));                
            }

            for (int i = 1900; i <= 2000; i += 10)
            {
                for (int j = 1; j <= 10; j++)                
                {
                       foreach (BabyName b in babyContatiner)
                       {
                           if (b.Rank(i) == j)
                           {
                               if (countRanks == 0)
                               {
                                   topnames += j.ToString();
                                   topnames += " ";
                                   topnames += b.Name;
                                   topnames += " and ";
                                   countRanks++;
                               }
                               else
                               {
                                   topnames += b.Name;
                               }
                           }                        
                       }
                       topnames += ";";
                    countRanks = 0;
                }

                dict.Add(i, topnames);
                topnames = "";
            }
        }

        private void DecadeOptions_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 1900; i <= 2000; i += 10)
            {
                DecadeOptions.Items.Add(i);
            }
        }

        private void DecadeOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] output;
            char[] separators = {';'};
            string value = "";

            ListDecadeTopNames.Items.Clear();
            dict.TryGetValue(Convert.ToInt16(DecadeOptions.SelectedItem), out value);
            output = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in output)
            {
                ListDecadeTopNames.Items.Add(s);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            RankingList.Items.Clear();
            var s = NameBox.Text;
            s = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
            var item = babyContatiner.FirstOrDefault(o => o.Name == s);
            if (item != null)
            {
                RankingBox.Text = item.AverageRank().ToString();
                if (item.Trend() < 0)
                {
                    TrendBox.Text = "Less popular";
                }
                else if (item.Trend() > 1)
                {
                    TrendBox.Text = "More popular";
                }
                else
                {
                    TrendBox.Text = "No trend";
                }

                for (int i = 1900; i <= 2000; i += 10)
                {
                    var rankList = i.ToString();
                    rankList += "  ";
                    rankList += item.Rank(i).ToString();
                    RankingList.Items.Add(rankList);
                }
            }
            else
            {
                NameBox.Background = Brushes.Red;
            }
        }

        private void MIExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FSmall_Click(object sender, RoutedEventArgs e)
        {
            MW.FontSize = 8;
        }

        private void FNormal_Click(object sender, RoutedEventArgs e)
        {
            MW.FontSize = 12;
        }

        private void FLarge_Click(object sender, RoutedEventArgs e)
        {
            MW.FontSize = 16;
        }

        private void FHuge_Click(object sender, RoutedEventArgs e)
        {
            MW.FontSize = 20;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pigeon_Reminder
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        HTTP http = new HTTP();
        ArrayList repos = new ArrayList();

        public MainWindow()
        {
            InitializeComponent();
            RepoComboBox.ItemsSource = repos;
            RepoComboBox.DisplayMemberPath = "repoFullName";
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            http.sAPI = http.GitUrlToAPI(RepoUrlBox.Text);
            http.Update();
            Repo repo = http.SetNewRepo();
            repos.Add(repo);

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void RepoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Repo item = RepoComboBox.SelectedItem as Repo;
            RepoLastUpateTime.Content = item.lastUpdateTime;
            RepoUrlBox.Text = item.repoCloneUrl;
            TimeSpan diff = DateTime.Now - item.lastUpdateTime;
            PigeonTime.Content = "约 " + (int)diff.TotalDays + " 天";
        }
    }
}

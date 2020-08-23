using Microsoft.WindowsAPICodePack.Dialogs;
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
using System.Windows.Forms;
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
        RepoManager manager = new RepoManager();
        Widget widget = new Widget();

        public MainWindow()
        {
            InitializeComponent();
            RepoComboBox.ItemsSource = manager.repos;
            RepoComboBox.DisplayMemberPath = "repoFullName";

            widget.Show();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Repo item = RepoComboBox.SelectedItem as Repo;
            if (item == null)
            {
                System.Windows.MessageBox.Show("您连Repo都没选择 您更新您马呢");
                return;
            }
            RepoComboBox.SelectedItem = manager.CreateNewRepo(item.repoCloneUrl);

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Repo repo = manager.CreateNewRepo(RepoUrlBox.Text);
            manager.repos.Add(repo);
            RepoComboBox.SelectedItem = repo;
        }

        private void RepoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Repo item = RepoComboBox.SelectedItem as Repo;
            RepoLastUpateTime.Content = item.lastUpdateTime.ToLocalTime();
            RepoUrlBox.Text = item.repoCloneUrl;
            TimeSpan diff = DateTime.Now - item.lastUpdateTime;
            PigeonTime.Content = "约 " + (int)diff.TotalDays + " 天";
        }

        private void WidgetSelect_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string strFileName = dialog.FileName;
                WidgetFolder.Text = strFileName;
            }
            BitmapImage bitmap = new BitmapImage(new Uri(WidgetFolder.Text + "\\idle.png", UriKind.Absolute));
            WidgetPreview.Source = bitmap;
            widget.SetImage(bitmap);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            widget.Close();
        }

        private void IWannaPigeon_Click(object sender, RoutedEventArgs e)
        {
            Widget widget = new Widget();
            widget.SetImage((BitmapImage)WidgetPreview.Source);
            widget.Show();
        }
    }
}

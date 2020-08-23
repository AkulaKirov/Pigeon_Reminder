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
        RepoManager rManager = new RepoManager();
        WidgetManager wManager = new WidgetManager();
        Widget widget = new Widget();

        public MainWindow()
        {
            InitializeComponent();
            RepoComboBox.ItemsSource = rManager.repos;
            RepoComboBox.DisplayMemberPath = "repoFullName";
            WidgetComboBox.ItemsSource = wManager.widgetTemplates;
            WidgetComboBox.DisplayMemberPath = "templateName";

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Repo item = RepoComboBox.SelectedItem as Repo;
            if (item == null)
            {
                System.Windows.MessageBox.Show("您连Repo都没选择 您更新您马呢");
                return;
            }
            rManager.UpdateRepo((Repo)item);

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (RepoUrlBox.Text.Trim() == "")
            {
                System.Windows.MessageBox.Show("您连克隆地址都没输入 您添加您马呢");
                return;
            }
            Repo repo = rManager.CreateNewRepo(RepoUrlBox.Text);
            rManager.AddRepo(repo);
            RepoComboBox.SelectedItem = repo;
            RepoComboBox.ItemsSource = rManager.repos;
        }

        private void RepoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Repo item = RepoComboBox.SelectedItem as Repo;
            RepoLastUpateTime.Content = item.lastUpdateTime.ToLocalTime();
            RepoUrlBox.Text = item.repoCloneUrl;
            TimeSpan diff = DateTime.Now - item.lastUpdateTime;
            PigeonTime.Content = "约 " + (int)diff.TotalDays + " 天";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            wManager.CloseAllWidgets();
            rManager.DeleteSameRepo();
            rManager.SaveRepos();
            GC.Collect();
            System.Windows.Application.Current.Shutdown();
        }

        private void IWannaPigeon_Click(object sender, RoutedEventArgs e)
        {
            wManager.AddWidget((WidgetTemplate)WidgetComboBox.SelectedItem);
        }

        private void WidgetAdd_Click(object sender, RoutedEventArgs e)
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
            var item = wManager.AddWidgetTemplateFromFolder(WidgetFolder.Text);
            wManager.wTemplate = item;
            WidgetComboBox.SelectedItem = item;
        }

        private void WidgetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (WidgetTemplate)WidgetComboBox.SelectedItem;
            WidgetFolder.Text = item.path;
            WidgetPreview.Source = ((Img)item.imgs[0]).img;
            WidgetImgListBox.ItemsSource = item.imgs;
            WidgetImgListBox.DisplayMemberPath = "name";
        }

        private void WidgetImgListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WidgetPreview.Source = ((Img)WidgetImgListBox.SelectedItem).img;
        }
    }
}

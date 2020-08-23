using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace Pigeon_Reminder
{
    /// <summary>
    /// Widget.xaml 的交互逻辑
    /// </summary>
    public partial class Widget : Window
    {
        public Widget()
        {
            InitializeComponent();
        }

        public void SetImage(BitmapImage bitmap)
        {
            img.Source = bitmap;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}

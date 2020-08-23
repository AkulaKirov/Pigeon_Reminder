using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pigeon_Reminder
{
    /// <summary>
    /// Widget.xaml 的交互逻辑
    /// </summary>
    public partial class Widget : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        Random r = new Random();


        public Widget()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Move();
        }

        void Move()
        {
            this.Left += r.Next(-300, 300);
            if (0 > Left)
            {
                Left = 0;
            }else if (Left > (int)SystemParameters.PrimaryScreenWidth)
            {
                Left = (int)SystemParameters.PrimaryScreenWidth;
            }
            this.Top += r.Next(-300, 300);
            if (0 > Top )
            {
                Top = 0;
            }
            else if (Top > (int)SystemParameters.PrimaryScreenHeight)
            {
                Left = (int)SystemParameters.PrimaryScreenHeight;
            }
        }

        public void SetImage(BitmapImage bitmap)
        {
            img.Source = bitmap;
        }

        public void SetTemplate(WidgetTemplate t)
        {
            Img img = (Img)t.imgs[0];
            BitmapImage i = img.img;
            SetImage(i);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
            else if(e.RightButton == MouseButtonState.Pressed)
            {
                this.Close();
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Pigeon_Reminder
{
    class WidgetManager
    {
        public ArrayList widgets = new ArrayList();
        public ArrayList widgetTemplates = new ArrayList();
        public WidgetTemplate wTemplate;

        public WidgetManager()
        {
            string local = AppDomain.CurrentDomain.BaseDirectory;
            string widgetTemplateFolder = local + "Widgets\\";
            DirectoryInfo templateFolder = new DirectoryInfo(widgetTemplateFolder);
            foreach (DirectoryInfo dir in templateFolder.GetDirectories())
            {
                widgetTemplates.Add(AddWidgetTemplateFromFolder(dir.FullName));
            }
        }

        public WidgetTemplate AddWidgetTemplateFromFolder(string folderPath)
        {
            WidgetTemplate template = new WidgetTemplate();
            DirectoryInfo dir = new DirectoryInfo(folderPath);
            template.templateName = dir.Name;
            template.path = folderPath;
            foreach (FileInfo file in dir.GetFiles())
            {
                Img img = new Img();
                img.img = new BitmapImage(new Uri(file.FullName, UriKind.Absolute));
                img.name = file.Name;
                template.imgs.Add(img);
                
            }
            //MessageBox.Show(template.templateName);
            widgetTemplates.Add(template);
            return template;
        }

        public void CloseAllWidgets()
        {
            foreach(Widget w in widgets)
            {
                w.Close();
            }
            return;
        }

        public void AddWidget(WidgetTemplate t)
        {
            wTemplate = t;
            Widget widget = new Widget();
            Random r = new Random();
            widget.SetTemplate(t);
            widget.WindowStartupLocation = WindowStartupLocation.Manual;
            widget.Left = r.Next(0, (int)SystemParameters.PrimaryScreenWidth);
            widget.Top = r.Next(0, (int)SystemParameters.PrimaryScreenHeight);

            widgets.Add(widget);
            widget.Show();
        }

    }
}

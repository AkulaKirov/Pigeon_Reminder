using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pigeon_Reminder
{
    public class WidgetTemplate
    {
        public string path;
        public string templateName
        {
            //我也不知道为啥要这样 但不搞访问器那边ComboBox就不显示
            get;
            set;
        }
        public ArrayList imgs = new ArrayList();
    }
}

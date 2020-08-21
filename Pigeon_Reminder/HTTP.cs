using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Pigeon_Reminder
{
    class HTTP
    {
        //string API = @"https://api.github.com/repos/{owner}/{repo}";
        string API = @"https://api.github.com/repos/AkulaKirov/Pigeon_Reminder";

        public DateTime GetLastUpdateTime()
        {
            string get;
            Task<string> task = Get();
            get = task.Result;
            //System.IO.File.WriteAllText(@"get.json", get);
            DateTime time = JsonToTime(get);
            return time;
        }

        public async Task<string> Get()
        {
            HttpWebRequest hwRequest = null;
            HttpWebResponse hwResponse = null;

            string get = string.Empty;
            try
            {
                hwRequest = (System.Net.HttpWebRequest)WebRequest.Create(API);
                //hwRequest.Timeout = 30000;
                hwRequest.Method = "GET";
                hwRequest.ContentType = "application/vnd.github.v3+json";
                hwRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:79.0) Gecko/20100101 Firefox/79.0";
            }
            catch (System.Exception err)
            {
                return err.ToString();
            }
            try
            {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.UTF8);
                get = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
            }
            catch (System.Exception err)
            {
                return err.ToString();
            }
            return get;
        }

        public DateTime JsonToTime(string json)
        {
            DateTime time = DateTime.Now;

            try
            {
                var result = JObject.Parse(json);
                var value = result.GetValue("updated_at");
                time = DateTime.Parse(value.ToString(), System.Globalization.CultureInfo.CurrentCulture);
            }
            catch (Exception e)
            {
                System.IO.File.WriteAllText(@"err.json", e.ToString());
            }
            
            return time;
        }

    }
}
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace Pigeon_Reminder
{
    class HTTP
    {
        const string API = @"https://api.github.com/repos/{owner}/{repo}";
        public string sAPI;
        public string get = null;
        JObject result;

        public Repo GetRepoInfo()
        {
            get = null;
            try
            {
                get = HttpGet(sAPI);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
            System.IO.File.WriteAllText(@"get.json", get);
            result = JObject.Parse(get);
            Repo repo = SetNewRepo();
            if (repo == null)
            {
                MessageBox.Show("获取失败");
            }
            else
            {
                MessageBox.Show("获取成功");
            }
            return repo;
        }

        
        //鸽了 不想写异步了
        public string HttpGet(string gitUrl)
        {
            string get = null;
            string url;
            url = GitUrlToAPI(gitUrl);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = "User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:79.0) Gecko/20100101 Firefox/79.0";
            request.ContentType = "application/vnd.github.v3+json";
            using (WebResponse wr = request.GetResponseAsync().Result)
            {
                using (StreamReader reader = new StreamReader(wr.GetResponseStream(), Encoding.UTF8))
                {
                    get = reader.ReadToEnd();
                }
            }
            return get;
        }

        public DateTime GetTime(string json)
        {
            DateTime time = DateTime.Now;

            try
            {
                var result = JObject.Parse(json);
                var value = result.GetValue("updated_at");
                time = DateTime.Parse(value.ToString(), System.Globalization.CultureInfo.CurrentCulture);
                time = time.AddHours(8f);
            }
            catch (Exception e)
            {
                System.IO.File.WriteAllText(@"err.json", e.ToString());
            }
            
            return time;
        }

        public string GitUrlToAPI(string gitUrl)
        {
            try
            {
                string[] s = gitUrl.Split('/');
                string owner = s[s.Length - 2];
                string repo = s[s.Length - 1].Replace(".git", "");
                return ReplaceAPI(owner, repo);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }

        }

        public string ReplaceAPI(string owner, string repo)
        {
            sAPI = API.Replace("{owner}", owner);
            sAPI = sAPI.Replace("{repo}", repo);
            return sAPI;
        }

        public Repo SetNewRepo()
        {
            Repo repo = new Repo();

            JObject owner = (JObject)result.GetValue("owner");
            repo.owner = owner.GetValue("login").ToString();
            repo.repoName = result.GetValue("name").ToString();
            repo.repoCloneUrl = result.GetValue("clone_url").ToString();
            repo.createTime = DateTime.Parse(result.GetValue("created_at").ToString(), System.Globalization.CultureInfo.CurrentCulture);
            repo.lastUpdateTime = DateTime.Parse(result.GetValue("updated_at").ToString(), System.Globalization.CultureInfo.CurrentCulture);
            
            return repo;
        }

    }
}
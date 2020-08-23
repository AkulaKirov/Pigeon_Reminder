using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pigeon_Reminder
{
    class RepoManager
    {
        public HTTP http = new HTTP();
        public ArrayList repos = new ArrayList();

        public RepoManager()
        {
            LoadRepos();
        }

        public void UpdateRepo(Repo r)
        {
            foreach (Repo a in repos)
            {
                if (a.repoFullName == r.repoFullName)
                {
                    repos.Remove(a);
                    repos.Add(CreateNewRepo(r.repoCloneUrl));
                    break;
                }
            }
        }

        public void UpdateRepo(string gitUrl)
        {
            foreach (Repo a in repos)
            {
                if (a.repoCloneUrl == gitUrl)
                {
                    repos.Remove(a);
                    Repo repo;
                    http.sAPI = http.GitUrlToAPI(gitUrl);
                    repo = http.GetRepoInfo();
                    repos.Add(repo);
                    break;
                }
            }
        }

        public void AddRepo(Repo r)
        {
            repos.Add(r);
            SaveRepos();
        }

        public Repo CreateNewRepo(string gitUrl)
        {
            foreach(Repo r in repos)
            {
                if(r.repoCloneUrl == gitUrl)
                {
                    MessageBox.Show("Repo已存在 只进行更新操作");
                    UpdateRepo(gitUrl);
                    return r;
                }
            }
            Repo repo;
            http.sAPI = http.GitUrlToAPI(gitUrl);
            repo = http.GetRepoInfo();
            SaveRepos();
            return repo;
        }

        public void SaveRepos()
        {
            JArray arr = JArray.FromObject(repos);
            string json = Convert.ToString(arr);
            File.WriteAllText("repos.json", json, Encoding.UTF8);
        }

        public void LoadRepos()
        {
            repos.RemoveRange(0, repos.Count);
            if (File.Exists("repos.json"))
            {
                string json = File.ReadAllText("repos.json", Encoding.UTF8);
                JArray arr = JArray.Parse(json);
                foreach (JObject obj in arr)
                {
                    repos.Add(obj.ToObject<Repo>());
                }
            }
            
        }

        public void DeleteSameRepo()
        {
            Repo r;
            r = (Repo)repos[0];
            ArrayList arr = new ArrayList();
            arr.Add(r);
            foreach(Repo a in repos)
            {
                if(a != null && a.repoFullName != r.repoFullName)
                {
                    arr.Add(a);
                }
            }
            repos = arr;
        }
    }
}

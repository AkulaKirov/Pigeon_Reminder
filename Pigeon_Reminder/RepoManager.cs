using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pigeon_Reminder
{
    class RepoManager
    {
        public HTTP http = new HTTP();
        public ArrayList repos = new ArrayList();

        void AddRepo(Repo r)
        {
            repos.Add(r);
        }

        public Repo CreateNewRepo(string gitUrl)
        {
            Repo repo;
            http.sAPI = http.GitUrlToAPI(gitUrl);
            repo = http.GetRepoInfo();
            return repo;
        }

    }
}

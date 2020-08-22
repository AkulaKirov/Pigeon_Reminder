using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pigeon_Reminder
{
    class Repo
    {
        public string owner;
        public string repoName;
        public string repoFullName
        {
            get { return owner + "/" + repoName; }
        }
        public string repoCloneUrl;
        public DateTime createTime;
        public DateTime lastUpdateTime;
        
        void Update()
        {

        }

        string GetRepoName()
        {
            return owner + "/" + repoName;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systems_ACW
{
    public class Announcement
    {
        string title;
        string body;
        DateTime timeSinceLastComment;

        public string Title { get { return title; } }
        public Announcement(string pTitle, string pBody)
        {
            title = pTitle;
            body = pBody;
            timeSinceLastComment = DateTime.Now;
        }
    }
}

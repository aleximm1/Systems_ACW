using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systems_ACW
{
    public class Comment
    {
        private User poster;
        private string body;

        public string Body { get { return body; } }
        public User Poster { get { return poster; } }

        public Comment(string pBody, User pPoster)
        {
            body = pBody;
            poster = pPoster;
        }
    }
}

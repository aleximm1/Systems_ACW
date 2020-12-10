using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systems_ACW
{
    class Reply
    {
        private User poster;
        private string body;
        private Comment commentRepliedTo;

        public string Body { get { return body; } }
        public User Poster { get { return poster; } }

        public Reply(string pBody, User pPoster, Comment pComment)
        {
            body = pBody;
            poster = pPoster;
            commentRepliedTo = pComment;
        }
    }
}

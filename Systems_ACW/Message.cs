using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systems_ACW
{
    class Message
    {
        private string sender;
        private string body;
        private DateTime timestamp;
        
        public string Sender { get { return sender; } }
        public string Body { get { return body; } }
        public DateTime Timestamp { get { return timestamp; } }

        public Message(string pSender, string pBody, DateTime pTimestamp)
        {
            sender = pSender;
            body = pBody;
            timestamp = pTimestamp;
        }
    }
}

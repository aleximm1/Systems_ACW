using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace Systems_ACW
{
    /// <summary>
    /// Interaction logic for MessagesWindow.xaml
    /// </summary>
    public partial class MessagesWindow : Window
    {
        private User currentUser;
        private User currentlySelectedUser;
        public MessagesWindow(User pCurrentUser)
        {
            InitializeComponent();
            currentUser = pCurrentUser;
            List<User> users = GetUsers();
            for(int userNum = 0; userNum < users.Count(); userNum++)
            {
                if (users[userNum].Id == currentUser.Id)
                {
                    users.Remove(users[userNum]);
                    break;
                }
            }
            currentlySelectedUser = users[0];
            for (int i = 0; i < users.Count(); i++)
            {
                UsersBox.Items.Add(users[i]);
            }
            ChangeSelectedUser();
        }

        private List<User> GetUsers()
        {
            List<User> usersList = new List<User>();
            int userId = 0;
            XmlDocument usersDoc = new XmlDocument();
            usersDoc.Load("..\\..\\XML_Files\\Users.xml");
            foreach (XmlNode node in usersDoc.DocumentElement)
            {
                string userIdString = node.Attributes[0].InnerText;
                try
                {
                    userId = Convert.ToInt32(userIdString);
                }
                catch {}
                usersList.Add(new User(userId ,node.FirstChild.InnerText, "null"));
            }
            return usersList;
        }

        private void ChangeSelectedUser()
        {
            MessagesBox.Items.Clear();
            XmlDocument ChatsDoc = new XmlDocument();
            ChatsDoc.Load("..\\..\\XML_Files\\Chats.xml");
            XmlNode currentChatNode = ChatsDoc.DocumentElement;
            bool ChatFound = false;
            foreach (XmlNode ChatNode in ChatsDoc.DocumentElement)
            {
                if ((ChatNode.Attributes[0].InnerText == currentUser.Id.ToString() || ChatNode.Attributes[1].InnerText == currentUser.Id.ToString()) && (ChatNode.Attributes[0].InnerText == currentlySelectedUser.Id.ToString() || ChatNode.Attributes[1].InnerText == currentlySelectedUser.Id.ToString()))
                {
                    ChatFound = true;
                    currentChatNode = ChatNode;
                }
                else
                {
                    continue;
                }
            }
            if (!ChatFound)
            {
                CreateChat();
                ChatsDoc.Load("..\\..\\XML_Files\\Chats.xml");
                foreach (XmlNode ChatNode in ChatsDoc.DocumentElement)
                {
                    if ((ChatNode.Attributes[0].InnerText == currentUser.Id.ToString() || ChatNode.Attributes[1].InnerText == currentUser.Id.ToString()) && (ChatNode.Attributes[0].InnerText == currentlySelectedUser.Id.ToString() || ChatNode.Attributes[1].InnerText == currentlySelectedUser.Id.ToString()))
                    {
                        currentChatNode = ChatNode;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            foreach(XmlNode messageNode in currentChatNode.ChildNodes)
            {
                Message message = new Message(messageNode.Attributes[0].InnerText, messageNode.FirstChild.InnerText, Convert.ToDateTime(messageNode.LastChild.InnerText));
                MessagesBox.Items.Add(message);
            }
        }

        private void UsersBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentlySelectedUser = (User)UsersBox.SelectedItem;
            ChangeSelectedUser();
        }

        private void CreateChat()
        {
            XmlDocument ChatsDoc = new XmlDocument();
            ChatsDoc.Load("..\\..\\XML_Files\\Chats.xml");
            XmlElement root = ChatsDoc.DocumentElement;
            XmlElement ChatElem = ChatsDoc.CreateElement("Chat");
            ChatElem.SetAttribute("id1", currentUser.Id.ToString());
            ChatElem.SetAttribute("id2", currentlySelectedUser.Id.ToString());
            root.AppendChild(ChatElem);
            ChatsDoc.Save("..\\..\\XML_Files\\Chats.xml");
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument ChatsDoc = new XmlDocument();
            ChatsDoc.Load("..\\..\\XML_Files\\Chats.xml");
            XmlElement root = ChatsDoc.DocumentElement;
            XmlNodeList ChatsNodeList = ChatsDoc.GetElementsByTagName("Chat");
            XmlNode chatNode = ChatsNodeList[0];
            foreach (XmlNode node in ChatsNodeList)
            {
                if ((node.Attributes[0].InnerText == currentUser.Id.ToString() || node.Attributes[1].InnerText == currentUser.Id.ToString()) && (node.Attributes[0].InnerText == currentlySelectedUser.Id.ToString() || node.Attributes[1].InnerText == currentlySelectedUser.Id.ToString()))
                {
                    chatNode = node;
                    break;
                }
            }
            XmlElement messageElem = ChatsDoc.CreateElement("message");
            messageElem.SetAttribute("senderid", currentUser.Id.ToString());
            XmlElement bodyElem = ChatsDoc.CreateElement("body");
            bodyElem.InnerText = MessageTextbox.Text;
            XmlElement timestampElem = ChatsDoc.CreateElement("timestamp");
            timestampElem.InnerText = DateTime.Now.ToString();
            messageElem.AppendChild(bodyElem);
            messageElem.AppendChild(timestampElem);
            chatNode.AppendChild(messageElem);
            root.AppendChild(chatNode);
            ChatsDoc.Save("..\\..\\XML_Files\\Chats.xml");
            Message message = new Message(currentUser.Id.ToString() , MessageTextbox.Text, DateTime.Now);
            MessagesBox.Items.Add(message);
        }
    }
}

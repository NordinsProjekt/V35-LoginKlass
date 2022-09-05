using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalChat
{
    public class Chat
    {
        private List<Message> messages = new List<Message>();
        private bool shouldRender = false;
        public Chat()
        {
            Post("test001", "Detta är ett test meddelande");
        }

        public void Post(string username, string message)
        {
            Message msg = new Message(username,message,DateTime.Now);
            RemoveOldPosts();
            messages.Add(msg);
        }

        public List<Message> GetAllMessages()
        {
            return messages;
        }

        public Message ShowMessage(int index)
        {
            Message msg = messages[index];
            return msg;
        }
        private void RemoveOldPosts()
        {
            if (messages.Count > 50)
            {
                messages.RemoveAt(0);
            }
        }
        public bool ShouldRender
        {
            get { return shouldRender; }
            set { shouldRender = value; }
        }
    }
    public record Message(string username, string message, DateTime dateTime)
    {
        public string ShowMessage()
        {
            return "["+dateTime.ToString()+"] " + username + ": " + message;
        }
    }
}

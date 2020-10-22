using System;
namespace AtWork.Models
{
    public class Message
    {
        public Message()
        {
        }

        public Message(string title = "", string description = "", string message = "")
        {
            this.title = title;
            this.description = description;
            this.message = message;
        }

        public string title { get; set; }
        public string description { get; set; }
        public string error { get; set; }
        public string error_description { get; set; }
        public string hint { get; set; }
        public string message { get; set; }
    }
}

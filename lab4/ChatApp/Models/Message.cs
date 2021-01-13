using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApp.Models
{
    public class Message
    {
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public string MessageText { get; set; }
        public string Time { get; set; }
        public string Photo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloggingApp.Models
{
    public class Posts
    {
        public int PostID { get; set; }
        public int UserID { get; set; }
        public int CategoryID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
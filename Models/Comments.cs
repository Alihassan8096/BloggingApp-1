using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloggingApp.Models
{
    public class Comments
    {
        public int CommentID { get; set; }
        public int UserID { get; set; }
        public int PostID { get; set; }
        public string CommentText { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
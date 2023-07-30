using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloggingApp.Models
{
    public class Users
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Password { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
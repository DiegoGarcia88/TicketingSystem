using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketingSystem.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public User(string name, string email, string password)
        {
            Email = email;
            Password = password;
            Name = name;
        }

        public User()
        {

        }
    }
}
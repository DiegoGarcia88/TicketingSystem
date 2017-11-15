using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketingSystem.Models
{
    public class User
    {
        public string Email { get;}
        public string Password { get;}
        public string Name { get;}

        public User(string name, string email, string password)
        {
            Email = email;
            Password = password;
            Name = name;
        }
    }
}
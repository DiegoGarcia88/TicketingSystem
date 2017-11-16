using System;
using System.ComponentModel.DataAnnotations;


namespace TicketingSystem.Models
{
    public class User
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public User(string name, string email, string password)
        {
            Email = email;
            //Passwords are stored in plain text due to time constraints, will be changing it later
            Password = password;
            Name = name;
        }

        public User()
        {

        }
    }
}
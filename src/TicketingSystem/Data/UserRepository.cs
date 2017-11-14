using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketingSystem.Models;

namespace TicketingSystem.Data
{
    public class UserRepository
    {
        private static List<User> _users = null;

        static UserRepository()
        {
            LoadData();
        }

        private static void LoadData()
        {
            _users = new List<User>()
            {
                new User("diego@test.com","Abcd1234","Diego"),
                new User("prueba@test.com","Password","Usuario")
            };
        }

        public static User GetUser(string email)
        {
            User u = null;
            foreach (var user in _users)
            {
                if (user.Email == email)
                {
                    u = user;
                    break;
                }
            }
            return u;
        }

        public static List<User> GetUsers()
        {
            return _users;
        }
    }
}
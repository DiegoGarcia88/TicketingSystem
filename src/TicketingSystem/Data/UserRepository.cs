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
                new User("Diego","diego@test.com","Abcd1234"),
                new User("Usuario","prueba@test.com","Password")
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

        //Verify if the user exists
        public static bool UserExists(string email)
        {
            bool exists = false;
            foreach (var user in _users)
            {
                if (email == user.Email)
                {
                    exists = true;
                    return exists;
                }
            }

            return exists;
        }
        //Adds user to the repository if it doesn't already exist
        public static bool AddUser(User u)
        {
            bool added = false;
            if (!UserExists(u.Email) && VerifyInputData(u.Name,u.Email,u.Password))
            {
                _users.Add(u);
                added = true;
            }
            return added;
        }

        //Final check to make sure we don't create a user with empty data
        public static bool VerifyInputData(string name,string email, string password)
        {
            bool validate = false;
            if (name != "" && email != "" && password != "")
            {
                validate = true;
            }
            return validate;
        }
    }
}
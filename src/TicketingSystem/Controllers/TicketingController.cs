using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketingSystem.Models;
using TicketingSystem.Data;

namespace TicketingSystem.Controllers
{
    public class TicketingController : Controller
    {
        public ActionResult Register()
        {
            User u = new User();
            return View(u);
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (UserRepository.AddUser(user))
                {
                    //poner mensaje de success
                    UserLogin(user.Email, user.Password);
                    return RedirectToAction("DashBoard");
                }
                //Poner mensaje de error usuario ya existe
                return View();
            }
            
            return View();
        }

        public ActionResult Login()
        {
            
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (UserLogin(email, password))
            {
                return View("DashBoard", TicketRepository.GetTickets());
            }
            else
            {
                return View();
            }
            
        }
        //Method to log the user into the system
        private bool UserLogin(string email, string password)
        {
            if (ValidateCredentials(email, password))
            {
                Session["userEmail"] = email;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidateCredentials(string email, string password)
        {
            bool validate = false;
            List<User> users = UserRepository.GetUsers();
            foreach (var user in users)
            {
                if (email == user.Email && password == user.Password)
                {
                    validate = true;
                    break;
                }
            }

            return validate;
        }

        public ActionResult DashBoard(string email)
        {
            return View(TicketRepository.GetTickets());
            
        }

        public ActionResult Create()
        {
            try
            {
                User tempUser = UserRepository.GetUser(Session["userEmail"].ToString());
                //We remove password from the user before passing it to the view
                User user = new User(tempUser.Name,tempUser.Email,"");
                var ticket = new Ticket(user, user);
                SetupUserSelectList();
                return View(ticket);
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("Login");
            }
            
        }

        [HttpPost]
        public ActionResult Create(string title,string body,int status,string assignee)
        {
            //Assignee is passed as string for both security and simplicity
            if (ModelState.IsValid)
            {
                User auth = UserRepository.GetUser(Session["userEmail"].ToString());
                User assign = UserRepository.GetUser(assignee);
                if (auth != null && assign != null)
                {
                    Ticket ticket = new Ticket(title, body, status, auth, assign);
                    if (TicketRepository.AddTicket(ticket))
                    {
                        //mensaje de exito
                        return RedirectToAction("DashBoard");
                    }
                }
                
            }
            SetupUserSelectList();
            return View();
            
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var ticket = TicketRepository.GetTicket((int)id);
            return View(ticket);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var ticket = TicketRepository.GetTicket((int)id);
            return View(ticket);
        }

         private void SetupUserSelectList()
        {
            
            ViewBag.UserList = new SelectList(UserRepository.GetUsers(),"Email","Name");
        }
    }
}
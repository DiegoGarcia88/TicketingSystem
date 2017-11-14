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
        private UserRepository _userRepo = null;
        private TicketRepository _ticketRepo = null;

        public TicketingController()
        {
            _userRepo = new UserRepository();
            _ticketRepo = new TicketRepository();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string name, string email, string password)
        {
            return View();
        }

        public ActionResult Login()
        {
            
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (ValidateCredentials(email, password))
            {
                return View("DashBoard", TicketRepository.GetTickets());
            }
            else
            {
                return View();
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
                    Session["userEmail"] = email;
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

         
    }
}
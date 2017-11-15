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
            User u = new User("", "", "");
            return View(u);
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (UserRepository.AddUser(user))
            {
                //poner mensaje de success
                return RedirectToAction("DashBoard");                
            }
            //poner mensaje de error
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
                Session["userEmail"] = email;
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
                User u = UserRepository.GetUser(Session["userEmail"].ToString());
                var ticket = new Ticket("", "", u, u);
                return View(ticket);
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("Login");
            }
            
        }

        //ver como hacer para entre el submit y ahora que se tomen los string de author y assignee
        //como users y no como strings
        [HttpPost]
        public ActionResult Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                TicketRepository.AddTicket(ticket);
            }
            
            return RedirectToAction("DashBoard");
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
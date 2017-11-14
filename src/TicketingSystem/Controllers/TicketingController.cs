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
        private UserRepository _users = null;
        private TicketRepository _tickets = null;

        public TicketingController()
        {
            _users = new UserRepository();
            _tickets = new TicketRepository();
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult DashBoard(string email)
        {
            //Cambiar email por variable de session email una vez implementado el login
            var user = _users.GetUser(email);
            ViewBag.Name = user.Name;
            ViewBag.Email = user.Email;
            return View(_tickets);
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
            var ticket = _tickets.GetTicket((int)id);
            return View(ticket);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var ticket = _tickets.GetTicket((int)id);
            return View(ticket);
        }

         
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketingSystem.Models;
using TicketingSystem.Data;
using System.Net;
using System.Collections;

namespace TicketingSystem.Controllers
{
    public class TicketingController : Controller
    {

        #region Action Methods
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
                    TempData["Message"] = "User was Created Successfully";
                    UserLogin(user.Email, user.Password);
                    return RedirectToAction("DashBoard");
                }
                TempData["Message"] = "User already exists";
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
                return RedirectToAction("DashBoard");
            }
            else
            {
                return View();
            }
            
        }
        
        public ActionResult DashBoard()
        {
            
            //Verify if the user is logged in, otherwise redirect him to login page
            try
            {
                //We display only current user open tickets by default
                User tempUser = UserRepository.GetUser(Session["userEmail"].ToString());
                //Select all tickets for current user
                List<Ticket> userTickets = TicketRepository.GetTickets().Where(t => t.Assignee == tempUser).ToList();
                //Select All Open Tickets
                List<Ticket> openTickets = TicketRepository.GetTickets().Where(t => t.Status == Ticket.EnumStatus.Open).ToList();
                //Intersect Open tickets with user tickets for default dashboard
                List<Ticket> ticketsToView = userTickets.Intersect(openTickets).ToList();
                //Store status options for display in the filter
                ViewBag.Statuses = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem { Selected = true,Text = "Open", Value = "0"},
                    new SelectListItem { Selected = false,Text = "Closed", Value = "1"},
                    new SelectListItem { Selected = false,Text = "All", Value = "2"}
                },"Value","Text");
                return View(ticketsToView);
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult DashBoard(string filterTitle, string status, bool allUsers)
        {
            //Store status options for display in the status filter
            ViewBag.Statuses = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem { Selected = true,Text = "Open", Value = "0"},
                    new SelectListItem { Selected = false,Text = "Closed", Value = "1"},
                    new SelectListItem { Selected = false,Text = "All", Value = "2"}
                }, "Value", "Text");
            //If all is selected, we show all tickets with no filters
            if (allUsers)
            {
                return View(FilterList(TicketRepository.GetTickets(),filterTitle,status));
            }
            else
            {
                User tempUser = UserRepository.GetUser(Session["userEmail"].ToString());
                //Select all tickets for current user
                List<Ticket> userTickets = TicketRepository.GetTickets().Where(t => t.Assignee == tempUser).ToList();
                return View(FilterList(userTickets, filterTitle,status));               
            }            
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
                //If user is not logged in we redirect them to login page
                return RedirectToAction("Login");
            }
            
        }

        [HttpPost]
        public ActionResult Create(string title,string body,int status,string assignee)
        {
            if (String.IsNullOrWhiteSpace(title))
            {
                ModelState.AddModelError("Title", "Title Can't be Empty");
            }
            if (String.IsNullOrWhiteSpace(body))
            {
                ModelState.AddModelError("Body", "Body Can't be Empty");
            }

            //Assignee is passed as string for both security and simplicity
            User auth = UserRepository.GetUser(Session["userEmail"].ToString());
            User assign = UserRepository.GetUser(assignee);
            if (auth == null)
            {
                ModelState.AddModelError("", "Error Selecting Author");
                auth = new User("Empty","Empty","Empty");
            }
            if (assign == null)
            {
                ModelState.AddModelError("", "Error Selecting Assignee");
                assign = new User("Empty", "Empty", "Empty");
            }
            Ticket ticket = new Ticket(title, body, status, auth, assign);
            if (ModelState.IsValid)
            {
                if (TicketRepository.AddTicket(ticket))
                {
                    TempData["Message"] = "Ticket Successfully Created!";
                    return RedirectToAction("DashBoard");
                }
            }

            SetupUserSelectList();
            return View(ticket);
            
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = TicketRepository.GetTicket((int)id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            SetupUserSelectList();
            return View(ticket);
        }

        [HttpPost]
        public ActionResult Edit(int id, string title, string body, int status, string assignee)
        {
            if (ModelState.IsValid)
            {
                User assign = UserRepository.GetUser(assignee);
                if (assign != null)
                {
                    TicketRepository.UpdateTicket(id, title, body, status, assign);
                    TempData["Message"] = "Ticket Successfully Updated!";
                    return RedirectToAction("DashBoard");
                }
                
                
                
            }

            SetupUserSelectList();
            return View(id);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = TicketRepository.GetTicket((int)id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }
        [ActionName("Delete"),HttpPost]
        public ActionResult DeletePost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketRepository.DeleteTicket((int)id);
            TempData["Message"] = "Ticket was Deleted";
            return RedirectToAction("DashBoard");
        }

        private void SetupUserSelectList()
        {
            
            ViewBag.UserList = new SelectList(UserRepository.GetUsers(),"Email","Name");
        }

        #endregion

        #region Auxiliary Functions
        //Filter lists by title and status
        private List<Ticket> FilterList(List<Ticket> list,string filterTitle,string status)
        {
            if (String.IsNullOrEmpty(filterTitle))
            {
                return FilterStatus(list,status);
            }
            List<Ticket> returnList = new List<Ticket>();
            if (!String.IsNullOrEmpty(filterTitle))
            {
                returnList = list.Where(s => s.Title.Contains(filterTitle)).ToList();
            }
            return FilterStatus(returnList, status);



        }
        //Returns only tickets matching status
        private List<Ticket> FilterStatus(List<Ticket> list, string status)
        {
            switch (status)
            {
                case "0":
                    //Select Open Tickets
                    List<Ticket> openTickets = list.Where(t => t.Status == Ticket.EnumStatus.Open).ToList();
                    return openTickets;
                case "1":
                    //Select Closed Tickets
                    List<Ticket> closedTickets = list.Where(t => t.Status == Ticket.EnumStatus.Closed).ToList();
                    return closedTickets;
                default:
                    return list;
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
        #endregion
    }
}
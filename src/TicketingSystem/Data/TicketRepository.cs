using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketingSystem.Models;

namespace TicketingSystem.Data
{
    public class TicketRepository
    {
        private static List<Ticket> _tickets = new List<Ticket>();

        static TicketRepository()
        {
            LoadData();
        }

        private static void LoadData()
        {
            _tickets = new List<Ticket>()
            {
                new Ticket("ticket 1","first ticket",0,UserRepository.GetUser("diego@test.com"),UserRepository.GetUser("diego@test.com")),
                new Ticket("ticket 2","second ticket",1,UserRepository.GetUser("prueba@test.com"),UserRepository.GetUser("diego@test.com")),
                new Ticket("ticket 3","third ticket",0,UserRepository.GetUser("diego@test.com"),UserRepository.GetUser("prueba@test.com")),
                new Ticket("ticket 4","fourth ticket",0,UserRepository.GetUser("prueba@test.com"),UserRepository.GetUser("diego@test.com")),
                new Ticket("ticket 5","fifth ticket",0,UserRepository.GetUser("diego@test.com"),UserRepository.GetUser("prueba@test.com")),
                new Ticket("ticket 6","sixth ticket",0,UserRepository.GetUser("prueba@test.com"),UserRepository.GetUser("diego@test.com")),
                new Ticket("ticket 7","seventh ticket",1,UserRepository.GetUser("diego@test.com"),UserRepository.GetUser("prueba@test.com")),
                new Ticket("ticket 8","eighth ticket",0,UserRepository.GetUser("prueba@test.com"),UserRepository.GetUser("diego@test.com")),
                new Ticket("ticket 9","ninth ticket",0,UserRepository.GetUser("diego@test.com"),UserRepository.GetUser("prueba@test.com"))

            };
            
        }

        public static Ticket GetTicket(int id)
        {
            Ticket t = null;
            foreach (var ticket in _tickets)
            {
                if (ticket.Id == id)
                {
                    t = ticket;
                    break;
                }
            }
            return t;
        }

        public static List<Ticket> GetTickets()
        {
            return _tickets;
        }

        public static bool AddTicket(Ticket ticket)
        {
            _tickets.Add(ticket);
            return true;
        }


        public static void UpdateTicket(Ticket ticket)
        {
            foreach (var t in _tickets)
            {
                if (ticket == t)
                {
                    t.Title = ticket.Title;
                    t.Body = ticket.Body;
                    t.Status = ticket.Status;
                    t.Assignee = ticket.Assignee;
                }
            }
        }

        public static void DeleteTicket(int id)
        {
            foreach (var t in _tickets)
            {
                if (id == t.Id)
                {
                    Ticket ticket = TicketRepository.GetTicket(id);
                    _tickets.Remove(ticket);
                    ticket = null;                    
                }
                
            }
        }
        
    }
}
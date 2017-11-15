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
                new Ticket("ticket 1","this is the first ticket that was ever created, and the most important one",UserRepository.GetUser("diego@test.com"),UserRepository.GetUser("diego@test.com"))
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
    }
}
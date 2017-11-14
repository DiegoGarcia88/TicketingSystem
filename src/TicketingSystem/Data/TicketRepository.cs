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
            var tickets = new List<Ticket>()
            {
                //new Ticket("ticket 1","this is the first ticket that was ever created, and the most important one",,),
            };
            
        }

        public Ticket GetTicket(int id)
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
    }
}
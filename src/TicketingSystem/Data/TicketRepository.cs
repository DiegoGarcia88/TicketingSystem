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
                new Ticket("ticket 2","second ticket",0,UserRepository.GetUser("prueba@test.com"),UserRepository.GetUser("prueba@test.com"))
                
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
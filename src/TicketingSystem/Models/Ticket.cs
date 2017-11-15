using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketingSystem.Models
{
    public class Ticket
    {

        private static int id = 0;
        public enum EnumStatus
        {
            Open,
            Closed
        }
        public string Title { get; set; }
        public string Body { get; set; }
        public EnumStatus Status { get; set; }
        public User Author { get; }
        public User Assignee { get; set; }
        public DateTime Created { get; }
        public int Id { get; }
        public Ticket(string title,string body, User author, User assignee)
        {
            id++;
            Id = id;
            Title = title;
            Body = body;
            Status = EnumStatus.Open;
            Author = author;
            Assignee = assignee;
            Created = DateTime.Now;
        }
    }
}
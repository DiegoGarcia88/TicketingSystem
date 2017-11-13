using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketingSystem.Models
{
    public class Ticket
    {

        public enum EnumStatus
        {
            Open,
            Closed
        }
        public string Title { get; set; }
        public string Body { get; set; }
        public EnumStatus Status { get; set; }
        public User Author { get; set; }
        public User Assignee { get; set; }
        public DateTime Created { get; set; }

        public Ticket(string title,string body, User author, User assignee)
        {
            Title = title;
            Body = body;
            Status = EnumStatus.Open;
            Author = Author;
            Assignee = assignee;
            Created = DateTime.Now;
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage ="Title is Required")]
        [MinLength(1, ErrorMessage = "Title is too short")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Body is Required")]
        [MinLength(1, ErrorMessage = "Body is too short")]
        public string Body { get; set; }
        public EnumStatus Status { get; set; }
        public User Author { get; set; }
        public User Assignee { get; set; }
        public DateTime Created { get; set; }
        public int Id { get; set; }
        public Ticket(string title,string body, int status, User author, User assignee)
        {
            id++;
            Id = id;
            Title = title;
            Body = body;
            Status = (EnumStatus)status;
            Created = DateTime.Now;
            Author = author;
            Assignee = assignee;
                        
        }

        //For temporary tickets, doesn't increase the ID and doesn't populate unnecessary fields
        public Ticket(User author, User assignee)
        {
            Created = DateTime.Now;
            Author = author;
            Assignee = assignee;
        }


    }
}
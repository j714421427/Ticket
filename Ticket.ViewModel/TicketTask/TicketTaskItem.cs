using System;
using System.Collections.Generic;
using System.Text;
using Ticket.Model.Enums;

namespace Ticket.ViewModel.TickeyTask
{
    public class TicketTaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TaskType { get; set; }
        public string TaskStatus { get; set; }
        public EntityStatus Status {get;set;}
    }
}

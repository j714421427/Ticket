using System;
using System.Collections.Generic;
using System.Text;
using Ticket.Model.Enums;

namespace Ticket.Model
{
    public class TicketTack : CreatableEntity, IEntityStatus
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public TicketTaskType TicketTaskType { get; set; }
        public TicketTaskStatus TicketTaskStatus { get; set; }
        public EntityStatus Status { get; set; }
    }
}

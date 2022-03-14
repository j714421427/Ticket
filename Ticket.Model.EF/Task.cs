using System;
using System.Collections.Generic;
using System.Text;
using Ticket.Model.Enums;

namespace Ticket.Model
{
    public class Task : CreatableEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public TaskType Type { get; set; }
        public TaskStatus Status { get; set; }
    }
}

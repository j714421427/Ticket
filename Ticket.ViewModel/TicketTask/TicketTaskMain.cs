using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.ViewModel.TickeyTask
{
    public class TicketTaskMain
    {
        public TicketTaskMain() {
            Items = new List<TicketTaskItem>();
        }
        public List<TicketTaskItem> Items { get; set; }
    }
}

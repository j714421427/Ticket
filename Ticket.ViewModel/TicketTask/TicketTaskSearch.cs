using Ticket.Model.Enums;

namespace Ticket.ViewModel.TickeyTask
{
    public class TicketTaskSearch
    {
        public string Title { get; set; }
        public TicketTaskType? TaskType { get; set; }
        public TicketTaskStatus? TaskStatus { get; set; }
    }
}

using Ticket.Model.Enums;

namespace Ticket.ViewModel.TickeyTask
{
    public class TicketTaskDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public TicketTaskType TaskType { get; set; }
        public TicketTaskStatus TaskStatus { get; set; }
        public EntityStatus Status { get; set; }
    }
}

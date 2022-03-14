using Ticket.Model.Enums;

namespace Ticket.Model
{
    public interface IEntityStatus
    {
        EntityStatus Status { get; set; }
    }
}

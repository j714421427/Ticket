namespace Ticket.Model
{
    public interface ICreatable : ICreatedAt
    {
        int CreatedById { get; set; }
    }
}

namespace Ticket.Model
{
    public class Role : CreatableEntity, IEntityStatus
    {
        public string Name { get; set; }
        public IEntityStatus Status { get; set; }
    }
}

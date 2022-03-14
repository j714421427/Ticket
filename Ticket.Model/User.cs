using Ticket.Model.Enums;

namespace Ticket.Model
{
    public class User : BaseEntity, IEntityStatus
    {
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public EntityStatus Status { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}

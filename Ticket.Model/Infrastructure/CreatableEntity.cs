using System;

namespace Ticket.Model
{
    public class CreatableEntity : BaseEntity ,ICreatable
    {
        public int CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual User CreatedBy { get; set; }
    }
}

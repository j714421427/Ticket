using System.Collections.Generic;
using Ticket.Model.Enums;

namespace Ticket.Model
{
    public class Role : CreatableEntity, IEntityStatus
    {
        public string Name { get; set; }
        public EntityStatus Status { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}

using System.Collections.Generic;
using Ticket.Model.Enums;

namespace Ticket.Model
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; }
        public PermissionCode PermissionCode  { get; set; }
        public int PermissionGroupId { get; set; }

        public virtual PermissionGroup PermissionGroup { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}

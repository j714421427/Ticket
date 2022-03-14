using Ticket.Model.Enums;

namespace Ticket.Model
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; }
        public PermissionCode PermissionCode  { get; set; }
        public int PermissionGroupId { get; set; }
    }
}

using Ticket.Model.Enums;

namespace Ticket.Model
{
    public class PermissionGroup : BaseEntity
    {
        public string Name { get; set; }
        public PermissionGroupCode GroupCode { get; set; }
    }
}

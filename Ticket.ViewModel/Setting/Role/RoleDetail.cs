using Ticket.Model.Enums;

namespace Ticket.ViewModel.Setting.Role
{
    public class RoleDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public EntityStatus Status { get; set; }
    }
}

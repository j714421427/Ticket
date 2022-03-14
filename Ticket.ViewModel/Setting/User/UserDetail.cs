using Ticket.Model.Enums;

namespace Ticket.ViewModel.Setting.User
{
    public class UserDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }

        public EntityStatus Status { get; set; }
    }
}

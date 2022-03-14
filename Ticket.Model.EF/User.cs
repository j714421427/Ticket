namespace Ticket.Model
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
    }
}

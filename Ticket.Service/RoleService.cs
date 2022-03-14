using Ticket.Model;

namespace Ticket.Service
{
    public class RoleService : StatusfulEntityService<Role>
    {
        public RoleService(TicketContext context) : base(context){}

        public void Update(Role item)
        {
            if (item.Id == 0)
            {
                context.Roles.Add(item);
            }
            else
            {
                var entity = GetById(item.Id, context);

                entity.Name = item.Name;
                entity.Status = item.Status;
            }
            context.SaveChanges();
        }
    }
}

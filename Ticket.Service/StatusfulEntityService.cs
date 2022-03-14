using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Ticket.Model;
using Ticket.Model.Enums;

namespace Ticket.Service
{
    public abstract class StatusfulEntityService<T> : EntityService<T>
        where T : class, IEntity, IEntityStatus
    {
        protected StatusfulEntityService(TicketContext context) : base(context) { }

        public virtual IEnumerable<T> GetAllEnabled()
        {
            return context.Set<T>().AsNoTracking().Where(q => q.Status == EntityStatus.Enabled).ToList();
        }

        public virtual void UpdateEntityStatus(int id, EntityStatus status)
        {
            var entity = GetById(id, context);

            entity.Status = status;

            context.SaveChanges();
        }
    }
}

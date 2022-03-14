using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Model;

namespace Ticket.Service
{
    public class TicketTaskService : StatusfulEntityService<TicketTack>
    {
        public TicketTaskService(TicketContext context) : base(context) { }

        public void Update(TicketTack item)
        {
            if (item.CreatedById == 0)
            {
                item.CreatedById = 1;
            }

            if (item.Id == 0)
            {
                context.TicketTasks.Add(item);
            }
            else
            {
                var entity = GetById(item.Id, context);

                entity.Title = item.Title;
                entity.Content = item.Content;
                entity.TicketTaskType = item.TicketTaskType;
                entity.TicketTaskStatus = item.TicketTaskStatus;
            }
            context.SaveChanges();
        }

        public void Resolve(int id)
        {
            var entity = GetById(id, context);

            entity.TicketTaskStatus = Model.Enums.TicketTaskStatus.Rejected;

            context.SaveChanges();
        }
    }
}

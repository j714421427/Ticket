using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Model.Enums;
using Ticket.ViewModel.Common;

namespace Ticket.Controllers
{
    public class OptionController : ExceptionController
    {
        public IEnumerable<OptionItem> GetTaskTypeAll()
        {
            return GetOptionItemsByEnum<TicketTaskType>();
        }

        public IEnumerable<OptionItem> GetTaskStatusAll()
        {
            return GetOptionItemsByEnum<TicketTaskStatus>();
        }

        public IEnumerable<OptionItem> GetEntityStatus()
        {
            var result = new List<OptionItem>();
            foreach (var item in new List<EntityStatus>() { EntityStatus.Enabled, EntityStatus.Disabled })
            {
                result.Add(new OptionItem()
                {
                    Id = (int)item,
                    StatusTitle = item.ToString(),
                    StatusText = item.ToString(),
                    OptionTitle = item.ToString(),
                    OptionText = item.ToString(),
                });
            }

            return result;
        }

        private IEnumerable<OptionItem> GetOptionItemsByEnum<T>()
        {
            var result = new List<OptionItem>();

            foreach (var item in Enum.GetValues(typeof(T)))
            {
                result.Add(new OptionItem()
                {
                    Id = (int)item,
                    StatusTitle = item.ToString(),
                    StatusText = item.ToString(),
                    OptionTitle = item.ToString(),
                    OptionText = item.ToString(),
                });
            }

            return result;
        }
    }
}

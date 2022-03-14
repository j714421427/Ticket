using System;
using System.Collections.Generic;
using System.Text;
using Ticket.Model.Enums;

namespace Ticket.ViewModel.Setting.User
{
    public class UserItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public EntityStatus Status {get;set;}
    }
}

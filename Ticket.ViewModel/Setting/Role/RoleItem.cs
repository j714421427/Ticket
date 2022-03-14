using System;
using System.Collections.Generic;
using System.Text;
using Ticket.Model.Enums;

namespace Ticket.ViewModel.Setting.Role
{
    public class RoleItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EntityStatus Status {get;set;}
    }
}

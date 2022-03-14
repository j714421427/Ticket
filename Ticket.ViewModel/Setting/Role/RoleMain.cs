using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.ViewModel.Setting.Role
{
    public class RoleMain
    {
        public RoleMain() {
            Items = new List<RoleItem>();
        }
        public List<RoleItem> Items { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.ViewModel.Setting.User
{
    public class UserMain
    {
        public UserMain() {
            Items = new List<UserItem>();
        }
        public List<UserItem> Items { get; set; }
    }
}

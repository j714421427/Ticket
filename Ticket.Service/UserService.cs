using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Model;

namespace Ticket.Service
{
    public class UserService : StatusfulEntityService<User>
    {
        public UserService(TicketContext context) : base(context) { }

        public void Update(User item)
        {
            if (item.Id == 0)
            {
                context.Users.Add(item);
            }
            else
            {
                var entity = GetById(item.Id, context);

                entity.Name = item.Name;
                entity.Account = item.Account;
                entity.Password = item.Password;
                entity.Status = item.Status;
            }
            context.SaveChanges();
        }

        public User Login(string account, string password)
        {
            var entity = context.Users.SingleOrDefault(
                u => u.Account == account);
            if (entity == null)
            {
                throw new Exception("InvalidLogin");
            }

            if (entity.Password != password) //todo password hash
            {
                throw new Exception("InvalidLogin");
            }

            return entity;
        }
    }
}

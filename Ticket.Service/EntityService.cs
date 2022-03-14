using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Ticket.Model;

namespace Ticket.Service
{
    public class EntityService<T>
        where T : class, IEntity
    {
        protected TicketContext context = null;

        public EntityService(TicketContext context)
        {
            this.context = context;
        }

        public virtual T Add(T item)
        {
            context.Set<T>().Add(item);

            context.SaveChanges();

            return item;
        }

        public virtual void AddRange(IEnumerable<T> items)
        {
            context.Set<T>().AddRange(items);

            context.SaveChanges();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return context.Set<T>().AsNoTracking().ToList();
        }

        public virtual T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public virtual T GetById(int id, params string[] includes)
        {
            IQueryable<T> result = context.Set<T>().AsNoTracking();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    result = result.Include(include);
                }
            }

            result = result.Where(q => q.Id == id);

            return result.SingleOrDefault();
        }

        public virtual T GetById(int id, DbContext ctx)
        {
            return ctx.Set<T>().Find(id);
        }

        public virtual IEnumerable<T> GetAllQuery(
            IEnumerable<Expression<Func<T, bool>>> query = null,
            string orderBy = null,
            bool isAscending = false,
            params string[] includes)
        {
            IQueryable<T> result = context.Set<T>().AsNoTracking();

            if (query != null)
            {
                foreach (var predicate in query)
                {
                    result = result.Where(predicate);
                }
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    result = result.Include(include);
                }
            }

            var data = OrderBy(result, orderBy, isAscending);

            return data.ToList();
        }

        protected virtual IQueryable<T> OrderBy(
            IQueryable<T> source,
            string orderBy = null,
            bool isAscending = false)
        {
            IOrderedQueryable<T> queryable = null;

            if (orderBy != null && orderBy.Length > 0)
            {
                var param = Expression.Parameter(typeof(T));
                var property = Expression.PropertyOrField(param, orderBy);
                var sort = Expression.Lambda(property, param);

                var call = Expression.Call(
                    typeof(Queryable),
                    isAscending ? "OrderBy" : "OrderByDescending",
                    new[] { typeof(T), property.Type },
                    source.Expression,
                    Expression.Quote(sort));

                queryable = (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
            }
            else
            {
                queryable = isAscending ?
                    source.OrderBy(c => c.Id) :
                    source.OrderByDescending(c => c.Id);
            }

            return queryable;
        }

    }
}

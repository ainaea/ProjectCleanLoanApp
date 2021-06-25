using Loan.ApplicationCore.Interfaces.Repositories;
using Loan.Infrastructure.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Loan.Infrastructure.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly LoanDbContext dbContext;
        private DbSet<T> dbSet;

        public Repository(LoanDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }
        public async Task Add(T entity)
        {
            await dbSet.AddAsync(entity);
        }
                
        public async Task<T> Find(string id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter, string[] includes = null)
        {
            IQueryable<T> query = GetQuerry(includes);
            return await query.Where(filter).FirstOrDefaultAsync();
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll(string[] includes = null, int count = 20)
        {
            IQueryable<T> query = GetQuerry(includes);
            return await query.Take(count).ToListAsync();
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter, string[] includes = null)
        {
            IQueryable<T> query = GetQuerry(includes);
            return await query.Where(filter).ToListAsync();
            //throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
            //throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
            //throw new NotImplementedException();
        }

        private IQueryable<T> GetQuerry(string[] includes)
        {
            IQueryable<T> query = dbSet;
            if (includes != null)
            {
                foreach (var child in includes)
                {
                    query = query.Include(child);
                }
            }
            return query;
        }
    }
}

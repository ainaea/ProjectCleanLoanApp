using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Loan.ApplicationCore.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> Find(string id);
        Task<T> Get(Expression<Func<T, bool>> filter, string[] includes = null);
        Task<IEnumerable<T>> GetAll(string[] includes = null, int count = 20);
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter, string[] includes = null);
        Task Add(T entity);
        void Remove(T entity);
        void Update(T entity);
    }
}

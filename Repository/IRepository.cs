using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace QuizPlatform.Repository
{
    public interface IRepository<T> where T : class
    {
        T Add<T>(T entity);
        Task<T> AddAsync<T>(T entity);
        T Update(T entity);
        void Delete<T>(T entity);
        T Get(Expression<Func<T, bool>> where);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetAsync(Expression<Func<T, bool>> where);
    }
}



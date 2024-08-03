
using Microsoft.EntityFrameworkCore;
using QuizPlatform.Infrastructure;
using QuizPlatform.Models;
using System.Linq.Expressions;

namespace QuizPlatform.Repository
{
    public class BaseRepository<T> : IRepository<T> where T :class
    {
        protected IDbFactory DbFactory
        {
            get;
            private set;
        }
        public DbSet<T> DbSet { get; set; }
        public virtual void Delete(T entity)
        {
            DbSet.Remove(entity);
        }
        public BaseRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            DbSet = DbContext.Set<T>();
        }

        private QuizPlatformContext _dbContext;

        protected QuizPlatformContext DbContext
        {
            get { return _dbContext ?? (_dbContext = DbFactory.Initialize()); }
        }



        public T Add<T>(T entity)
        {
            _dbContext.Add(entity);
            return entity;
        }
        public async Task<T> AddAsync<T>(T entity)
        {
           await _dbContext.AddAsync(entity);
            return entity;
        }
        public T Get(Expression<Func<T, bool>> where)
        {
            return DbSet.FirstOrDefault<T>(where);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> where)
        {
            return await DbSet.FirstOrDefaultAsync<T>(where);
        }

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> where)
        {
            return DbSet.Where(where);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.AnyAsync(predicate);
        }
        public void Delete<T>(T entity)
        {
            _dbContext.Remove(entity);
        }
      
        public  T Update(T entity)
        {
            DbSet.Attach(entity);
            //_dbContext.Set<T>().AddOrUpdate(entity);            
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public List<T> Add<T>(List<T> entities)
        {
            if (entities != null && entities.Count() > 0)

                _dbContext.AddRange(entities);
            return entities;
        }
    }

    
}

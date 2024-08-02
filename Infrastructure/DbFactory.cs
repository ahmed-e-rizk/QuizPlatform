using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using QuizPlatform.Models;

namespace QuizPlatform.Infrastructure
{

    /// <summary>   A database factory. </summary>
    public class DbFactory : IDbFactory
    {
        /// <summary>   Context for the database. </summary>
        private QuizPlatformContext _dbContext { get; set; }
        private DbConnection _connection;
        private bool seeddata;
        public DbFactory(DbConnection connection)
        {
            _connection = connection;
            seeddata = true;
        }
        public DbFactory(QuizPlatformContext context)
        {
            _dbContext = context;
        }

        public QuizPlatformContext Initialize()
        {
            if (_dbContext == null)
            {
                _dbContext = new QuizPlatformContext();
            }

            _dbContext.Database.SetDbConnection(_connection);
            return _dbContext;
        }
    }
}

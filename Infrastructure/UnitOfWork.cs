﻿using Microsoft.EntityFrameworkCore.Storage;
using QuizPlatform.Models;
using System;
using System.Threading.Tasks;

namespace QuizPlatform.Infrastructure
{
    /// <summary>   A unit of work. </summary>
    public class UnitOfWork : IUnitOfWork //, IDisposable
    {
        /// <summary>   The database factory. </summary>
        private readonly IDbFactory _dbFactory;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="dbFactory">    The database factory. </param>
        ///-------------------------------------------------------------------------------------------------

        public UnitOfWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a context for the database. </summary>
        ///
        /// <value> The database context. </value>
        ///-------------------------------------------------------------------------------------------------

        public QuizPlatformContext DbContext
        {
            get
            {
                return _dbFactory.Initialize();
            }
        }

        /// <summary>   Commits this object. </summary>
        public void Commit()
        {
            try
            {
                DbContext.Commit();
            }
            catch (Exception ex)
            {

                Dispose();
                throw;
            }
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                return await DbContext.CommitAsync();
            }
            catch (Exception ex)
            {
                await DisposeAsync();
                throw;
            }
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }

        public async Task DisposeAsync()
        {
            await DbContext.DisposeAsync();
        }
    }
}

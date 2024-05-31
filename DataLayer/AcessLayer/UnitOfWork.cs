using DomainLayer.AcessLayer;
using DomainLayer.Data;
using LeaveManagement.Models;

namespace DomainLayer.UnitOfWork
{
    /// <summary>
    /// Implementation of the Unit of Work pattern for managing database operations.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Initializes a new instance of the UnitOfWork class with the provided database context.
        /// </summary>
        /// <param name="context">The database context to be used by the unit of work.</param>
        public UnitOfWork(LeaveDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Gets the database context associated with the unit of work.
        /// </summary>
        public LeaveDbContext Context { get; private set; }

        /// <summary>
        /// Saves changes made in the unit of work to the database.
        /// </summary>
        public void Save()
        {
            Context.SaveChanges();
        }
    }
}

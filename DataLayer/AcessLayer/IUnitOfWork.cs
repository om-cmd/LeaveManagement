using DomainLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace DomainLayer.AcessLayer
{
    /// <summary>
    /// Interface for Unit of Work pattern to manage database operations.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the database context associated with the unit of work.
        /// </summary>
        LeaveDbContext Context { get; }

        /// <summary>
        /// Saves changes made in the unit of work to the database.
        /// </summary>
        void Save();
    }
}

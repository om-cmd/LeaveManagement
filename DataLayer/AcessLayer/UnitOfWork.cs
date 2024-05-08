using DomainLayer.AcessLayer;
using DomainLayer.Data;
using LeaveManagement.Models;

namespace DomainLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(LeaveDbContext context)
        {
            Context = context;
            
        }

        public LeaveDbContext Context { get; private set; }
      

        public void Save()
        {
            Context.SaveChanges();
        }
    }
   
}

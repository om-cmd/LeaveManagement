using DomainLayer.Data;
using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace DomainLayer.AcessLayer
{
    public interface IUnitOfWork
    {
        public LeaveDbContext Context { get; } 
     
        public void Save();      

    }
}

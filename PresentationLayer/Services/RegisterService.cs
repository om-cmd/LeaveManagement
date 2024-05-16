using DomainLayer.Interface.IService;
using DomainLayer.IRepoInterface.IRepo;
using DomainLayer.ViewModels;
using LeaveManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    
    public class RegisterService : IRegisterService
    {
        private readonly IRegisterRepo _repo;
        public RegisterService(IRegisterRepo repo)
        {
            _repo = repo;
        }

        public Person Register(RegisterViewModel register)
        {
            return _repo.Register(register);
        }
    }
}

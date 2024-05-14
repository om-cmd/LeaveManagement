using DomainLayer.Interface.IService;
using DomainLayer.IRepoInterface.IRepo;
using LeaveManagement.Models;
using PresentationLayer.VIewModels;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo _repo;
        public EmployeeService(IEmployeeRepo repo)
        {
            _repo = repo;
        }

        public Employee CreateEmployee(EmployeeViewModel model)
        {
            return _repo.CreateEmployee(model);
        }

        public EmployeeViewModel DeleteEmployee(int id)
        {
            return _repo.DeleteEmployee(id);
        }

        public ICollection<EmployeeViewModel> EmployeeList()
        {
            return _repo.EmployeeList();
        }

        public EmployeeViewModel GetEmployee(int id)
        {
            return _repo.GetEmployee(id);
        }

        public Employee UpdateEmployee(EmployeeViewModel model, int id)
        {
            return _repo.UpdateEmployee(model,id);
        }
    }
}

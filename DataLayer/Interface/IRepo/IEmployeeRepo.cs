using LeaveManagement.Models;
using PresentationLayer.VIewModels;

namespace DomainLayer.IRepoInterface.IRepo
{
    public interface IEmployeeRepo
    {
        ICollection<EmployeeViewModel> EmployeeList();
        EmployeeViewModel GetEmployee(int id);
        Employee CreateEmployee(EmployeeViewModel model);
        Employee UpdateEmployee(EmployeeViewModel model,int id);
        EmployeeViewModel DeleteEmployee(int id);
    }
}

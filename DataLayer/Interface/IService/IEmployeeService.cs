using LeaveManagement.Models;
using PresentationLayer.VIewModels;

namespace DomainLayer.Interface.IService
{
    public interface IEmployeeService
    {
        ICollection<EmployeeViewModel> EmployeeList();
        EmployeeViewModel GetEmployee(int id);
        Employee UpdateEmployee(EmployeeViewModel model,int id);
        EmployeeViewModel DeleteEmployee(int id);
    }
}

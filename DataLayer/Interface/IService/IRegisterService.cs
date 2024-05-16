using DomainLayer.ViewModels;
using LeaveManagement.Models;

namespace DomainLayer.Interface.IService
{
    public interface IRegisterService
    {
        Person Register(RegisterViewModel register);
    }
}

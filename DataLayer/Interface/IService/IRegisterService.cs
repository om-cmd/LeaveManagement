using DomainLayer.ViewModels;
using LeaveManagement.Models;

namespace DomainLayer.Interface.IService
{
    public interface IRegisterService
    {
        User Register(RegisterViewModel register);
    }
}

using DomainLayer.ViewModels;
using LeaveManagement.Models;

namespace DomainLayer.Interface.IService
{
    public interface ILoginService
    {
        User Login(LoginViewModel loginViewModel);
    }
}

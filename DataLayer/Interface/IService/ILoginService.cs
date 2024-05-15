using DomainLayer.ViewModels;
using LeaveManagement.Models;

namespace DomainLayer.Interface.IService
{
    public interface ILoginService
    {
        JWTTokenViewModels Login(LoginViewModel loginViewModel);
    }
}

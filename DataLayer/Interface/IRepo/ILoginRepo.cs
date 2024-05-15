using DomainLayer.ViewModels;
using LeaveManagement.Models;

namespace DomainLayer.IRepoInterface.IRepo
{
    public interface ILoginRepo
    {
        JWTTokenViewModels Login(LoginViewModel login);

    }
}

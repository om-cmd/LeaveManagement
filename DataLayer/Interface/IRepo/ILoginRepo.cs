using DomainLayer.ViewModels;
using LeaveManagement.Models;

namespace DomainLayer.IRepoInterface.IRepo
{
    public interface ILoginRepo
    {
        User Login(LoginViewModel login);

    }
}

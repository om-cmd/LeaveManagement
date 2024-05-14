using DomainLayer.ViewModels;
using LeaveManagement.Models;

namespace DomainLayer.IRepoInterface.IRepo
{
    public interface IRegisterRepo
    {
        User Register(RegisterViewModel register);
    }
}

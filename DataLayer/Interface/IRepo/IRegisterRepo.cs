using DomainLayer.ViewModels;
using LeaveManagement.Models;

namespace DomainLayer.IRepoInterface.IRepo
{
    public interface IRegisterRepo
    {
        Person Register(RegisterViewModel register);
    }
}

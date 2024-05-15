using LeaveManagement.Models;
using PresentationLayer.ViewModels;

namespace DomainLayer.Interface.IService
{
    public interface ICalanerService
    {
        CalanderViewModel GetHoliday(int id);
        ICollection<CalanderViewModel> CalanderList();
        Calander AddHoliday(CalanderViewModel Holiday);

        CalanderViewModel RemoveHoliday(int id);
    }
}

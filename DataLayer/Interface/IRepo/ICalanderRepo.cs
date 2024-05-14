using LeaveManagement.Models;
using PresentationLayer.ViewModels;

namespace DomainLayer.IRepoInterface.IRepo
{
    public interface ICalanderRepo
    {
        CalanderViewModel GetHoliday(int id);
        ICollection<CalanderViewModel> CalanderList(DateTime startDate, DateTime endDate);
        Calander AddHoliday(CalanderViewModel Holiday);

        CalanderViewModel RemoveHoliday(int id);


    }
}

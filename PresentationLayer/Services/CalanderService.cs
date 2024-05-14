using DomainLayer.Interface.IService;
using DomainLayer.IRepoInterface.IRepo;
using LeaveManagement.Models;
using PresentationLayer.ViewModels;

namespace BusinessLayer.Services
{
    public class CalanderService : ICalanerService
    {
        private readonly ICalanderRepo _calanderRepo;
        public CalanderService(ICalanderRepo calanderRepo)
        {
            _calanderRepo = calanderRepo;
        }
        public Calander AddHoliday(CalanderViewModel Holiday)
        {
            return _calanderRepo.AddHoliday(Holiday);

        }

        public ICollection<CalanderViewModel> CalanderList(DateTime startDate, DateTime endDate)
        {
            return _calanderRepo.CalanderList(startDate, endDate);
        }

        public CalanderViewModel GetHoliday(int id)
        {
            return _calanderRepo.GetHoliday(id);
        }

        public CalanderViewModel RemoveHoliday(int id)
        {
            return _calanderRepo.RemoveHoliday(id);
        }
    }
}

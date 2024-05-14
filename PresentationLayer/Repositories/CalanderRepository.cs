using AutoMapper;
using DomainLayer.AcessLayer;
using DomainLayer.IRepoInterface.IRepo;
using LeaveManagement.Models;
using PresentationLayer.ViewModels;

namespace BusinessLayer.Repositories
{
    public class CalanderRepository : ICalanderRepo
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CalanderRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Calander AddHoliday(CalanderViewModel Holiday)
        {
            var holiday = _mapper.Map<Calander>(Holiday);
            holiday.IsPublicHoliday = true;
            _unitOfWork.Context.HolidayCalanders.Add(holiday);
            _unitOfWork.Context.SaveChanges();
            return holiday;
        }

        public ICollection<CalanderViewModel> CalanderList(DateTime startDate, DateTime endDate)
        {
            var holidays = _unitOfWork.Context.HolidayCalanders
               .Where(h => h.Date >= startDate && h.Date <= endDate)
               .ToList();

            return _mapper.Map<List<CalanderViewModel>>(holidays);
        }

        public CalanderViewModel GetHoliday(int id)
        {
            var holiday = _unitOfWork.Context.HolidayCalanders.Find(id);
            return _mapper.Map<CalanderViewModel>(holiday);
        }

        public CalanderViewModel RemoveHoliday(int id)
        {
            var holiday = _unitOfWork.Context.HolidayCalanders.Find(id);
            if (holiday != null)
            {
                holiday.IsPublicHoliday = false;
                _unitOfWork.Context.HolidayCalanders.Remove(holiday);
                _unitOfWork.Context.SaveChanges();
            }
            return _mapper.Map<CalanderViewModel>(holiday);
        }
    }
}

using AutoMapper;
using DomainLayer.AcessLayer;
using DomainLayer.IRepoInterface.IRepo;
using LeaveManagement.Models;
using PresentationLayer.ViewModels;

namespace BusinessLayer.Repositories
{
    /// <summary>
    /// Provides methods to manage holiday calendar entries.
    /// </summary>
    public class CalanderRepository : ICalanderRepo
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalanderRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for data access operations.</param>
        /// <param name="mapper">The mapper for object-object mapping.</param>
        public CalanderRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds a new holiday to the calendar.
        /// </summary>
        /// <param name="Holiday">The holiday view model to add.</param>
        /// <returns>The added holiday entity.</returns>
        public Calander AddHoliday(CalanderViewModel Holiday)
        {
            var holiday = _mapper.Map<Calander>(Holiday);
            holiday.IsPublicHoliday = true;
            _unitOfWork.Context.HolidayCalanders.Add(holiday);
            _unitOfWork.Context.SaveChanges();
            return holiday;
        }

        /// <summary>
        /// Retrieves a list of public holidays from the calendar.
        /// </summary>
        /// <returns>A collection of holiday view models.</returns>
        public ICollection<CalanderViewModel> CalanderList()
        {
            var holidays = _unitOfWork.Context.HolidayCalanders
                .Where(h => h.IsPublicHoliday == true)
                .ToList();

            return _mapper.Map<List<CalanderViewModel>>(holidays);
        }

        /// <summary>
        /// Gets the details of a specific holiday by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the holiday.</param>
        /// <returns>The holiday view model.</returns>
        public CalanderViewModel GetHoliday(int id)
        {
            var holiday = _unitOfWork.Context.HolidayCalanders.Find(id);
            return _mapper.Map<CalanderViewModel>(holiday);
        }

        /// <summary>
        /// Removes a holiday from the calendar.
        /// </summary>
        /// <param name="id">The identifier of the holiday to remove.</param>
        /// <returns>The removed holiday view model.</returns>
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

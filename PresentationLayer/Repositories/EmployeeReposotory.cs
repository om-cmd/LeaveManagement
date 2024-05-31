using AutoMapper;
using DomainLayer.AcessLayer;
using DomainLayer.IRepoInterface.IRepo;
using LeaveManagement.Models;
using PresentationLayer.VIewModels;

namespace BusinessLayer.Repositories
{
    /// <summary>
    /// used to manage employee
    /// </summary>
   
    public class EmployeeReposotory : IEmployeeRepo
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        /// <summary>
        /// used for dependency injection
        /// </summary>
        /// <param name="unitOfWork">The unit of work for data access operations</param>
        /// <param name="mapper">The mapper for object-object mapping</param>

        public EmployeeReposotory(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        /// <summary>
        /// deletes the employee
        /// </summary>
        /// <param name="id">get id and deletes it </param>
        /// <returns>MAPS the updated model after delte</returns>
     

        public EmployeeViewModel DeleteEmployee(int id)
        {
            var existingEmployee = _unitOfWork.Context.Employee.FirstOrDefault(x => x.Id == id);
            if (existingEmployee == null)
            {
                return null;
            }
            existingEmployee.Status = DomainLayer.Models.Status.InActive;
            existingEmployee.LeftDate = DateTime.Now;
            _unitOfWork.Context.SaveChanges();
            return _mapper.Map<EmployeeViewModel>(existingEmployee);
        }
        /// <summary>
        /// list of the employee
        /// </summary>
        /// <returns>returns  list of employee which are active</returns>
      

        public ICollection<EmployeeViewModel> EmployeeList()
        {
            var employees = _unitOfWork.Context.Employee.Where(emp => emp.Status != DomainLayer.Models.Status.InActive).ToList();
            return _mapper.Map<List<EmployeeViewModel>>(employees);
        }

        /// <summary>
        /// this is used to get the single employee
        /// </summary>
        /// <param name="id">takes id as parameter to find it from database</param>
        /// <returns>return the id that have been called </returns>
        public EmployeeViewModel GetEmployee(int id)
        {
            var employee = _unitOfWork.Context.Employee.Find(id);
            return _mapper.Map<EmployeeViewModel>(employee);
        }

        /// <summary>
        /// for the update of employee or edit its informations
        /// </summary>
        /// <param name="model">the viewModels params are used </param>
        /// <param name="id">takes id as parameter to find it from database</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public Employee UpdateEmployee(EmployeeViewModel model, int id)
        {
            var existingEmployee = _unitOfWork.Context.Employee.FirstOrDefault(x => x.Id == id);
            if (existingEmployee == null)
            {
                throw new KeyNotFoundException("Employee not found.");
            }
            _mapper.Map(model, existingEmployee);
            _unitOfWork.Context.SaveChanges();
            return existingEmployee;
        }
    }
}

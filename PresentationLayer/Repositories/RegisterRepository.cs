using AutoMapper;
using BusinessLayer.Helper;
using DomainLayer.AcessLayer;
using DomainLayer.IRepoInterface.IRepo;
using DomainLayer.ViewModels;
using LeaveManagement.Models;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Repositories
{
    public class RegisterRepository : IRegisterRepo
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RegisterRepository(IUnitOfWork unitOfWork, IMapper mapper,IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public Person Register(RegisterViewModel register)
        {
            var existingUser = _unitOfWork.Context.Users.FirstOrDefault(u => u.Email == register.Email);
            if (existingUser != null)
            {
                throw new Exception("user already exist");
            }
            string HashPassword = PasswordHash.Hashing(register.Password);

            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
            {
                throw new Exception("The CreatedBy field cannot be null or empty.");
            }

            // Map the register view model to the employee entity
            Employee employee = _mapper.Map<Employee>(register);
            employee.Password = HashPassword;
            employee.JoinedDate = DateTime.Now;
            employee.CreatedBy = userName;

            // Map the register view model to the user entity
            User user = _mapper.Map<User>(register);
            user.Password = HashPassword;
            user.JoinedDate = DateTime.Now;
            user.CreatedBy = userName;

            // Add the new entities to the context
            _unitOfWork.Context.Employee.Add(employee);
            _unitOfWork.Context.Users.Add(user);

            // Save changes to the database
            _unitOfWork.Context.SaveChanges();

            return employee;

        }
    }
}

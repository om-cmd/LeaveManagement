using AutoMapper;
using BusinessLayer.Helper;
using DomainLayer.AcessLayer;
using DomainLayer.IRepoInterface.IRepo;
using DomainLayer.ViewModels;
using LeaveManagement.Models;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Repositories
{
    /// <summary>
    /// Provides methods for user registration.
    /// </summary>
    public class RegisterRepository : IRegisterRepo
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work for data access operations.</param>
        /// <param name="mapper">The mapper for object-object mapping.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor to get the current user.</param>
        public RegisterRepository(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="register">The register view model containing user details.</param>
        /// <returns>The registered employee entity.</returns>
        /// <exception cref="Exception">Thrown when the user already exists or the CreatedBy field is null or empty.</exception>
        public Person Register(RegisterViewModel register)
        {
            var existingUser = _unitOfWork.Context.Users.FirstOrDefault(u => u.Email == register.Email);
            if (existingUser != null)
            {
                throw new Exception("User already exists.");
            }

            string hashedPassword = PasswordHash.Hashing(register.Password);

            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
            {
                throw new Exception("The CreatedBy field cannot be null or empty.");
            }

            // Map the register view model to the employee entity
            Employee employee = _mapper.Map<Employee>(register);
            employee.Password = hashedPassword;
            employee.JoinedDate = DateTime.Now;
            employee.CreatedBy = userName;

            // Map the register view model to the user entity
            User user = _mapper.Map<User>(register);
            user.Password = hashedPassword;
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

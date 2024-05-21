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

            var person = _mapper.Map<Person>(register);
            person.JoinedDate = DateTime.Now;
            person.CreatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            person.Password = HashPassword;

            Employee employee = _mapper.Map<Employee>(register);
            User users = _mapper.Map<User>(register);
            _unitOfWork.Context.Employee.Add(employee);
            _unitOfWork.Context.Users.Add(users);
            _unitOfWork.Context.SaveChanges();

            return person;

        }
    }
}

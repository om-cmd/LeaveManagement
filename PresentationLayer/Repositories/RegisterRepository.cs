using AutoMapper;
using DomainLayer.AcessLayer;
using DomainLayer.IRepoInterface.IRepo;
using DomainLayer.ViewModels;
using LeaveManagement.Models;

namespace BusinessLayer.Repositories
{
    public class RegisterRepository : IRegisterRepo
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RegisterRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Person Register(RegisterViewModel register)
        {


            var existingUser = _unitOfWork.Context.Users.FirstOrDefault(u => u.Email == register.Email);
            if (existingUser != null)
            {
                throw new Exception("user already exist");
            }

            var person = _mapper.Map<Person>(register);
            person.JoinedDate = DateTime.Now;
            // Additional logic for user creation, such as password hashing, 

            //_unitOfWork.Context.People.Add(person);
            Employee employee = _mapper.Map<Employee>(register);
            User users = _mapper.Map<User>(register);
            _unitOfWork.Context.Employee.Add(employee);
            _unitOfWork.Context.Users.Add(users);
            _unitOfWork.Context.SaveChanges();

            return person;

        }
    }
}

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
        public User Register(RegisterViewModel register)
        {
            try
            {
                var existingUser = _unitOfWork.Context.Users.FirstOrDefault(u => u.Email == register.Email);
                if (existingUser != null)
                {
                    throw new Exception("user already exist");
                }

                var user = _mapper.Map<User>(register);
                user.DateCreated = DateTime.Now;
                // Additional logic for user creation, such as password hashing, 

                _unitOfWork.Context.Users.Add(user);
                _unitOfWork.Context.SaveChanges();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during registration", ex);
            }
        }
    }
}

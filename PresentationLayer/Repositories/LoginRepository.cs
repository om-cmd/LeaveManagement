using AutoMapper;
using BusinessLayer.Middleware;
using DomainLayer.AcessLayer;
using DomainLayer.IRepoInterface.IRepo;
using DomainLayer.ViewModels;
using LeaveManagement.Models;

namespace BusinessLayer.Repositories
{
    public class LoginRepository : ILoginRepo
    {
        private readonly IUnitOfWork _unitOfWork;
        private Authentication _middleware;

        //private readonly BusinessLayer.Middleware.Authentication _middleware;
        private readonly IMapper _mapper;
        public LoginRepository(IUnitOfWork unitOfWork, IMapper mapper, Authentication middleware)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _middleware = middleware;
        }
        public JWTTokenViewModels Login(LoginViewModel login)
        {
           
                var authenticatedUser = _unitOfWork.Context.Users.FirstOrDefault(x => x.UserName == login.UserName);

                if (authenticatedUser == null || authenticatedUser.Password != login.Password)
                {
                    throw new Exception("not matched"); 
                }

                var user = _mapper.Map<User>(authenticatedUser);
                var token = _middleware.ProvideBothToken(user);

                var model = new JWTTokenViewModels
                {
                    AcessTokens = token.AcessToken.ToString(),
                    RefreshTokens = token.RefreshToken.ToString(),
                };
                return model;
          
        }
    }
}

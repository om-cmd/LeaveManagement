using AutoMapper;
using BusinessLayer.Middleware;
using DomainLayer.AcessLayer;
using DomainLayer.IRepoInterface.IRepo;
using DomainLayer.ViewModels;
using LeaveManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public User Login(LoginViewModel login)
        {
            try
            {
                var authenticatedUser = _unitOfWork.Context.Users.FirstOrDefault(x => x.UserName == login.UserName);

                if (authenticatedUser == null || authenticatedUser.Password != login.Password)
                {
                    return null; // Return null if user not found or password is incorrect
                }

                var user = _mapper.Map<User>(authenticatedUser);
                var (refreshToken, accessToken) = _middleware.ProvideBothToken(user);

                // Optionally, you can return the access token and refresh token here
                // return new { AccessToken = accessToken, RefreshToken = refreshToken };

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred during login", ex);
            }
        }
    }
}

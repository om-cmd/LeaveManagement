using AutoMapper;
using BusinessLayer.Helper;
using BusinessLayer.Middleware;
using DomainLayer.AcessLayer;
using DomainLayer.IRepoInterface.IRepo;
using DomainLayer.ViewModels;
using LeaveManagement.Models;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Repositories
{
    /// <summary>
    /// Provides methods for user authentication and token generation.
    /// </summary>
    public class LoginRepository : ILoginRepo
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Authentication _middleware;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// used for injection
        /// </summary>
        /// <param name="unitOfWork">The unit of work for data access operations</param>
        /// <param name="mapper">The mapper for object-object mapping</param>
        /// <param name="middleware">Used for authentication</param>
        /// <param name="httpContextAccessor">Accessor for HTTP context</param>
        public LoginRepository(IUnitOfWork unitOfWork, IMapper mapper, Authentication middleware, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _middleware = middleware;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// for login 
        /// </summary>
        /// <param name="login">takes the view model for parameters</param>
        /// <returns>updated model</returns>
        /// <exception cref="Exception"></exception>
        public JWTTokenViewModels Login(LoginViewModel login)
        {
            // Validate the CAPTCHA
            var storedCaptcha = _httpContextAccessor.HttpContext.Session.GetString("CaptchaCode");

            if (storedCaptcha == null || login.Captcha != storedCaptcha)
            {
                throw new Exception("Invalid CAPTCHA.");
            }

            string hashedPassword = PasswordHash.Hashing(login.Password);
            var authenticatedUser = _unitOfWork.Context.Users.FirstOrDefault(x => x.UserName == login.UserName);

            if (authenticatedUser == null || authenticatedUser.Password != hashedPassword)
            {
                throw new Exception("Username and password do not match.");
            }

            var user = _mapper.Map<User>(authenticatedUser);
            var token = _middleware.ProvideBothToken(user);

            var model = new JWTTokenViewModels
            {
                AcessTokens = token.AccessToken.ToString(),
                RefreshTokens = token.RefreshToken.ToString(),
            };

            // Optionally, remove the CAPTCHA from the session after successful validation
            _httpContextAccessor.HttpContext.Session.Remove("CaptchaCode");

            return model;
        }
    }
}

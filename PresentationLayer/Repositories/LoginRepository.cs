using AutoMapper;
using BusinessLayer.Helper;
using BusinessLayer.Middleware;
using DomainLayer.AcessLayer;
using DomainLayer.IRepoInterface.IRepo;
using DomainLayer.Models;
using DomainLayer.ViewModels;
using LeaveManagement.Models;

namespace BusinessLayer.Repositories
{
    /// <summary>
    /// Provides methods for user authentication and token generation.
    /// </summary>

    public class LoginRepository : ILoginRepo
    {
        private readonly IUnitOfWork _unitOfWork;
        private Authentication _middleware;
        private readonly IMapper _mapper;
        /// <summary>
        /// used for injection
        /// </summary>
        /// <param name="unitOfWork">The unit of work for data access operations</param>
        /// <param name="mapper">The mapper for object-object mapping</param>
        /// <param name="middleware">Used for authentication</param>

        public LoginRepository(IUnitOfWork unitOfWork, IMapper mapper, Authentication middleware)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _middleware = middleware;
        }
        /// <summary>
        /// for login 
        /// </summary>
        /// <param name="login">takes the view model for parameterS</param>
        /// <returns>updated model</returns>
        /// <exception cref="Exception"></exception>
       
        public JWTTokenViewModels Login(LoginViewModel login)
        {
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

            return model;
        }
    }
}

using DomainLayer.Interface.IService;
using DomainLayer.IRepoInterface.IRepo;
using DomainLayer.ViewModels;
using LeaveManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepo _login;
        public LoginService(ILoginRepo login)
        {
            _login = login;
        }

        public JWTTokenViewModels Login(LoginViewModel loginViewModel)
        {
            return _login.Login(loginViewModel);
        }
    }
}

using BusinessLayer.AttributeValidations;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.ViewModels
{
    public class LoginViewModel 
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [ValidatePassword]
        public string Password { get; set; }
        [Required]
        public string Captcha { get; set; }

    }
}

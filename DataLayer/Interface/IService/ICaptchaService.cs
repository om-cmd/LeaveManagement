using Microsoft.AspNetCore.Mvc;

namespace DomainLayer.Interface.IService
{
    public interface ICaptchaService
    {
        public (string Code, byte[] ImageBytes) GenerateCaptcha();
        public IActionResult GetCaptcha();
    }
}

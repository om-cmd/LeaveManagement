using Microsoft.AspNetCore.Mvc;

namespace DomainLayer.Interface.IRepo
{
    public interface ICaptchaRepo
    {
        public (string Code, byte[] ImageBytes) GenerateCaptcha();
        public IActionResult GetCaptcha();

    }
}

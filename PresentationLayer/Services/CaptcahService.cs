using BusinessLayer.Repositories;
using DomainLayer.Interface.IService;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Services
{
    public class CaptcahService : ICaptchaService
    {
        private readonly CaptchaGenerator _captchaGenerator;

        public CaptcahService(CaptchaGenerator captchaGenerator)
        {
            _captchaGenerator = captchaGenerator;
        }

        public (string Code, byte[] ImageBytes) GenerateCaptcha()
        {
            return _captchaGenerator.GenerateCaptcha();
        }

        public IActionResult GetCaptcha()
        {
            return _captchaGenerator.GetCaptcha();
        }
    }
}

using DomainLayer.Interface.IRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;

namespace BusinessLayer.Repositories
{/// <summary>
/// captcha generator and sender to client
/// </summary>
    public class CaptchaGenerator : ICaptchaRepo
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CaptchaGenerator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// it get captcha that is generated 
        /// </summary>send it to the client after request for captcah </returns>
        public IActionResult GetCaptcha()
        {
            var (code, imageBytes) = GenerateCaptcha();

            // Store the CAPTCHA code in the session
            _httpContextAccessor.HttpContext.Session.SetString("CaptchaCode", code);

            return new FileStreamResult(new MemoryStream(imageBytes), "image/png");
        }
        /// <summary>
        /// generate captcha using graphic 
        /// </summary>
        /// <returns>Generated captch</returns>
        public (string Code, byte[] ImageBytes) GenerateCaptcha()
        {
            var random = new Random();
            var code = random.Next(1000, 9999).ToString();
            var bitmap = new Bitmap(100, 40);
            var graphics = Graphics.FromImage(bitmap);
            var font = new Font("Arial", 20, FontStyle.Bold);
            var brush = new SolidBrush(Color.Black);

            graphics.Clear(Color.White);
            graphics.DrawString(code, font, brush, 10, 10);

            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, ImageFormat.Png);
                return (code, memoryStream.ToArray());
            }
        }

        (string Code, byte[] ImageBytes) ICaptchaRepo.GenerateCaptcha()
        {
            throw new NotImplementedException();
        }
    }
}

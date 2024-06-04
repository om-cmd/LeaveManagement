using DomainLayer.Interface.IService;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CaptchaController : ControllerBase
{
    private readonly ICaptchaService _service;

    /// <summary>
    /// use to cretae captcha and check it
    /// </summary>
    /// <param name="service"></param>
    public CaptchaController(ICaptchaService  service)
    {
        _service = service;
    }
    /// <summary>
    /// captcha getting controller
    /// </summary>
    /// <returns>it returns the captcha </returns>

    [HttpGet("get-captcha")]
    public IActionResult GetCaptcha()
    {
        var captcha = _service.GetCaptcha();
        return Ok(captcha);

    }
}


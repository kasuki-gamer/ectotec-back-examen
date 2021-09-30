using ectotec.Models.ViewModels;
using Edi.Captcha;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ectotec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        public readonly ISessionBasedCaptcha _captcha;

        public CaptchaController(ISessionBasedCaptcha captcha)
        {
            _captcha = captcha;
        }

        [HttpGet]
        public IActionResult GetCaptchaImage()
        {
            var s = _captcha.GenerateCaptchaImageFileStream(HttpContext.Session, 150, 36);
            return s;
        }

        [HttpGet("valida/{id}")]
        public ActionResult<CaptchaResultCode> Valida(string id)
        {
            var captcha = HttpContext.Session.GetString("CaptchaCode");

            if (id == captcha)
            {
                return new CaptchaResultCode
                {
                    CaptchaCode = HttpContext.Session.GetString("CaptchaCode")
                };
            }
            else
            {
                return new CaptchaResultCode
                {
                    CaptchaCode = ""
                };
            }

        }
    }
}

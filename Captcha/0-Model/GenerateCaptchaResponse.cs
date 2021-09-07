using System;
namespace Captcha
{
    public class GenerateCaptchaResponse
    {
        public string Captcha { get; set; }
        public Guid CaptchaId { get; set; }
        public byte[] CaptchaContent { get; set; }
    }
}

using System;

namespace Captcha._0_Model
{
    public class GenerateCaptchaResponse
    {
        public string Captcha { get; set; }
        public Guid CaptchaId { get; set; }
        public byte[] CaptchaContent { get; set; }
    }
}

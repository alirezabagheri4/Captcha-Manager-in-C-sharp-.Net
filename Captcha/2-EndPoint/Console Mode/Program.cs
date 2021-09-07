namespace Captcha
{
    class Program
    {
        static void Main(string[] args)
        {
            var captchaManager = new CaptchaManager();

            var generateCaptchaResponse = captchaManager.GenerateCaptcha();

            var captcha= generateCaptchaResponse.Captcha;

            var captchaContent = generateCaptchaResponse.CaptchaContent;

            var captchaId = generateCaptchaResponse.CaptchaId;

            //
            string loginCaptcha=captcha;

            var verifyCaptchaRequest = new VerifyCaptchaRequest() {Captcha = captcha, LoginCaptcha = loginCaptcha};

            var verifyCaptchaResponse= captchaManager.VerifyCaptcha(verifyCaptchaRequest);
        }
    }
}

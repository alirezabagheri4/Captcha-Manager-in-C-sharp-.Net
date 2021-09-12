using System;
namespace Captcha
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Test Create Captcha

            var captchaManager = new CaptchaManager();

            var generateCaptchaResponse = captchaManager.GenerateCaptcha();

            var captcha = generateCaptchaResponse.Captcha;

            Console.WriteLine($"Captcha code is :{captcha}");

            var captchaContent = generateCaptchaResponse.CaptchaContent;

            var captchaId = generateCaptchaResponse.CaptchaId;

            #endregion

            #region Test Verify Captcha

            Console.WriteLine("please inter the security code(Captcha Code)");
            string inputCaptcha = Console.ReadLine();

            var verifyCaptchaRequest = new VerifyCaptchaRequest() { Captcha = captcha, LoginCaptcha = inputCaptcha };

            var verifyCaptchaResponse = captchaManager.VerifyCaptcha(verifyCaptchaRequest);

            switch (verifyCaptchaResponse.Result.ActionResult)
            {
                case eVerifyCaptchaResponse.Matched:
                    Console.WriteLine("Matched");
                    break;
                case eVerifyCaptchaResponse.Expired:
                    Console.WriteLine("Expired");
                    break;
                case eVerifyCaptchaResponse.NotMatched:
                    Console.WriteLine("NotMatched");
                    break;
                case eVerifyCaptchaResponse.TryAgain:
                    Console.WriteLine("TryAgain");
                    break;
                default:
                    Console.WriteLine("TryAgain");
                    break;
            }
            

            #endregion

            Console.ReadKey();
        }
    }
}

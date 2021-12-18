using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Threading.Tasks;
namespace Captcha
{
    public class CaptchaManager
    {
        private static readonly CaptchaManager instance = null;
        public static CaptchaManager Instance => instance ?? new CaptchaManager();

        public GenerateCaptchaResponse GenerateCaptcha()
        {
            var captcha = GetRandomNumber();
            var captchaImg = DrawString(captcha);
            var captchaUniqueId = Guid.NewGuid();
            var generateCaptchaResponse = new GenerateCaptchaResponse()
            {
                Captcha = captcha,
                CaptchaContent = captchaImg,
                CaptchaId = captchaUniqueId
            };
            return generateCaptchaResponse;
        }

        private static string GetRandomNumber()
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            var value = rand.Next(100000, 999999);
            return value.ToString();
        }

        private static byte[] DrawString(string captcha)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(130, 30))
            using (var gfx = Graphics.FromImage(bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                int i, r, x, y;
                var pen = new Pen(Color.Yellow);
                for (i = 1; i < 10; i++)
                {
                    pen.Color = Color.FromArgb((rand.Next(10, 255)), (rand.Next(10, 255)), (rand.Next(10, 255)));
                    r = rand.Next(0, (130 / 3));
                    x = rand.Next(0, 130);
                    y = rand.Next(0, 30);
                    gfx.DrawEllipse(pen, x - r, y - r, r, r);
                }

                //add question
                gfx.DrawString(captcha, new Font("Tahoma", 18), Brushes.DarkSlateGray, 5, 0);

                //render as Jpeg
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);

                return mem.ToArray();
            }
        }

        internal virtual VerifyCaptchaResponse VerifyCaptcha(VerifyCaptchaRequest verifyCaptcha)
        {
            var operationResult = new VerifyCaptchaResponse();
            try
            {
                if (verifyCaptcha.LoginCaptcha != null && verifyCaptcha.Captcha == verifyCaptcha.LoginCaptcha)
                {
                    operationResult.ActionResult = eVerifyCaptchaResponse.Matched;
                }
                else
                {
                    operationResult.ActionResult = eVerifyCaptchaResponse.NotMatched;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                operationResult.ActionResult = eVerifyCaptchaResponse.TryAgain;
            }
            return operationResult;
        }
    }
}

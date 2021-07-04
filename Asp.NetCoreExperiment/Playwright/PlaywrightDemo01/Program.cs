using Microsoft.Playwright;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightDemo01
{
    class Program
    {
        public static async Task Main()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync();
            var page = await browser.NewPageAsync();

            await page.GotoAsync("https://www.google.com/");
            // Fill an input.
            await page.FillAsync(".gLFyf", "桂素伟");

            // Navigate implicitly by clicking a link.
            await page.ClickAsync(".gNO89b");
            // Expect a new url.
            Console.WriteLine(page.Url);


            await page.GotoAsync(page.Url);
            // var response = await page.GotoAsync("https://playwright.dev/dotnet");
            //var body = await response.BodyAsync();
            //var content = Encoding.UTF8.GetString(body);
            //Console.WriteLine(content);
            var path = $"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.png";
            var imageBytes = await page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = path,
                FullPage = true
            });

            File.WriteAllBytes($"{Directory.GetCurrentDirectory()}/{ path}", imageBytes);
        }
    }
}

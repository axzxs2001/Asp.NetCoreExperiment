using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LineDemo02.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
            _logger.LogInformation("OnGet");
            var client = new HttpClient();
            var content = await client.GetStringAsync(@"https://access.line.me/dialog/oauth/weblogin?response_type=code&client_id=1656887987&redirect_uri=https%3A%2F%2Fnew-way.xin%2Fline&state=12345abcde&scope=profile%20openid&nonce=09876xyz");
           // _logger.LogInformation(content);
        }
    }
}
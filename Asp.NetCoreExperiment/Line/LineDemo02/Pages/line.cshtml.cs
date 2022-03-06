using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LineDemo02.Pages
{
    public class lineModel : PageModel
    {
        private readonly ILogger<lineModel> _logger;

        public lineModel(ILogger<lineModel> logger)
        {
            _logger = logger;
        }
        public User _user { get; set; } = null;
        public async Task OnGet(string code, string state)
        {
            _logger.LogInformation(code);
            if (!string.IsNullOrWhiteSpace(state))
            {
                var client = new HttpClient();
                var nvc = new KeyValuePair<string, string>[]
                {
               new  KeyValuePair<string,string>("grant_type", "authorization_code"),
                new  KeyValuePair<string,string> ("code", code),
                new  KeyValuePair<string,string> ("redirect_uri", "https://new-way.xin/line"),
                 new  KeyValuePair<string,string>("client_id","1656887987"),
                 new  KeyValuePair<string,string>("client_secret","3e34ba571dabd583b80bc606a6475f7f"),
                       new  KeyValuePair<string,string>("state",state),

                };
                var content = new FormUrlEncodedContent(nvc);
                content.Headers.Clear();
                content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                var url = @"https://api.line.me/v2/oauth/accessToken";//@"https://api.line.me/oauth2/v2.1/token"
                var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
                var res = await client.SendAsync(req);
                var backcontent = await res.Content.ReadAsStringAsync();
                _logger.LogInformation(backcontent);


                var abc = System.Text.Json.JsonSerializer.Deserialize<ABC>(backcontent);
                var req1 = new HttpRequestMessage(HttpMethod.Get, @"https://api.line.me/v2/profile");
                req1.Headers.Add("Authorization", $"Bearer {abc.access_token}");
                var res1 = await client.SendAsync(req1);
                var backcontent1 = await res1.Content.ReadAsStringAsync();
                _logger.LogInformation(backcontent1);

                _user = System.Text.Json.JsonSerializer.Deserialize<User>(backcontent1);
            }

        }
    }
    class ABC
    {
        public string access_token { get; set; }
    }

    public class User
    {
        public string userId { get; set; }
        public string displayName { get; set; }

        public string pictureUrl { get; set; }
    }
}

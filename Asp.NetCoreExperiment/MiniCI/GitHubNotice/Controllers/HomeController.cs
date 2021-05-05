using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GitHubNotice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IEnumerable<Project> _projects;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _projects = configuration.GetSection("Projects").Get<List<Project>>();
            _logger = logger;
        }

        [HttpPost("/")]
        public async Task<IActionResult> Notice()
        {
            _logger.LogInformation($"应用收到github Notice");     
            Request.Headers.TryGetValue("X-GitHub-Delivery", out StringValues gitHubDeliveryId);
            Request.Headers.TryGetValue("X-GitHub-Event", out StringValues gitHubEvent);
            _logger.LogInformation($"收到github通知事务：X-GitHub-Delivery：{gitHubDeliveryId}，X-GitHub-Event：{gitHubEvent}");
            Request.Headers.TryGetValue("X-Hub-Signature", out StringValues gitHubSignature);
            Request.Headers.TryGetValue("X-Hub-Signature-256", out StringValues gitHubSignature256);
            var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var bodyContent = await reader.ReadToEndAsync();
            var key = GetMark(bodyContent);
            var rgx = new Regex(@"^[a-zA-Z]+$");
            if (!string.IsNullOrWhiteSpace(key) && !rgx.IsMatch(key))
            {
                _logger.LogError($"key={key} 错误");
                return BadRequest();
            }
            else
            {
                var project = _projects.SingleOrDefault(s => s.Key == key);
                if (project != null)
                {
                    var resultSHA256 = IsGitHubSignatureSHA256(project.Secret, bodyContent, gitHubSignature256);
                    var resultSHA1 = IsGitHubSignatureSHA1(project.Secret, bodyContent, gitHubSignature);
                    _logger.LogInformation($"SHA1={resultSHA1},SHA256={resultSHA256}");
                    if (resultSHA1 && resultSHA256)
                    {
                        var p = new Process();
                        p.StartInfo.CreateNoWindow = true;
                        p.StartInfo.UseShellExecute = true;
                        p.StartInfo.FileName = project.BATPath;
                        p.Start();
                        p.Close();
                        _logger.LogInformation($"github通知成功");
                        return Ok();
                    }
                    else
                    {
                        _logger.LogError("认证错误");
                        return Unauthorized();
                    }
                }
                else
                {
                    _logger.LogError($"检查配置文件Projects是否与github中的Payload URL相匹配");
                    return BadRequest();
                }
            }
        }
        /// <summary>
        /// 获取匹配项目的key
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string GetMark(string text)
        {
            var arry = text.Split('&');
            foreach (var item in arry)
            {
                if (item.Contains("key="))
                {
                    return item.Split('=')[1];
                }
            }
            return "";
        }
        /// <summary>
        /// sha1
        /// </summary>
        /// <param name="seckey"></param>
        /// <param name="bodyContent"></param>
        /// <param name="signatureSHA1"></param>
        /// <returns></returns>
        static bool IsGitHubSignatureSHA1(string seckey, string bodyContent, string signatureSHA1)
        {
            if (string.IsNullOrWhiteSpace(bodyContent))
                throw new ArgumentNullException(nameof(bodyContent));
            if (string.IsNullOrWhiteSpace(signatureSHA1))
                throw new ArgumentNullException(nameof(signatureSHA1));

            var signature = signatureSHA1.Replace("sha1=", "");
            var secret = Encoding.ASCII.GetBytes(seckey);
            var payloadBytes = Encoding.ASCII.GetBytes(bodyContent);

            using (var hmacsha1 = new HMACSHA1(secret))
            {
                var hash = hmacsha1.ComputeHash(payloadBytes);
                var hashString = ToHexString(hash);
                if (hashString.Equals(signature))
                    return true;
            }
            return false;

            static string ToHexString(byte[] bytes)
            {
                var builder = new StringBuilder(bytes.Length * 2);
                foreach (byte b in bytes)
                {
                    builder.AppendFormat("{0:x2}", b);
                }
                return builder.ToString();
            }
        }


        /// <summary>
        /// X-Hub-Signature-256
        /// </summary>
        /// <param name="secret"></param>
        /// <param name="bodyContent"></param>
        /// <param name="signatureSHA256"></param>
        /// <returns></returns>
        static bool IsGitHubSignatureSHA256(string secret, string bodyContent, string signatureSHA256)
        {
            if (string.IsNullOrWhiteSpace(bodyContent))
                throw new ArgumentNullException(nameof(bodyContent));
            if (string.IsNullOrWhiteSpace(signatureSHA256))
                throw new ArgumentNullException(nameof(signatureSHA256));

            var secretBytes = Encoding.UTF8.GetBytes(secret);
            var hasher = new HMACSHA256(secretBytes);
            var data = Encoding.UTF8.GetBytes(bodyContent);
            var computedSignature = BitConverter.ToString(hasher.ComputeHash(data)).Replace("-", "").ToLower();
            return computedSignature == signatureSHA256.Replace("sha256=", "");
        }
    }
    /// <summary>
    /// 项目实体
    /// </summary>
    public class Project
    {
        public string Key { get; set; }
        public string BATPath { get; set; }
        public string Secret { get; set; }
    }
}

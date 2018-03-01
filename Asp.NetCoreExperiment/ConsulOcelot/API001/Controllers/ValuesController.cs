using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace API001.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        [HttpGet]
        public string Get()
        {
            return $"API001:{DateTime.Now.ToString()}  { Environment.MachineName + " OS:" + Environment.OSVersion.VersionString}";
        }

        [HttpGet("/health")]
        public IActionResult Heathle()
        {
            return Ok();
        }


        [HttpPost("/notice")]
        public IActionResult Notice()
        {
            var bytes = new byte[10240];
            var i = Request.Body.ReadAsync(bytes, 0, bytes.Length);
            var content = System.Text.Encoding.UTF8.GetString(bytes).Trim('\0');
            SendEmail(content);
            return Ok();
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="content"></param>
        void SendEmail(string content)
        {
            try
            {
                dynamic list = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                if (list != null && list.Count > 0)
                {
                    var emailBody = new StringBuilder("健康检查故障:\r\n");
                    foreach (var noticy in list)
                    {
                        emailBody.AppendLine($"--------------------------------------");
                        emailBody.AppendLine($"Node:{noticy.Node}");
                        emailBody.AppendLine($"Service ID:{noticy.ServiceID}");
                        emailBody.AppendLine($"Service Name:{noticy.ServiceName}");
                        emailBody.AppendLine($"Check ID:{noticy.CheckID}");
                        emailBody.AppendLine($"Check Name:{noticy.Name}");
                        emailBody.AppendLine($"Check Status:{noticy.Status}");
                        emailBody.AppendLine($"Check Output:{noticy.Output}");
                        emailBody.AppendLine($"--------------------------------------");
                    }
                    var message = new MimeMessage();
                    //这里只是是测试邮箱，请不要发垃圾邮件，谢谢
                    message.From.Add(new MailboxAddress("gswmicroservice", "gswmicroservice@163.com"));
                    message.To.Add(new MailboxAddress("285130205", "285130205@qq.com"));

                    message.Subject = "作业报警";
                    message.Body = new TextPart("plain") { Text = emailBody.ToString() };
                    using (var client = new SmtpClient())
                    {

                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                        client.Connect("smtp.163.com", 25, false);
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        client.Authenticate("你的用户名", "你的密码");
                        client.Send(message);
                        client.Disconnect(true);
                    }
                }
            }
            catch
            {

            }

        }
    }
}

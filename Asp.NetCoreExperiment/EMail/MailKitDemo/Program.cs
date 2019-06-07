using ICSharpCode.SharpZipLib.Zip;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.IO;
using System.Text;

namespace MailKitDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        string ZipFile(string parentPath, string filePath, string password)
        {
            //这行决定压缩文件名不乱码
            ZipStrings.CodePage = Encoding.UTF8.CodePage;
            var zipfile = $"{Path.GetDirectoryName(filePath)}\\{Guid.NewGuid().ToString()}.zip";
            var zipFile = ICSharpCode.SharpZipLib.Zip.ZipFile.Create(zipfile);
            zipFile.BeginUpdate();
            zipFile.Password = password;
            ZipAddFile(zipFile, parentPath, filePath);
            zipFile.CommitUpdate();
            zipFile.Close();
            return zipfile;


        }
        void ZipAddFile(ZipFile zipFile, string parentPath, string filePath)
        {
            //添加文件
            foreach (var file in Directory.GetFiles(filePath))
            {
                zipFile.Add(file, Path.Combine(parentPath, Path.GetFileName(file)));
            }
            //添加文件夹
            foreach (var dir in Directory.GetDirectories(filePath))
            {
                ZipAddFile(zipFile, Path.Combine(parentPath, dir), dir);
            }
        }

        void SendEmail(dynamic emailSetting, string bodystring, string attachmentDirectory)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailSetting.username, emailSetting.fromaddress));
            //接收邮件地址
            var toaddresses = emailSetting.toaddresses?.Split(',');
            foreach (var toaddresse in toaddresses)
            {
                message.To.Add(new MailboxAddress(toaddresse.Split('@')?[0], toaddresse));
            }
            //抄送
            if (!string.IsNullOrEmpty(emailSetting.ccaddresses))
            {
                var ccaddresses = emailSetting.ccaddresses.Split(',');
                foreach (var ccaddresse in ccaddresses)
                {
                    message.Cc.Add(new MailboxAddress(ccaddresse.Split('@')?[0], ccaddresse));
                }
            }
            //标题
            message.Subject = emailSetting.subject;
            var builder = new BodyBuilder()
            {
                TextBody = string.Format(emailSetting.body, bodystring)
            };
            //todo 10个企业发送一次没有解决
            var zipfile = "";
            FileStream stream = null;
            if (emailSetting.hasattachment)
            {
                if (emailSetting.iscompress)
                {
                    zipfile = ZipFile(string.Format(emailSetting.directoryprefix, DateTime.Now.ToString("yyMMdd")), attachmentDirectory, emailSetting.compresspassword);
                    stream = new FileStream(zipfile, FileMode.Open);
                    var attachment = new MimePart()
                    {
                        Content = new MimeContent(stream),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.UUEncode
                    };
                    var zipName = string.Format(emailSetting.zipprefix, DateTime.Now.ToString("yyMMdd"));
                    //这两行决定附件名称不乱码
                    attachment.ContentType.Parameters.Add(new Parameter("utf-8", "name", zipName) { EncodingMethod = ParameterEncodingMethod.Rfc2047 });
                    attachment.ContentDisposition.Parameters.Add(new Parameter("utf-8", "filename", zipName) { EncodingMethod = ParameterEncodingMethod.Rfc2047 });
                    builder.Attachments.Add(attachment);
                }
            }
            message.Body = builder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect(emailSetting.host, emailSetting.port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(emailSetting.fromaddress, emailSetting.password);
                client.Send(message);
                client.Disconnect(true);
                stream?.Close();
                if (File.Exists(zipfile))
                {
                    File.Delete(zipfile);
                }
            }
        }
    }
}

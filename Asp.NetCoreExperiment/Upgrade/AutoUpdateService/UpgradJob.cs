using Quartz;
using System.Text.RegularExpressions;

namespace AutoUpdateService
{
    public class UpgradJob : IJob
    {
        private readonly HttpClient _client;
        private readonly string? _selfUrl;
        private readonly string? _zipFile;
        public UpgradJob(IHttpClientFactory clientFactory, UpgradeSettingModel upgradeSettingModel)
        {
            _client = clientFactory.CreateClient("upgrade");
            _selfUrl = upgradeSettingModel?.SelfUrl;
            _zipFile = upgradeSettingModel?.ZipFile;
        }
        string GetVersion(string content)
        {
            var pattern = @"<version>(.*?)</version>";
            var matches = Regex.Matches(content, pattern);
            foreach (Match match in matches)
            {
                if (match.Groups.Count > 0)
                {
                    return match.Groups[0].Value;

                }
            }
            return "";
        }
        string GetUpgradeServerUrl(string content)
        {
            var pattern = @"<url>(.*?)</url>";
            var matches = Regex.Matches(content, pattern);
            foreach (Match match in matches)
            {
                if (match.Groups.Count > 0)
                {
                    return match.Groups[0].Value;

                }
            }
            return "";
        }
        public async Task Execute(IJobExecutionContext context)
        {

            var response = await _client.GetAsync("AutoUpdaterStarter.xml");
            var xml = await response.Content.ReadAsStringAsync();
            var currentPath = Directory.GetCurrentDirectory();
            var oldVersion = "";
            if (File.Exists(Path.Combine(currentPath, "wwwroot", "AutoUpdaterStarter.xml")))
            {
                oldVersion = GetVersion(File.ReadAllText(Path.Combine(currentPath, "wwwroot", "AutoUpdaterStarter.xml")));
            }
            var newVersion = "";
            if (!string.IsNullOrWhiteSpace(xml))
            {
                newVersion = GetVersion(xml);
            }
            if (oldVersion != newVersion)
            {
                var upgradeServerUrl = GetUpgradeServerUrl(xml);
                if (!string.IsNullOrWhiteSpace(upgradeServerUrl))
                {

                    upgradeServerUrl = upgradeServerUrl.Replace("<url>", "").Replace("</url>", "");
                    xml = xml.Replace(upgradeServerUrl, Path.Combine(_selfUrl, _zipFile));
                    await File.WriteAllTextAsync(Path.Combine(currentPath, "wwwroot", "AutoUpdaterStarter.xml"), xml);

                    var responseStream = await _client.GetAsync(upgradeServerUrl);
                    using (Stream contentStream = await responseStream.Content.ReadAsStreamAsync())
                    {
                        // 将数据流写入本地文件
                        using (var fileStream = new FileStream(Path.Combine(currentPath, "wwwroot", _zipFile), FileMode.Create, FileAccess.Write))
                        {
                            await contentStream.CopyToAsync(fileStream);
                        }
                    }
                }
            }
        }
    }
}

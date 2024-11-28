// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Text;
using HtmlAgilityPack;
using System.Reflection.PortableExecutable;




Console.WriteLine(await GetUrlContentAsync("#https://doda.jp/DodaFront/View/JobSearchDetail/j_jid__3011267946/-tp__1/-tab__jd/-fm__jobdetail/"));



async Task<string> GetUrlContentAsync(string content)
{
    using (var client = new HttpClient())
    {
        var pattern = @"#(https?://)([\da-z\.-]+)\.([a-z\.]{2,6})([/\w \.-]*)*/?";
        var reg = new RegularExpressionAttribute(pattern);
        var regex = new Regex(pattern);

        var matches = regex.Matches(content);
        var contentBuilder = new StringBuilder();
        if (matches.Count > 0)
        {
            foreach (Match match in matches)
            {
                var urlContent = await GetContentByUrlAsync(match.Value.TrimStart('#', ' '));
                contentBuilder.Append(urlContent);
            }
            return contentBuilder.ToString();

        }
        else
        {
            return content;
        }
    }

    async Task<string> GetContentByUrlAsync(string url, bool isHtml = false)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        using (var client = new HttpClient())
        {
            //添加header
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36");
            client.DefaultRequestHeaders.Add("Accept", "*/*");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("Host", "doda.jp");


            // 发送 GET 请求
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // 读取响应内容为字符串
            var responseBody = await response.Content.ReadAsStringAsync();

            // 创建 HtmlDocument 对象
            var doc = new HtmlDocument();
            doc.LoadHtml(responseBody);

            // 移除不需要的标签内容，如 JavaScript 或 CSS
            RemoveTag(doc, "script");
            RemoveTag(doc, "style");
            RemoveTag(doc, "noscript");

            // 使用 XPath 查询找到所有的文本节点，忽略样式和脚本标签

            var nodes = doc.DocumentNode.SelectNodes(isHtml ? "//body" : "//body//text()[normalize-space()]");
            var content = new StringBuilder();
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    if (isHtml)
                    {
                        content.AppendLine(node.InnerHtml.Trim().Replace(" ", "").Replace("/r", "").Replace("/n", ""));
                    }
                    else
                    {
                        content.AppendLine(node.InnerText.Trim());
                    }
                }
            }
            return content.ToString();
        }
    }

    void RemoveTag(HtmlDocument document, string tagName)
    {
        var nodes = document.DocumentNode.SelectNodes($"//{tagName}");
        if (nodes != null)
        {
            foreach (var node in nodes)
            {
                node.Remove();
            }
        }
    }
}

using HtmlAgilityPack;
using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.V8;
using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;

namespace HtmlAgilityPackDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var arrStrings = File.ReadAllLines(@"C:\MyFile\aaa.txt");
            ChromeMethod(arrStrings);

            IJsEngine engine = new V8JsEngine(
    new V8Settings
    {
        MaxNewSpaceSize = 4,
        MaxOldSpaceSize = 8
    }
);

            V8Method(engine);


            HTMLMethod(arrStrings);

        }
        static void  ChromeMethod(string[] arrStrings)
        {
            ChromeOptions op = new ChromeOptions();
            op.AddArguments("--headless");//开启无gui模式
           // op.AddArguments("--no-sandbox");//停用沙箱以在Linux中正常运行
            ChromeDriver cd = new ChromeDriver(op);
            cd.Navigate().GoToUrl("https://www.baidu.com");
            string text = cd.FindElementById("su").GetAttribute("value");

           
            cd.Quit();
            Console.WriteLine(text);
            Console.Read();
        }

        private static void V8Method(IJsEngine engine)
        {
            engine.Execute(" function A(){ console.log('a');return 'abcd'}");

            engine.ExecuteFile(@"c:/myfile/test/lc.line.web.login_1548918449.js");
            var obj = engine.CallFunction("A");
            Console.WriteLine(obj);
        }

        private static void HTMLMethod(string[] arrStrings)
        {
            using (var httpClientHandler = new HttpClientHandler
            {
                MaxResponseHeadersLength = 1024,
                //AutomaticDecompression = DecompressionMethods.GZip,
                //CheckCertificateRevocationList = true,
                AllowAutoRedirect = false,
                //UseCookies = true,
                //// UseDefaultCredentials = true,
                //CookieContainer = new CookieContainer()
            })
            {
                using (var client = new HttpClient(httpClientHandler))
                {
                    client.BaseAddress = new Uri(arrStrings[4]);
                    var values = new List<KeyValuePair<string, string>>();
                    var getRequest = new HttpRequestMessage(HttpMethod.Get, arrStrings[0]);
                    var getResponse = client.SendAsync(getRequest).Result;
                    if (getResponse.IsSuccessStatusCode)
                    {
                        var content = getResponse.Content.ReadAsStringAsync().Result;
                        #region 准备参数
                        var doc = new HtmlDocument();
                        doc.LoadHtml(content);
                        foreach (var input in doc.DocumentNode.SelectNodes("//form/input"))
                        {
                            Console.WriteLine(input.GetAttributeValue("name", "") + "=" + input.GetAttributeValue("value", ""));

                            if (input.GetAttributeValue("name", "").Trim() == "userId")
                            {
                                values.Add(new KeyValuePair<string, string>(input.GetAttributeValue("name", ""), arrStrings[1]));

                            }
                            else
                              if (input.GetAttributeValue("name", "").Trim() == "password")
                            {
                                values.Add(new KeyValuePair<string, string>(input.GetAttributeValue("name", ""), arrStrings[5]));

                            }
                            else
                            {
                                values.Add(new KeyValuePair<string, string>(input.GetAttributeValue("name", ""), input.GetAttributeValue("value", "")));
                            }



                        }
                        values.Add(new KeyValuePair<string, string>("tid", arrStrings[1]));
                        values.Add(new KeyValuePair<string, string>("tpasswd", arrStrings[2]));




                        #endregion



                        var postRequest = new HttpRequestMessage(HttpMethod.Post, arrStrings[3]);

                        postRequest.Content = new FormUrlEncodedContent(values);
                        var postResponse = client.SendAsync(postRequest).Result;
                        if (postResponse.IsSuccessStatusCode)
                        {
                            content = postResponse.Content.ReadAsStringAsync().Result;
                            Console.WriteLine("返回值：" + content);
                        }
                        else
                        {
                            content = postResponse.Content.ReadAsStringAsync().Result;
                            Console.WriteLine("返回值：" + content);
                        }
                    }
                    else
                    {
                        var content = getResponse.Content.ReadAsStringAsync().Result;
                        Console.WriteLine("返回值：" + content);
                    }



                }
            }
        }
    }
}

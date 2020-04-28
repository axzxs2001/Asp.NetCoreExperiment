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
using Tesseract;

namespace HtmlAgilityPackDemo
{
    class Program
    {
        static void Main(string[] args)
        {


            TesseractTest(args);

           // var arrStrings = File.ReadAllLines(@"C:\MyFile\aaa.txt");
           // ChromeMethod(arrStrings);

            //V8Method();
            //HTMLMethod(arrStrings);

        }
        /// <summary>
        /// ocr
        /// </summary>
        /// <param name="args"></param>
        static void TesseractTest(string[] args)
        {
            var testImagePath = "./a.jpg";// "./b.tif";// "c:/myfile/a.jpg";
            if (args.Length > 0)
            {
                testImagePath = args[0];
            }

            try
            {
                using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(testImagePath))
                    {
                        using (var page = engine.Process(img))
                        {
                            var text = page.GetText();
                            Console.WriteLine("Mean confidence: {0}", page.GetMeanConfidence());

                            Console.WriteLine("Text (GetText): \r\n{0}", text);
                            Console.WriteLine("Text (iterator):");
                            using (var iter = page.GetIterator())
                            {
                                iter.Begin();

                                do
                                {
                                    do
                                    {
                                        do
                                        {
                                            do
                                            {
                                                if (iter.IsAtBeginningOf(PageIteratorLevel.Block))
                                                {
                                                    Console.WriteLine("<BLOCK>");
                                                }

                                                Console.Write(iter.GetText(PageIteratorLevel.Word));
                                                Console.Write(" ");

                                                if (iter.IsAtFinalOf(PageIteratorLevel.TextLine, PageIteratorLevel.Word))
                                                {
                                                    Console.WriteLine();
                                                }
                                            } while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));

                                            if (iter.IsAtFinalOf(PageIteratorLevel.Para, PageIteratorLevel.TextLine))
                                            {
                                                Console.WriteLine();
                                            }
                                        } while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                                    } while (iter.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                                } while (iter.Next(PageIteratorLevel.Block));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {              
                Console.WriteLine("Unexpected Error: " + e.Message);
                Console.WriteLine("Details: ");
                Console.WriteLine(e.ToString());
            }
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
        /// <summary>
        /// 爬虫
        /// </summary>
        /// <param name="arrStrings"></param>
        static void ChromeMethod(string[] arrStrings)
        {
            ChromeOptions op = new ChromeOptions();
            // op.AddArguments("--headless");//开启无gui模式
            // op.AddArguments("--no-sandbox");//停用沙箱以在Linux中正常运行
            ChromeDriver cd = new ChromeDriver(op);
            cd.Navigate().GoToUrl(arrStrings[4] + arrStrings[0]);
            var userNameEle = cd.FindElementById("id");
            var passwordEle = cd.FindElementById("passwd");
            var loginEle = cd.FindElementByClassName("MdBtn03Login");

            userNameEle.SendKeys(arrStrings[1]);
            passwordEle.SendKeys(arrStrings[2]);

            Console.WriteLine("回车登录");
            Console.ReadLine();
            loginEle.Click();

            Console.WriteLine("选择enterprise菜单");
            Console.ReadLine();
            var enterpriseEle = cd.FindElementByClassName("_center_merchant_list");
            enterpriseEle.Click();
            Console.ReadLine();
            cd.Quit();
            Console.ReadLine();
        }

        private static void V8Method()
        {
            IJsEngine engine = new V8JsEngine(
new V8Settings
{
    MaxNewSpaceSize = 4,
    MaxOldSpaceSize = 8
}
);
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

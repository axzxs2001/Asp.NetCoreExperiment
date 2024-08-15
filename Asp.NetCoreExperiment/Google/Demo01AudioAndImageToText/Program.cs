using Google.Apis.Services;
using Google.Cloud.Speech.V1;
using Google.Protobuf;
using Google.Apis.Discovery;
using Google.Apis.Discovery.v1;
using System.Text;


namespace Demo01AudioAndImageToText
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //GetToken().Wait();
            // Run().Wait();
            //AudioToText().Wait();
            //Speech().Wait();
            // CreateRec().Wait();
             FileSpeech2().Wait();
            // FileSpeech1().Wait();
            // Vision1().Wait();
            //CreateRecognizer().Wait();
           // ImageToText().Wait();
        }

        static async Task Vision1()
        {
            var apiKey = File.ReadAllText("C:\\GPT\\googlecloudkey.txt");
            string imagePath = "gsw.jpg";
            string imageContent = Convert.ToBase64String(System.IO.File.ReadAllBytes(imagePath));

            // 创建HTTP客户端
            var client = new HttpClient();
            string url = $"https://vision.googleapis.com/v1/images:annotate?key={apiKey}";

            // 构建请求内容
            var requestBody = new
            {
                requests = new[]
                {
                new
                {
                    image = new
                    {
                        content = imageContent
                    },
                    features = new[]
                    {
                        new
                        {
                            type = "TEXT_DETECTION"
                        }
                    }
                }
            }
            };

            string jsonRequestBody = System.Text.Json.JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

            // 发送POST请求
            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            // 解析响应内容
            string jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonResponse);
            //JObject responseObject = JObject.Parse(jsonResponse);
            //// 输出识别到的文本
            //var fullTextAnnotation = responseObject["responses"][0]["fullTextAnnotation"];
            //Console.WriteLine($"Text: {fullTextAnnotation["text"]}");

        }
        static async Task ImageToText()
        {
            var json = File.ReadAllText("C:\\GPT\\test.txt");
            var client = new Google.Cloud.Vision.V1.ImageAnnotatorClientBuilder
            {
                //JsonCredentials = json
            }.Build();
            var image = Google.Cloud.Vision.V1.Image.FromFile("gsw.jpg");
            var text = await client.DetectDocumentTextAsync(image);
            Console.WriteLine(text.Text);
        }

        static async Task FileSpeech2()
        {
            var apiKey = File.ReadAllText("C:\\GPT\\googlecloudkey.txt");

            var url = $"https://speech.googleapis.com/v2/projects/sre-common-test-379805/locations/global/recognizers/speechrecognizer:recognize?key={apiKey}";

            // 将本地文件转换为 Base64 编码
            var fileContent = File.ReadAllBytes("audio.wav");
            var audioBase64 = Convert.ToBase64String(fileContent);

            var json = $@"
            {{
            'config':{{
                'model':'long',
                'languageCodes':['cmn-Hans-CN', 'ja-JP', 'en-US'],
                'autoDecodingConfig':null   
            }},
            'content': '{audioBase64}'           
        }}";

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                Console.WriteLine(response.StatusCode);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
        }
        static async Task FileSpeech1()
        {
            var apiKey = File.ReadAllText("C:\\GPT\\googlecloudkey.txt");
            var url = $"https://speech.googleapis.com/v1/speech:recognize?key={apiKey}";

            // 将本地文件转换为 Base64 编码
            var fileContent = File.ReadAllBytes("audio1.wav");
            var audioBase64 = Convert.ToBase64String(fileContent);

            //encoding    https://cloud.google.com/speech-to-text/docs/reference/rest/v1/RecognitionConfig#AudioEncoding
            var json = $@"{{
            'config': {{
                'encoding':'WEBM_OPUS',
                'sampleRateHertz': 48000,
                'languageCode': 'zh-CN'
            }},
            'audio': {{
                'content': '{audioBase64}'
            }}
        }}";

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
        }

        static async Task AudioToText(CancellationToken cancellationToken = default)
        {
            var json = File.ReadAllText("C:\\GPT\\test.txt");
            var speech = new Google.Cloud.Speech.V2.SpeechClientBuilder
            {
                // JsonCredentials = json

            }.Build();
            var response = speech.RecognizeAsync(new Google.Cloud.Speech.V2.RecognizeRequest
            {
                Config = new Google.Cloud.Speech.V2.RecognitionConfig
                {
                    LanguageCodes = { "cmn-Hans-CN", "ja-JP", "en-US" },
                    Model = "long",
                    AutoDecodingConfig = new Google.Cloud.Speech.V2.AutoDetectDecodingConfig(),
                },
                Content = ByteString.CopyFrom(File.ReadAllBytes("audio.wav")),
                Recognizer = "projects/sre-common-test-379805/locations/global/recognizers/speechrecognizer"
            }, cancellationToken);
            response.Result.Results.ToList().ForEach(x =>
            {
                Console.WriteLine(x.ChannelTag);
                x.Alternatives.ToList().ForEach(y =>
                {
                    Console.WriteLine(y.Transcript);
                });
            });
        }

    }
}

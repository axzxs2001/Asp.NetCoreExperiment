using Google.Apis.Services;
using Google.Cloud.Speech.V1;
using Google.Protobuf;
using Google.Apis.Discovery;
using Google.Apis.Discovery.v1;
using Google.Apis.Discovery.v1.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util.Store;
using Grpc.Auth;
using Grpc.Core;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Google.Apis.Drive.v3;
using System.Text;
using Google.Cloud.Vision.V1;
using Newtonsoft.Json.Linq;

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
            //FileSpeech2().Wait();
            //FileSpeech1().Wait();
            //Vision1().Wait();
            CreateRecognizer().Wait();
        }

        static async Task Vision1()
        {
            var apiKey = File.ReadAllText("C:\\GPT\\googlecloudkey.txt");
            string imagePath = "gsw.jpg";
            string imageContent = Convert.ToBase64String(System.IO.File.ReadAllBytes(imagePath));

            // 创建HTTP客户端
            HttpClient client = new HttpClient();
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

            string jsonRequestBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

            // 发送POST请求
            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            // 解析响应内容
            string jsonResponse = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(jsonResponse);
            JObject responseObject = JObject.Parse(jsonResponse);
            // 输出识别到的文本
            var fullTextAnnotation = responseObject["responses"][0]["fullTextAnnotation"];
            Console.WriteLine($"Text: {fullTextAnnotation["text"]}");

        }


        static async Task CreateRecognizer()
        {
            var apiKey = File.ReadAllText("C:\\GPT\\googlecloudkey.txt");
            var url = $"https://speech.googleapis.com/v2/projects/sre-common-test-379805/locations/global/recognizers?recognizerId=az19999991&key={apiKey}";
            var json = $@"{{
  ""name"": ""speechrecognizer"",
  ""model"": ""long"",
  ""languageCodes"": ['cmn-Hans-CN', 'ja-JP', 'en-US'],
  ""defaultRecognitionConfig"":  {{
                'model':'long',
                'languageCodes':['cmn-Hans-CN', 'ja-JP', 'en-US'],
                'explicitDecodingConfig': {{
                    'encoding':'LINEAR16',
                    'sampleRateHertz':48000,
                    'audioChannelCount':1
                }}
            }}
}}";
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                Console.WriteLine(response.StatusCode);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }

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
                'model':'default',
                'languageCodes':['cmn-Hans-CN', 'ja-JP', 'en-US'],
                'explicitDecodingConfig': {{
                    'encoding':'LINEAR16',
                    'sampleRateHertz':48000,
                    'audioChannelCount':1
                }}
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
        static async Task GetToken()
        {
            var credential = await GoogleCredential.GetApplicationDefaultAsync();
            if (credential.IsCreateScopedRequired)
            {
                credential = credential.CreateScoped(new[] { "https://www.googleapis.com/auth/cloud-platform" });
            }

            ITokenAccess tokenAccess = credential;
            var token = await tokenAccess.GetAccessTokenForRequestAsync();
            Console.WriteLine(token);
        }
        static async Task Run()
        {
            var apiKey = File.ReadAllText("C:\\GPT\\googlecloudkey.txt");

            var service = new DiscoveryService(new BaseClientService.Initializer
            {
                ApplicationName = "qa-test",
                ApiKey = apiKey,
            });


            Console.WriteLine("Executing a list request...");
            var result = await service.Apis.List().ExecuteAsync();


            if (result.Items != null)
            {
                foreach (DirectoryList.ItemsData api in result.Items)
                {

                    Console.WriteLine(api.Id + " - " + api.Title + " -----  " + api.DiscoveryRestUrl);
                }
            }
        }
        static async Task AudioToText(CancellationToken cancellationToken = default)
        {


            //var clientSecrets = await GoogleClientSecrets.FromFileAsync("C:\\GPT\\speech_auth.json");

            //var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
            //     clientSecrets.Secrets,
            //     new[] { DriveService.ScopeConstants.DriveFile },
            //     "user",
            //     CancellationToken.None);

            //var driveService = new DriveService(new BaseClientService.Initializer()
            //{
            //    HttpClientInitializer = credential
            //});




            var apiKey = File.ReadAllText("C:\\GPT\\googlecloudkey.txt");
            var speech = new Google.Cloud.Speech.V2.SpeechClientBuilder
            {

                // Credential = credential
                //JsonCredentials = $"{{\"type\":\"api_key\",\"api_key\":\"{apiKey}\"}}"

            }.Build();
            var response = speech.RecognizeAsync(new Google.Cloud.Speech.V2.RecognizeRequest
            {
                Config = new Google.Cloud.Speech.V2.RecognitionConfig
                {
                    LanguageCodes = { "cmn-Hans-CN", "ja-JP", "en-US" },
                    Model = "short",
                    AutoDecodingConfig = new Google.Cloud.Speech.V2.AutoDetectDecodingConfig(),
                },
                Content = ByteString.CopyFrom(File.ReadAllBytes("audio.wav")),
                Recognizer = "qa-test"
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

        static async Task Speech()
        {
            string jsonPath = "C:\\GPT\\speech_auth.json";
            string audioFilePath = "audio.wav";
            var apiKey = File.ReadAllText("C:\\GPT\\googlecloudkey.txt");
            // 从 JSON 文件中加载服务账号凭证
            // var credential = GoogleCredential.FromFile(jsonPath);
            var json = $$"""{"type":"api_key","api_key":"{{apiKey}}"}""";
            // 使用凭证创建 SpeechClient
            var speechClient = new SpeechClientBuilder
            {
                JsonCredentials = json
            }.Build();

            var response = await RecognizeAsync(speechClient, audioFilePath);
            foreach (var result in response.Results)
            {
                foreach (var alternative in result.Alternatives)
                {
                    Console.WriteLine($"Transcript: {alternative.Transcript}");
                }
            }
        }

        static async Task<RecognizeResponse> RecognizeAsync(SpeechClient speechClient, string filePath)
        {
            var config = new RecognitionConfig
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                SampleRateHertz = 16000,
                LanguageCode = "en-US",
            };

            var audio = RecognitionAudio.FromFile(filePath);
            return await speechClient.RecognizeAsync(config, audio);
        }



    }
}

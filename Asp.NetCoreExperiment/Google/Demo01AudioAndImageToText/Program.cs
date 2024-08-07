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

namespace Demo01AudioAndImageToText
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //GetToken().Wait();
              //Run().Wait();
          AudioToText().Wait();
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

            // Create the service.
            var service = new DiscoveryService(new BaseClientService.Initializer
            {
                ApplicationName = "Discovery Sample",
                ApiKey = "AIzaSyCdFdb23XPcSexB2HSrOPe9EQmzDR3GFzU",
            });

            // Run the request.
            Console.WriteLine("Executing a list request...");
            var result = await service.Apis.List().ExecuteAsync();

            // Display the results.
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
            string jsonPath = "c:/gpt/google_speech_key.json";

            // 从 JSON 文件中加载服务账号凭证
            var credential = GoogleCredential.FromFile(jsonPath);
            var speech = new Google.Cloud.Speech.V2.SpeechClientBuilder
            {
                Credential = credential
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
                Recognizer = "Discovery Sample"
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
            string jsonPath = "path/to/your-service-account-file.json";
            string audioFilePath = "path/to/your/audiofile.wav";

            // 从 JSON 文件中加载服务账号凭证
            var credential = GoogleCredential.FromFile(jsonPath);

            // 使用凭证创建 SpeechClient
            var speechClient = new SpeechClientBuilder
            {
                Credential = credential
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

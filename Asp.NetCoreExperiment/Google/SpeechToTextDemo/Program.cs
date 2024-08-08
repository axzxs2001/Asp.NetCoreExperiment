using Google.Cloud.Speech.V2;


SpeechClient client = SpeechClient.Create();
var response = client.Recognize(new RecognizeRequest
{
    Config = new RecognitionConfig {
        
     },
     Content = Google.Protobuf.ByteString.FromStream(File.OpenRead("brooklyn_bridge.raw")),
});
response.Results.ToList().ForEach(result =>
{
    
    Console.WriteLine(result.Alternatives[0].Transcript);
});


var builder = WebApplication.CreateBuilder(args);



var app = builder.Build();



app.MapGet("/test", () =>
{

});

app.Run();


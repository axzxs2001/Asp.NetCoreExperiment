using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

var key = System.IO.File.ReadAllText(@"C:\NetStars\speechkey.txt");
var speechConfig = SpeechConfig.FromSubscription(key, "japaneast");
speechConfig.SpeechRecognitionLanguage = "zh-CN";
var text = await FromMic(speechConfig);

async static Task<string> FromMic(SpeechConfig speechConfig)
{
    using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
    using var recognizer = new SpeechRecognizer(speechConfig, audioConfig);
    Console.WriteLine("Speak into your microphone.");
    var result = await recognizer.RecognizeOnceAsync();
    Console.WriteLine($"RECOGNIZED: Text={result.Text}");

    return result.Text;
}




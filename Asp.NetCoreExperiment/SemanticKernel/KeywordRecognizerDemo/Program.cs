using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech;

var keywordModel = KeywordRecognitionModel.FromFile("znzl.table");
using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
using var keywordRecognizer = new KeywordRecognizer(audioConfig);
while (true)
{
    Console.WriteLine("开始：");
    KeywordRecognitionResult result = await keywordRecognizer.RecognizeOnceAsync(keywordModel);
    Console.WriteLine(result.Text);
}
using Microsoft.SemanticKernel.AudioToText;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Contents;
using Microsoft.SemanticKernel.TextToAudio;
using Microsoft.SemanticKernel;
#pragma warning disable SKEXP0005
const string TextToAudioModel = "tts-1";
const string AudioToTextModel = "whisper-1";
const string AudioFilePath = "audio.wav";
var openAIApiKey = File.ReadAllText(@"C:\GPT\key.txt");

await TextToAudioAsync();
await AudioToTextAsync();
async Task TextToAudioAsync()
{
    // Create a kernel with OpenAI text to audio service
    var kernel = Kernel.CreateBuilder()
        .AddOpenAITextToAudio(
            modelId: TextToAudioModel,
            apiKey: openAIApiKey)
        .Build();

    var textToAudioService = kernel.GetRequiredService<ITextToAudioService>();

    var sampleText = $"老板，苹果多少钱一斤？";

    // Set execution settings (optional)
    var executionSettings = new OpenAITextToAudioExecutionSettings("alloy")
    {
        ResponseFormat = "mp3", // The format to audio in.
                                // Supported formats are mp3, opus, aac, and flac.
        Speed = 1.0f // The speed of the generated audio.
                     // Select a value from 0.25 to 4.0. 1.0 is the default.
    };

    // Convert text to audio
    var audioContent = await textToAudioService.GetAudioContentAsync(sampleText, executionSettings);

    // Save audio content to a file
    await File.WriteAllBytesAsync(AudioFilePath, audioContent.Data!.ToArray());
}


async Task AudioToTextAsync()
{
    // Create a kernel with OpenAI audio to text service
    var kernel = Kernel.CreateBuilder()
        .AddOpenAIAudioToText(
            modelId: AudioToTextModel,
            apiKey: openAIApiKey)
        .Build();

    var audioToTextService = kernel.GetRequiredService<IAudioToTextService>();

    // Set execution settings (optional)
    OpenAIAudioToTextExecutionSettings executionSettings = new(AudioFilePath)
    {
        Language = "zh", // The language of the audio data as two-letter ISO-639-1 language code (e.g. 'en' or 'es').
        Prompt = "sample prompt", // An optional text to guide the model's style or continue a previous audio segment.
                                  // The prompt should match the audio language.
        ResponseFormat = "json", // The format to return the transcribed text in.
                                 // Supported formats are json, text, srt, verbose_json, or vtt. Default is 'json'.
        Temperature = 0.3f, // The randomness of the generated text.
                            // Select a value from 0.0 to 1.0. 0 is the default.
    };

    // Read audio content from a file
    ReadOnlyMemory<byte> audioData = await File.ReadAllBytesAsync(AudioFilePath);
    AudioContent audioContent = new(new BinaryData(audioData));

    // Convert audio to text
    var textContent = await audioToTextService.GetTextContentAsync(audioContent, executionSettings);

    // Output the transcribed text
    Console.WriteLine(textContent.Text);
}
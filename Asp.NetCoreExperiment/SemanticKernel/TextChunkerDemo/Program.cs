﻿#pragma warning disable SKEXP0055

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.DeepDev;
using Microsoft.ML.Tokenizers;
using Microsoft.SemanticKernel.Text;
using SharpToken;
using TextChunkerDemo;
using static Microsoft.SemanticKernel.Text.TextChunker;

var demo = new Demo();
//demo.RunExample();

demo.RunExampleForTokenCounterType(TokenCounterType.SharpToken);
class Demo
{

    const string Text = @"The city of Venice, located in the northeastern part of Italy,
is renowned for its unique geographical features. Built on more than 100 small islands in a lagoon in the
Adriatic Sea, it has no roads, just canals including the Grand Canal thoroughfare lined with Renaissance and
Gothic palaces. The central square, Piazza San Marco, contains St. Mark's Basilica, which is tiled with Byzantine
mosaics, and the Campanile bell tower offering views of the city's red roofs.

The Amazon Rainforest, also known as Amazonia, is a moist broadleaf tropical rainforest in the Amazon biome that
covers most of the Amazon basin of South America. This basin encompasses 7 million square kilometers, of which
5.5 million square kilometers are covered by the rainforest. This region includes territory belonging to nine nations
and 3.4 million square kilometers of uncontacted tribes. The Amazon represents over half of the planet's remaining
rainforests and comprises the largest and most biodiverse tract of tropical rainforest in the world.

The Great Barrier Reef is the world's largest coral reef system composed of over 2,900 individual reefs and 900 islands
stretching for over 2,300 kilometers over an area of approximately 344,400 square kilometers. The reef is located in the
Coral Sea, off the coast of Queensland, Australia. The Great Barrier Reef can be seen from outer space and is the world's
biggest single structure made by living organisms. This reef structure is composed of and built by billions of tiny organisms,
known as coral polyps.";

    public void RunExample()
    {
        Console.WriteLine("=== Text chunking ===");

        var lines = TextChunker.SplitPlainTextLines(Text, 10);
        var paragraphs = TextChunker.SplitPlainTextParagraphs(lines, 120);

        WriteParagraphsToConsole(paragraphs);
    }
    void WriteParagraphsToConsole(List<string> paragraphs)
    {
        for (var i = 0; i < paragraphs.Count; i++)
        {
            Console.WriteLine(paragraphs[i]);

            if (i < paragraphs.Count - 1)
            {
                Console.WriteLine("------------------------");
            }
        }
    }

    public void RunExampleForTokenCounterType(TokenCounterType counterType)
    {
        Console.WriteLine($"=== Text chunking with a custom({counterType}) token counter ===");
        var sw = new Stopwatch();
        sw.Start();
        var tokenCounter = s_tokenCounterFactory(counterType);

        var lines = TextChunker.SplitPlainTextLines(Text, 40, tokenCounter);
        var paragraphs = TextChunker.SplitPlainTextParagraphs(lines, 120, tokenCounter: tokenCounter);

        sw.Stop();
        Console.WriteLine($"Elapsed time: {sw.ElapsedMilliseconds} ms");
        WriteParagraphsToConsole(paragraphs);
    }

    void RunExampleWithHeader()
    {
        Console.WriteLine("=== Text chunking with chunk header ===");

        var lines = TextChunker.SplitPlainTextLines(Text, 40);
        var paragraphs = TextChunker.SplitPlainTextParagraphs(lines, 150, chunkHeader: "DOCUMENT NAME: test.txt\n\n");

        WriteParagraphsToConsole(paragraphs);
    }


    /// <summary>
    /// Custom token counter implementation using SharpToken.
    /// Note: SharpToken is used for demonstration purposes only, it's possible to use any available or custom tokenization logic.
    /// </summary>
    TokenCounter SharpTokenTokenCounter => (string input) =>
    {
        // Initialize encoding by encoding name
         //var encoding = GptEncoding.GetEncoding("cl100k_base");

        // Initialize encoding by model name
        var encoding = GptEncoding.GetEncodingForModel("gpt-4");

        var tokens = encoding.Encode(input);

        return tokens.Count;
    };

    /// <summary>
    /// MicrosoftML token counter implementation.
    /// </summary>
    TokenCounter MicrosoftMLTokenCounter => (string input) =>
    {
        Tokenizer tokenizer = new(new Bpe());
        var tokens = tokenizer.Encode(input).Tokens;

        return tokens.Count;
    };

    /// <summary>
    /// MicrosoftML token counter implementation using Roberta and local vocab
    /// </summary>
    TokenCounter MicrosoftMLRobertaTokenCounter => (string input) =>
    {
        var encoder = EmbeddedResource.ReadStream("EnglishRoberta.encoder.json");
        var vocab = EmbeddedResource.ReadStream("EnglishRoberta.vocab.bpe");
        var dict = EmbeddedResource.ReadStream("EnglishRoberta.dict.txt");

        if (encoder is null || vocab is null || dict is null)
        {
            throw new FileNotFoundException("Missing required resources");
        }

        EnglishRoberta model = new(encoder, vocab, dict);

        model.AddMaskSymbol(); // Not sure what this does, but it's in the example
        Tokenizer tokenizer = new(model, new RobertaPreTokenizer());
        var tokens = tokenizer.Encode(input).Tokens;

        return tokens.Count;
    };

    /// <summary>
    /// DeepDev token counter implementation.
    /// </summary>
    TokenCounter DeepDevTokenCounter => (string input) =>
   {
       // Initialize encoding by encoding name
       var tokenizer = TokenizerBuilder.CreateByEncoderNameAsync("cl100k_base").GetAwaiter().GetResult();

       // Initialize encoding by model name
       // var tokenizer = TokenizerBuilder.CreateByModelNameAsync("gpt-4").GetAwaiter().GetResult();

       var tokens = tokenizer.Encode(input, new HashSet<string>());
       return tokens.Count;
   };

    TokenCounter s_tokenCounterFactory(TokenCounterType counterType)
    {
        switch (counterType)
        {
            case TokenCounterType.SharpToken: return SharpTokenTokenCounter;
            case TokenCounterType.MicrosoftML: return MicrosoftMLTokenCounter;
            case TokenCounterType.DeepDev: return DeepDevTokenCounter;
            case TokenCounterType.MicrosoftMLRoberta: return MicrosoftMLRobertaTokenCounter;
            default: throw new ArgumentOutOfRangeException(nameof(counterType), counterType, null);
        }
    }
}
public enum TokenCounterType
{
    SharpToken,
    MicrosoftML,
    DeepDev,
    MicrosoftMLRoberta,
}
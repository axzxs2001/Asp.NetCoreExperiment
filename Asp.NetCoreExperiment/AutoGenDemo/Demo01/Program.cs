using AutoGen.OpenAI;
using AutoGen;
using OpenAI;
using AutoGen.OpenAI.Extension;
using AutoGen.Core;

var openAIKey = File.ReadAllText("C:/gpt/key.txt");
var openAIClient = new OpenAIClient(openAIKey);
var model = "gpt-4o-mini";

var assistantAgent = new OpenAIChatAgent(
    name: "assistant",
    systemMessage: "您是帮助用户完成某些任务的助手。",
    chatClient: openAIClient.GetChatClient(model))
    .RegisterMessageConnector()
    .RegisterPrintMessage(); // register a hook to print message nicely to console

// set human input mode to ALWAYS so that user always provide input
var userProxyAgent = new UserProxyAgent(
    name: "user",
    humanInputMode: HumanInputMode.ALWAYS)
    .RegisterPrintMessage();

// start the conversation
await userProxyAgent.InitiateChatAsync(
    receiver: assistantAgent,
    message: "Hey assistant, please do me a favor.",
    maxRound: 10);
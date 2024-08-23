
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System;
#pragma warning disable SKEXP0010
var kernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion(
        modelId: "phi-3.5",
        apiKey: "lm-studio",
        endpoint: new Uri("http://localhost:1234"))
    .Build();

//var kernel = Kernel.CreateBuilder()
//    .AddOpenAIChatCompletion(
//        modelId: "llama3",
//        apiKey: "lm-studio",
//        endpoint: new Uri("http://localhost:1234"))
//    .Build();

//var prompt = @"将三个反引号之间的文本重写为商务邮件。使用专业语气，清晰简洁。
//以 AI 助手身份签名邮件。

//文本：```{{$input}}```";

//var mailFunction = kernel.CreateFunctionFromPrompt(prompt, new OpenAIPromptExecutionSettings
//{
//    TopP = 0.5,
//    MaxTokens = 1000,
//});

//var response = kernel.InvokeStreamingAsync(mailFunction, new() { ["input"] = "告诉小明我将在本周末之前完成商业计划。请用中文来写邮件。" });
//await foreach (var  item in response)
//{
//    Console.Write(item.ToString());
//}
//Console.WriteLine(response);


var chat = kernel.GetRequiredService<IChatCompletionService>();
var systemPrompt = @"
角色：你是一位高级文字信息分析专家，有很强的理解能力，数据提取能力，分类能力，格式转换能力。
任务：
1、你能从User输入的内容中，提取模版中对应的数据项，并组装成Json字符串，只组装有值的数据项，格式为[{""index"":1,""value"":值1},{""index"":2,""value"":""值2""}]。
2、在组装Json信息时，要按照模版中每个数据项提供的的Prompt属性要求进行数据报取、转换、汇总。
要求：
1、请仔细分数用户数据，反复提取，找出更多的数据项内容。
2、直接输出的结果是纯Json字符串，不要以```json，以```结束。
------模版数据项-------
{{subprompt}}
";
var subprompt = """
[
  {
    "Index": 0,
    "Prompt": "名前（例：张三）"
  },
  {
    "Index": 1,
    "Prompt": "名前を全角のカタカナに変換します（例: チョウサン）"
  },
  {
    "Index": 2,
    "Prompt": "名を半角カタカナに変換します（例：ﾁｮｳｻﾝ）"
  },
  {
    "Index": 3,
    "Prompt": "名を英語表記に変換します（例：ChoSan）"
  },
  {
    "Index": 4,
    "Prompt": "電話番号（例：030-1234-5678）"
  },
  {
    "Index": 5,
    "Prompt": "メールアドレス または email"
  },
  {
    "Index": 6,
    "Prompt": "生年月日(格式：2022-02-02)"
  },
  {
    "Index": 7,
    "Prompt": "郵便番号または〒で始まる郵便番号、〒は不要です（例：111-0032）"
  },
  {
    "Index": 8,
    "Prompt": "住所"
  },
  {
    "Index": 9,
    "Prompt": "職業[無職:0,技術者:1,教師:2,医師:3,芸術家:4,料理人:5,商人:6]"
  },
  {
    "Index": 10,
    "Prompt": "性別-男性"
  },
  {
    "Index": 11,
    "Prompt": "性別-女性"
  },
  {
    "Index": 12,
    "Prompt": "性別-その他"
  },
  {
    "Index": 13,
    "Prompt": "趣味-スポーツ"
  },
  {
    "Index": 14,
    "Prompt": "趣味-音楽"
  },
  {
    "Index": 15,
    "Prompt": "趣味-芸術"
  },
  {
    "Index": 16,
    "Prompt": "其他：汇总信息，80字以内"
  }
]
""";
systemPrompt = systemPrompt.Replace("{{subprompt}}", subprompt);
var chatHistory = new ChatHistory(systemPrompt);
//var html = """
//    <div class="container mt-5">
//        <h2>订单录入</h2>
//        <form>
//            <div class="form-row">
//                <div class="form-group col-md-6">
//                    <label for="orderNumber">订单编号</label>
//                    <input type="text" class="form-control" id="orderNumber" placeholder="订单编号">
//                </div>
//                <div class="form-group col-md-6">
//                    <label for="orderDate">订单日期</label>
//                    <input type="date" class="form-control" id="orderDate">
//                </div>
//            </div>

//            <h4>客户信息</h4>
//            <div class="form-row">
//                <div class="form-group col-md-4">
//                    <label for="customerNumber">客户编号</label>
//                    <input type="text" class="form-control" id="customerNumber" placeholder="客户编号">
//                </div>
//                <div class="form-group col-md-4">
//                    <label for="customerName">客户姓名</label>
//                    <input type="text" class="form-control" id="customerName" placeholder="客户姓名">
//                </div>
//                <div class="form-group col-md-4">
//                    <label for="customerContact">客户联系方式</label>
//                    <input type="text" class="form-control" id="customerContact" placeholder="客户联系方式">
//                </div>
//            </div>
//            <div class="form-row">
//                <div class="form-group col-md-12">
//                    <label for="customerAddress">客户地址</label>
//                    <input type="text" class="form-control" id="customerAddress" placeholder="客户地址">
//                </div>
//            </div>

//            <h4>明细信息</h4>
//            <div id="medicineContainer">
//                <div class="form-row align-items-end medicine-row">
//                    <div class="form-group col-md-2">
//                        <label for="medicineCode">药品编号</label>
//                    </div>
//                    <div class="form-group col-md-3">
//                        <label for="medicineName">药品名称</label>
//                    </div>
//                    <div class="form-group col-md-2">
//                        <label for="medicineSpec">药品规格</label>
//                    </div>
//                    <div class="form-group col-md-1">
//                        <label for="medicineQuantity">数量</label>
//                    </div>
//                    <div class="form-group col-md-2">
//                        <label for="medicinePrice">单价</label>
//                    </div>
//                    <div class="form-group col-md-2">
//                        <label for="totalPrice">总价</label>
//                    </div>
//                </div>
//                <div class="form-row align-items-end medicine-row">
//                    <div class="form-group col-md-2">
//                        <input type="text" class="form-control" id="medicineCode" placeholder="药品编号">
//                    </div>
//                    <div class="form-group col-md-3">
//                        <input type="text" class="form-control" id="medicineName" placeholder="药品名称">
//                    </div>
//                    <div class="form-group col-md-2">
//                        <input type="text" class="form-control" id="medicineSpec" placeholder="药品规格">
//                    </div>
//                    <div class="form-group col-md-1">
//                        <input type="number" class="form-control" id="medicineQuantity" placeholder="数量">
//                    </div>
//                    <div class="form-group col-md-2">

//                        <input type="number" class="form-control" id="medicinePrice" placeholder="单价">
//                    </div>
//                    <div class="form-group col-md-2">
//                        <input type="number" class="form-control" id="totalPrice" placeholder="总价" readonly="">
//                    </div>
//                </div>

//                  <div class="form-row align-items-end medicine-row">
//                    <div class="form-group col-md-2">                   
//                        <input type="text" class="form-control" id="medicineCode" placeholder="药品编号">
//                    </div>
//                    <div class="form-group col-md-3">                      
//                        <input type="text" class="form-control" id="medicineName" placeholder="药品名称">
//                    </div>
//                    <div class="form-group col-md-2">                 
//                        <input type="text" class="form-control" id="medicineSpec" placeholder="药品规格">
//                    </div>
//                    <div class="form-group col-md-1">                      
//                        <input type="number" class="form-control" id="medicineQuantity" placeholder="数量">
//                    </div>
//                    <div class="form-group col-md-2">

//                        <input type="number" class="form-control" id="medicinePrice" placeholder="单价">
//                    </div>
//                    <div class="form-group col-md-2">                     
//                        <input type="number" class="form-control" id="totalPrice" placeholder="总价" readonly="">
//                    </div>
//                </div>
//                  <div class="form-row align-items-end medicine-row">
//                    <div class="form-group col-md-2">                   
//                        <input type="text" class="form-control" id="medicineCode" placeholder="药品编号">
//                    </div>
//                    <div class="form-group col-md-3">                      
//                        <input type="text" class="form-control" id="medicineName" placeholder="药品名称">
//                    </div>
//                    <div class="form-group col-md-2">                 
//                        <input type="text" class="form-control" id="medicineSpec" placeholder="药品规格">
//                    </div>
//                    <div class="form-group col-md-1">                      
//                        <input type="number" class="form-control" id="medicineQuantity" placeholder="数量">
//                    </div>
//                    <div class="form-group col-md-2">

//                        <input type="number" class="form-control" id="medicinePrice" placeholder="单价">
//                    </div>
//                    <div class="form-group col-md-2">                     
//                        <input type="number" class="form-control" id="totalPrice" placeholder="总价" readonly="">
//                    </div>
//                </div>
//                  <div class="form-row align-items-end medicine-row">
//                    <div class="form-group col-md-2">                   
//                        <input type="text" class="form-control" id="medicineCode" placeholder="药品编号">
//                    </div>
//                    <div class="form-group col-md-3">                      
//                        <input type="text" class="form-control" id="medicineName" placeholder="药品名称">
//                    </div>
//                    <div class="form-group col-md-2">                 
//                        <input type="text" class="form-control" id="medicineSpec" placeholder="药品规格">
//                    </div>
//                    <div class="form-group col-md-1">                      
//                        <input type="number" class="form-control" id="medicineQuantity" placeholder="数量">
//                    </div>
//                    <div class="form-group col-md-2">

//                        <input type="number" class="form-control" id="medicinePrice" placeholder="单价">
//                    </div>
//                    <div class="form-group col-md-2">                     
//                        <input type="number" class="form-control" id="totalPrice" placeholder="总价" readonly="">
//                    </div>
//                </div></div>
//            <button type="button" class="btn btn-secondary mt-3" id="addMedicine">添加药品</button>
//            <button type="submit" class="btn btn-primary mt-3">提交订单</button>
//        </form>
//        <div class="form-row">
//            <div class="form-group col-md-6">
//                <label for="orderNumber">总金额</label>
//                <input type="text" class="form-control" readonly="" id="amount" placeholder="0.00">
//            </div>
//            <div class="form-group col-md-6">
//                <label for="orderDate">总数量</label>
//                <input type="date" class="form-control" readonly="" id="quantity">
//            </div>
//        </div>
//    </div>
//    """;
//var json = """
//    [
//      {
//        "Index": 0,
//        "Prompt": "订单编号",
//        "IsRepeat": false
//      },
//      {
//        "Index": 1,
//        "Prompt": "订单日期",
//        "IsRepeat": false
//      },
//      {
//        "Index": 2,
//        "Prompt": "客户编号",
//        "IsRepeat": false
//      },
//      {
//        "Index": 3,
//        "Prompt": "客户姓名",
//        "IsRepeat": false
//      },
//      {
//        "Index": 4,
//        "Prompt": "客户联系方式",
//        "IsRepeat": false
//      },
//      {
//        "Index": 5,
//        "Prompt": "客户地址",
//        "IsRepeat": false
//      },
//      {
//        "Index": 6,
//        "Prompt": "药品编号",
//        "IsRepeat": true
//      },
//      {
//        "Index": 7,
//        "Prompt": "药品名称",
//        "IsRepeat": true
//      },
//      {
//        "Index": 8,
//        "Prompt": " 药品规格",
//        "IsRepeat": true
//      },
//      {
//        "Index": 9,
//        "Prompt": " 数量",
//        "IsRepeat": true
//      },
//      {
//        "Index": 10,
//        "Prompt": "单价",
//        "IsRepeat": true
//      },
//      {
//        "Index": 11,
//        "Prompt": "总价= 数量*单价",
//        "IsRepeat": true
//      }
//    ]
//    """;
chatHistory.AddUserMessage(new ChatMessageContentItemCollection
{
     new TextContent($@"私の名前は桂素偉です，男性で，1979年6月22日生まれ、千葉県習志野市津田沼出身です。現在、プログラムアーキテクトとして働いています。普段はあまりスポーツをしませんが、最近は登山とハイキングに興味があります。
最近、生成AIの研究に力を入れており、これを活用して人々の日常生活を便利にすることを目指しています。また、コミュニティ活動にも参加し、AIに関する知識を共有しています。
【連絡先】住所: 千葉県習志野市津田沼2-17-1, 〒275-0016携帯電話: 070-9065-7186電子メール: axxs2018@gmail.comFAX: 043-123-4567Line ID: suwei1979
")
});
var settings = new Dictionary<string, object>
{
    ["max_tokens"] = 1000,
    ["temperature"] = 0,
    ["top_p"] = 1,
    ["presence_penalty"] = 0.0,
    ["frequency_penalty"] = 0.0
};

var content = chat.GetStreamingChatMessageContentsAsync(chatHistory, new PromptExecutionSettings
{
    ExtensionData = settings
});
await foreach (var item in content)
{
    Console.Write(item.Content);
}
Console.ReadLine();
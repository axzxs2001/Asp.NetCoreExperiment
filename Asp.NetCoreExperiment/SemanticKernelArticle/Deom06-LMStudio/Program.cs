
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System;
#pragma warning disable SKEXP0010
var kernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion(
        modelId: "phi-3",
        apiKey: "lm-studio",
        endpoint: new Uri("http://localhost:1234"))
    .Build();

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
var chatHistory = new ChatHistory();
var html = """
    <div class="container mt-5">
        <h2>订单录入</h2>
        <form>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <label for="orderNumber">订单编号</label>
                    <input type="text" class="form-control" id="orderNumber" placeholder="订单编号">
                </div>
                <div class="form-group col-md-6">
                    <label for="orderDate">订单日期</label>
                    <input type="date" class="form-control" id="orderDate">
                </div>
            </div>

            <h4>客户信息</h4>
            <div class="form-row">
                <div class="form-group col-md-4">
                    <label for="customerNumber">客户编号</label>
                    <input type="text" class="form-control" id="customerNumber" placeholder="客户编号">
                </div>
                <div class="form-group col-md-4">
                    <label for="customerName">客户姓名</label>
                    <input type="text" class="form-control" id="customerName" placeholder="客户姓名">
                </div>
                <div class="form-group col-md-4">
                    <label for="customerContact">客户联系方式</label>
                    <input type="text" class="form-control" id="customerContact" placeholder="客户联系方式">
                </div>
            </div>
            <div class="form-row">
                <div class="form-group col-md-12">
                    <label for="customerAddress">客户地址</label>
                    <input type="text" class="form-control" id="customerAddress" placeholder="客户地址">
                </div>
            </div>

            <h4>明细信息</h4>
            <div id="medicineContainer">
                <div class="form-row align-items-end medicine-row">
                    <div class="form-group col-md-2">
                        <label for="medicineCode">药品编号</label>
                    </div>
                    <div class="form-group col-md-3">
                        <label for="medicineName">药品名称</label>
                    </div>
                    <div class="form-group col-md-2">
                        <label for="medicineSpec">药品规格</label>
                    </div>
                    <div class="form-group col-md-1">
                        <label for="medicineQuantity">数量</label>
                    </div>
                    <div class="form-group col-md-2">
                        <label for="medicinePrice">单价</label>
                    </div>
                    <div class="form-group col-md-2">
                        <label for="totalPrice">总价</label>
                    </div>
                </div>
                <div class="form-row align-items-end medicine-row">
                    <div class="form-group col-md-2">
                        <input type="text" class="form-control" id="medicineCode" placeholder="药品编号">
                    </div>
                    <div class="form-group col-md-3">
                        <input type="text" class="form-control" id="medicineName" placeholder="药品名称">
                    </div>
                    <div class="form-group col-md-2">
                        <input type="text" class="form-control" id="medicineSpec" placeholder="药品规格">
                    </div>
                    <div class="form-group col-md-1">
                        <input type="number" class="form-control" id="medicineQuantity" placeholder="数量">
                    </div>
                    <div class="form-group col-md-2">

                        <input type="number" class="form-control" id="medicinePrice" placeholder="单价">
                    </div>
                    <div class="form-group col-md-2">
                        <input type="number" class="form-control" id="totalPrice" placeholder="总价" readonly="">
                    </div>
                </div>

                  <div class="form-row align-items-end medicine-row">
                    <div class="form-group col-md-2">                   
                        <input type="text" class="form-control" id="medicineCode" placeholder="药品编号">
                    </div>
                    <div class="form-group col-md-3">                      
                        <input type="text" class="form-control" id="medicineName" placeholder="药品名称">
                    </div>
                    <div class="form-group col-md-2">                 
                        <input type="text" class="form-control" id="medicineSpec" placeholder="药品规格">
                    </div>
                    <div class="form-group col-md-1">                      
                        <input type="number" class="form-control" id="medicineQuantity" placeholder="数量">
                    </div>
                    <div class="form-group col-md-2">

                        <input type="number" class="form-control" id="medicinePrice" placeholder="单价">
                    </div>
                    <div class="form-group col-md-2">                     
                        <input type="number" class="form-control" id="totalPrice" placeholder="总价" readonly="">
                    </div>
                </div>
                  <div class="form-row align-items-end medicine-row">
                    <div class="form-group col-md-2">                   
                        <input type="text" class="form-control" id="medicineCode" placeholder="药品编号">
                    </div>
                    <div class="form-group col-md-3">                      
                        <input type="text" class="form-control" id="medicineName" placeholder="药品名称">
                    </div>
                    <div class="form-group col-md-2">                 
                        <input type="text" class="form-control" id="medicineSpec" placeholder="药品规格">
                    </div>
                    <div class="form-group col-md-1">                      
                        <input type="number" class="form-control" id="medicineQuantity" placeholder="数量">
                    </div>
                    <div class="form-group col-md-2">

                        <input type="number" class="form-control" id="medicinePrice" placeholder="单价">
                    </div>
                    <div class="form-group col-md-2">                     
                        <input type="number" class="form-control" id="totalPrice" placeholder="总价" readonly="">
                    </div>
                </div>
                  <div class="form-row align-items-end medicine-row">
                    <div class="form-group col-md-2">                   
                        <input type="text" class="form-control" id="medicineCode" placeholder="药品编号">
                    </div>
                    <div class="form-group col-md-3">                      
                        <input type="text" class="form-control" id="medicineName" placeholder="药品名称">
                    </div>
                    <div class="form-group col-md-2">                 
                        <input type="text" class="form-control" id="medicineSpec" placeholder="药品规格">
                    </div>
                    <div class="form-group col-md-1">                      
                        <input type="number" class="form-control" id="medicineQuantity" placeholder="数量">
                    </div>
                    <div class="form-group col-md-2">

                        <input type="number" class="form-control" id="medicinePrice" placeholder="单价">
                    </div>
                    <div class="form-group col-md-2">                     
                        <input type="number" class="form-control" id="totalPrice" placeholder="总价" readonly="">
                    </div>
                </div></div>
            <button type="button" class="btn btn-secondary mt-3" id="addMedicine">添加药品</button>
            <button type="submit" class="btn btn-primary mt-3">提交订单</button>
        </form>
        <div class="form-row">
            <div class="form-group col-md-6">
                <label for="orderNumber">总金额</label>
                <input type="text" class="form-control" readonly="" id="amount" placeholder="0.00">
            </div>
            <div class="form-group col-md-6">
                <label for="orderDate">总数量</label>
                <input type="date" class="form-control" readonly="" id="quantity">
            </div>
        </div>
    </div>
    """;
var json = """
    [
      {
        "Index": 0,
        "Prompt": "订单编号",
        "IsRepeat": false
      },
      {
        "Index": 1,
        "Prompt": "订单日期",
        "IsRepeat": false
      },
      {
        "Index": 2,
        "Prompt": "客户编号",
        "IsRepeat": false
      },
      {
        "Index": 3,
        "Prompt": "客户姓名",
        "IsRepeat": false
      },
      {
        "Index": 4,
        "Prompt": "客户联系方式",
        "IsRepeat": false
      },
      {
        "Index": 5,
        "Prompt": "客户地址",
        "IsRepeat": false
      },
      {
        "Index": 6,
        "Prompt": "药品编号",
        "IsRepeat": true
      },
      {
        "Index": 7,
        "Prompt": "药品名称",
        "IsRepeat": true
      },
      {
        "Index": 8,
        "Prompt": " 药品规格",
        "IsRepeat": true
      },
      {
        "Index": 9,
        "Prompt": " 数量",
        "IsRepeat": true
      },
      {
        "Index": 10,
        "Prompt": "单价",
        "IsRepeat": true
      },
      {
        "Index": 11,
        "Prompt": "总价= 数量*单价",
        "IsRepeat": true
      }
    ]
    """;
chatHistory.AddUserMessage(new ChatMessageContentItemCollection
{
     new TextContent($@"{html}
---------------
根据上面html，完善下面json对应的信息，并输出完整的json，如果有重复的项目，请用第一组，第二组区分
json信息如下：
{json}
")
});
var settings = new Dictionary<string, object>
{
    ["max_tokens"] = 1000,
    ["temperature"] = 0.2,
    ["top_p"] = 0.8,
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
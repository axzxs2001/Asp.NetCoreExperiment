#pragma warning disable SKEXP0080 
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Reflection;
using System.Text.Json.Serialization;

#region 流程定义
var process = new ProcessBuilder("Trip");
var getCustomerStep = process.AddStepFromType<GetCustomerStep>();
var tripAirStep = process.AddStepFromType<TripAirTicketProcessStep>();
var tripHotelStep = process.AddStepFromType<TripHotelProcessStep>();
var tripCarStep = process.AddStepFromType<TripCarProcessStep>();
var mailServiceStep = process.AddStepFromType<MailServiceStep>();
var tripCompleteStep = process.AddStepFromType<TripCompleteStep>();
var stopProcessStep = process.AddStepFromType<StopProcessStep>();
#endregion

#region 组织流程
process.OnInputEvent(TripEvents.StartProcess)
          .SendEventTo(new ProcessFunctionTargetBuilder(getCustomerStep, GetCustomerStep.Functions.GetCustomer));

getCustomerStep
    .OnEvent(TripEvents.GetCustomer)
    .SendEventTo(new ProcessFunctionTargetBuilder(tripAirStep, TripAirTicketProcessStep.Functions.TripAirTicket, parameterName: "customer"));

tripAirStep
    .OnEvent(TripEvents.TripAirTicketSuccess)
    .SendEventTo(new ProcessFunctionTargetBuilder(tripHotelStep, TripHotelProcessStep.Functions.TripHotel));

tripAirStep
    .OnEvent(TripEvents.TripAirTicketFail)
    .SendEventTo(new ProcessFunctionTargetBuilder(mailServiceStep, MailServiceStep.Functions.SendMailToUserWithDetails, parameterName: "message"));

tripHotelStep
    .OnEvent(TripEvents.TripHotelSuccess)
    .SendEventTo(new ProcessFunctionTargetBuilder(tripCarStep, TripCarProcessStep.Functions.TripCar));

tripHotelStep
    .OnEvent(TripEvents.TripHotelFail)
    .SendEventTo(new ProcessFunctionTargetBuilder(mailServiceStep, MailServiceStep.Functions.SendMailToUserWithDetails, parameterName: "message"));

tripCarStep
    .OnEvent(TripEvents.TripCarSuccess)
    .SendEventTo(new ProcessFunctionTargetBuilder(tripCompleteStep, TripCompleteStep.Functions.TripComplete));

tripCarStep
    .OnEvent(TripEvents.TripCarFail)
    .SendEventTo(new ProcessFunctionTargetBuilder(mailServiceStep, MailServiceStep.Functions.SendMailToUserWithDetails, parameterName: "message"));

tripCompleteStep
    .OnEvent(TripEvents.TripCompleted)
    .SendEventTo(new ProcessFunctionTargetBuilder(stopProcessStep, StopProcessStep.Functions.StopProcess));

stopProcessStep
    .OnEvent(TripEvents.StopProcess)
    .StopProcess();

#endregion

var kernelProcess = process.Build();
Kernel kernel = Kernel.CreateBuilder()
        .AddOpenAIChatCompletion(
            modelId: "gpt-4o",
            apiKey: File.ReadAllText("c:/gpt/key.txt"))
        .Build();
while (true)
{
    Console.WriteLine("请输入用户id:");
    var id = int.Parse(Console.ReadLine());
    //开始流程
    using var runningProcess = await kernelProcess.StartAsync(kernel, new KernelProcessEvent() { Id = TripEvents.StartProcess, Data = id });
}

#region 定义事件
public static class TripEvents
{
    //开始Process
    public static readonly string StartProcess = nameof(StartProcess);

    public static readonly string GetCustomer = nameof(GetCustomer);

    public static readonly string TripAirTicketSuccess = nameof(TripAirTicketSuccess);

    public static readonly string TripAirTicketFail = nameof(TripAirTicketFail);

    public static readonly string TripHotelSuccess = nameof(TripHotelSuccess);

    public static readonly string TripHotelFail = nameof(TripHotelFail);

    public static readonly string TripCarSuccess = nameof(TripCarSuccess);

    public static readonly string TripCarFail = nameof(TripCarFail);
    // 邮件服务已发送
    public static readonly string MailServiceSent = nameof(MailServiceSent);

    public static readonly string TripCompleted = nameof(TripCompleted);
    public static readonly string StopProcess = nameof(StopProcess);
}
#endregion
#region 各个Step
//public class BaseProcessStep : KernelProcessStep<InputState>
//{
//    public override ValueTask ActivateAsync(KernelProcessStepState<InputState> state)
//    {
//        return base.ActivateAsync(state);
//    }
//}
//public record InputState
//{
//    public string Input { get; init; }
//}

public class GetCustomerStep : KernelProcessStep
{
    public static class Functions
    {
        public const string GetCustomer = nameof(GetCustomer);
    }
    [KernelFunction(Functions.GetCustomer)]
    public async Task GetCustomerStepAsync(KernelProcessStepContext context, int id, Kernel _kernel)
    {
        Console.WriteLine($"【查询客户】开始,id={id}");
        var customers = new List<CustomerForm>()
        {
            new CustomerForm()
            {
                UserFirstName = "张",
                UserLastName = "三",
                UserDateOfBirth = "1990-01-01",
                UserState = "上海",
                UserPhoneNumber = "13800000000",
                UserId = 1,
                UserEmail = "",
                State = "1"
            },
            new CustomerForm()
            {
                UserFirstName = "李",
                UserLastName = "四",
                UserDateOfBirth = "1990-01-01",
                UserState = "上海",
                UserPhoneNumber = "13800000000",
                UserId = 2,
                UserEmail = "",
                State = "2"
            },
            new CustomerForm()
            {
                UserFirstName = "王",
                UserLastName = "五",
                UserDateOfBirth = "1990-01-01",
                UserState = "上海",
                UserPhoneNumber = "13800000000",
                UserId = 3,
                UserEmail = "",
                State = "3"
            },
            new CustomerForm()
            {
                UserFirstName = "赵",
                UserLastName = "六",
                UserDateOfBirth = "1990-01-01",
                UserState = "上海",
                UserPhoneNumber = "13800000000",
                UserId = 4,
                UserEmail = "",
                State = "4"
            }
        };
        var customer = customers.SingleOrDefault(s => s.UserId == id);
        Console.WriteLine($"【查询客户】成功:{customer}");     
        await context.EmitEventAsync(new() { Id = TripEvents.GetCustomer, Data = customer, Visibility = KernelProcessEventVisibility.Public });
    }
}
public class TripAirTicketProcessStep : KernelProcessStep
{
    public static class Functions
    {
        public const string TripAirTicket = nameof(TripAirTicket);
    }

    [KernelFunction(Functions.TripAirTicket)]
    public async Task TripAirTicketAsync(KernelProcessStepContext context, CustomerForm customer, Kernel _kernel)
    {
        Console.WriteLine($"【机票预订】开始，姓名：{customer.UserFirstName}{customer.UserLastName}");
        var chat = _kernel.GetRequiredService<IChatCompletionService>();
        var chatHistory = new ChatHistory();
        chatHistory.AddUserMessage("生成一个国际航班的时刻表，标题是“| 航班号 | 出发地 | 目的地 | 出发时间 | 到达时间 | 航空公司 |”，只返回表格，不返回其他任何信息");
        var chatResult = chat.GetStreamingChatMessageContentsAsync(chatHistory);
        await foreach (var item in chatResult)
        {
            Console.Write(item);
        }
        Console.WriteLine();
        if (customer.State == "1")
        {
            Console.WriteLine("【机票预订】失败");
            await context.EmitEventAsync(new()
            {
                Id = TripEvents.TripAirTicketFail,
                Data = "我们遗憾地通知您，机票预订失败！",
                Visibility = KernelProcessEventVisibility.Public,
            });
            return;
        }

        Console.WriteLine("【机票预订】成功");
        await context.EmitEventAsync(new() { Id = TripEvents.TripAirTicketSuccess, Data = customer, Visibility = KernelProcessEventVisibility.Public });
    }
}
public class TripHotelProcessStep : KernelProcessStep
{
    public static class Functions
    {
        public const string TripHotel = nameof(TripHotel);
    }
    [KernelFunction(Functions.TripHotel)]
    public async Task TripHotelAsync(KernelProcessStepContext context, CustomerForm customer, Kernel _kernel)
    {
        Console.WriteLine($"【酒店预订】开始，姓名：{customer.UserFirstName}{customer.UserLastName}");
        if (customer.State == "2")
        {
            Console.WriteLine("【酒店预订】失败");
            await context.EmitEventAsync(new()
            {
                Id = TripEvents.TripHotelFail,
                Data = "我们遗憾地通知您，酒店预订失败！",
                Visibility = KernelProcessEventVisibility.Public,
            });
            return;
        }
        Console.WriteLine("【酒店预订】成功");
        await context.EmitEventAsync(new() { Id = TripEvents.TripHotelSuccess, Data = customer, Visibility = KernelProcessEventVisibility.Public });
    }
}
public class TripCarProcessStep : KernelProcessStep
{
    public static class Functions
    {
        public const string TripCar = nameof(TripCar);
    }
    [KernelFunction(Functions.TripCar)]
    public async Task TripCarAsync(KernelProcessStepContext context, CustomerForm customer, Kernel _kernel)
    {
        Console.WriteLine($"【租车预订】开始，姓名：{customer.UserFirstName}{customer.UserLastName}");
        if (customer.State == "3")
        {
            Console.WriteLine("【租车预订】失败");
            await context.EmitEventAsync(new()
            {
                Id = TripEvents.TripCarFail,
                Data = "我们遗憾地通知您，租车预订失败！",
                Visibility = KernelProcessEventVisibility.Public,
            });
            return;
        }
        Console.WriteLine("【租车预订】成功");
        await context.EmitEventAsync(new() { Id = TripEvents.TripCarSuccess, Data = customer, Visibility = KernelProcessEventVisibility.Public });
    }
}
public class TripCompleteStep : KernelProcessStep
{
    public static class Functions
    {
        public const string TripComplete = nameof(TripComplete);
    }
    [KernelFunction(Functions.TripComplete)]
    public async Task TripCompleteAsync(KernelProcessStepContext context, CustomerForm customer, Kernel _kernel)
    {
        Console.WriteLine("【总预订】成功");
        await context.EmitEventAsync(new() { Id = TripEvents.TripCompleted, Data = true, Visibility = KernelProcessEventVisibility.Public });
    }
}
public class StopProcessStep : KernelProcessStep
{
    public static class Functions
    {
        public const string StopProcess = nameof(StopProcess);
    }
    [KernelFunction(Functions.StopProcess)]
    public async Task StopProcessAsync(KernelProcessStepContext context, bool result, Kernel _kernel)
    {
        Console.WriteLine("全部结束");
        await Task.Delay(10);
    }
}


public class MailServiceStep : KernelProcessStep
{
    public static class Functions
    {
        public const string SendMailToUserWithDetails = nameof(SendMailToUserWithDetails);
    }

    [KernelFunction(Functions.SendMailToUserWithDetails)]
    public async Task SendMailServiceAsync(KernelProcessStepContext context, string message)
    {
        Console.WriteLine("======== 邮件服务 ======== ");
        Console.WriteLine(message);
        Console.WriteLine("============================== ");

        await context.EmitEventAsync(new() { Id = TripEvents.StopProcess, Data = false });
    }
}
#endregion
#region 各Step中传输的实体类
public class CustomerForm
{
    [JsonPropertyName("userFirstName")]
    public string UserFirstName { get; set; } = string.Empty;

    [JsonPropertyName("userLastName")]
    public string UserLastName { get; set; } = string.Empty;

    [JsonPropertyName("userDateOfBirth")]
    public string UserDateOfBirth { get; set; } = string.Empty;

    [JsonPropertyName("userState")]
    public string UserState { get; set; } = string.Empty;

    [JsonPropertyName("userPhoneNumber")]
    public string UserPhoneNumber { get; set; } = string.Empty;

    [JsonPropertyName("userId")]
    public int UserId { get; set; }


    [JsonPropertyName("userEmail")]
    public string UserEmail { get; set; } = string.Empty;

    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"姓名：{UserFirstName}{UserLastName}，生日：{UserDateOfBirth}，电话：{UserPhoneNumber}，邮箱：{UserEmail}，状态：{State}";
    }

}
#endregion
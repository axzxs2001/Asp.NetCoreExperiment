using Google.Apis.Auth.OAuth2;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Functions.V2;
using Google.Cloud.Storage.V1;
using System.Security.AccessControl;
using Google.Cloud.Functions.V2;
using Google.Api.Gax.ResourceNames;
using Google.LongRunning;
using Google.Cloud.Iam.V1;
using Google.Cloud.Run.V2;
using Google.Protobuf.WellKnownTypes;

//await Upload();
await DeleteCode();
//CreateFunction();
//await SetPolcy();
//DeleteFunction();

async System.Threading.Tasks.Task Upload()
{

    //var credential = GoogleCredential.FromFile("C:\\Users\\axzxs\\AppData\\Roaming\\gcloud\\application_default_credentials.json");
    var storageClient = await StorageClient.CreateAsync();

    using var fileStream = File.OpenRead("code.zip");

    var result = await storageClient.UploadObjectAsync("smartapi-test", "testdir/code.zip", null, source: fileStream);
}
async System.Threading.Tasks.Task DeleteCode()
{
    var storageClient = await StorageClient.CreateAsync();   
    await storageClient.DeleteObjectAsync("smartapi-test", "testdir/code.zip");
}

async System.Threading.Tasks.Task SetPolcy()
{
    var projectId = "bard-386905";
    string serviceName = "function-01";
    var Region = "asia-northeast1";
    // 创建服务客户端
    ServicesClient servicesClient = ServicesClient.Create();

    // 构造服务的全路径名称
    string serviceFullName = $"projects/{projectId}/locations/{Region}/services/{serviceName}";

    // 获取当前的 IAM 策略
    var request = new GetIamPolicyRequest();
    request.Resource = serviceFullName;
    Google.Cloud.Iam.V1.Policy policy = await servicesClient.GetIamPolicyAsync(request);

    // 给 "allUsers" 赋予 "roles/run.invoker" 角色
    Binding binding = new Binding
    {
        Role = "roles/run.invoker",
        Members = { "allUsers" } // "allUsers" 表示允许未通过身份验证的调用
    };

    // 更新 IAM 策略
    policy.Bindings.Add(binding);
    var setRequest = new SetIamPolicyRequest();
    setRequest.Resource = serviceFullName;
    setRequest.Policy = policy;
    await servicesClient.SetIamPolicyAsync(setRequest);

    Console.WriteLine($"Updated IAM policy for Cloud Run service {serviceName} to allow unauthenticated access.");


}
void CreateFunction()
{
    var functionServiceClient = new FunctionServiceClientBuilder().Build();

    var projectId = "bard-386905";
    var location = "asia-northeast1";

    string functionId = "function-01";
    string parent = $"projects/{projectId}/locations/{location}";
    string functionName = $"projects/{projectId}/locations/{location}/functions/{functionId}";

    var request = new CreateFunctionRequest
    {

        Parent = parent,
        //ParentAsLocationName = new LocationName(projectId, location),
        Function = new Function
        {
            Name = functionName,
            BuildConfig = new BuildConfig
            {
                Runtime = "dotnet8",
                EntryPoint = "FuncationDemo01.Function",
                Source = new Source
                {
                    StorageSource = new Google.Cloud.Functions.V2.StorageSource
                    {
                        Bucket = "smartapi-test",
                        Object = "testdir/code.zip",
                        Generation = 1
                    }
                },

            },
            ServiceConfig = new ServiceConfig
            {
                IngressSettings = ServiceConfig.Types.IngressSettings.AllowAll,
                Uri = $"https://{location}-{projectId}.cloudfunctions.net/{functionId}"
            },
            //EventTrigger = new EventTrigger
            //{
            //    EventType = "google.storage.object.finalize",
            //    Trigger = $"projects/{projectId}/buckets/qa-smartapi"
            //}
        }
    };





    // 创建函数 
    var response = functionServiceClient.CreateFunction(parent, request.Function, functionId);

    // 等待操作完成
    response = response.PollUntilCompleted();

    if (response.IsCompleted)
    {
        Console.WriteLine("函数创建成功！");
    }
    else
    {
        Console.WriteLine("函数创建失败！");
    }
}

void DeleteFunction()
{
    var functionServiceClient = new FunctionServiceClientBuilder().Build();


    var functionName = "function-01"; 
    var projectId = "bard-386905";
    var location = "asia-northeast1";



    // 构建函数的完整名称
    string fullFunctionName = $"projects/{projectId}/locations/{location}/functions/{functionName}";

    // 创建删除函数的请求
    var request = new DeleteFunctionRequest
    {
        Name = fullFunctionName
    };

    try
    {
        // 调用 DeleteFunction 方法
        Operation<Empty, OperationMetadata> response = functionServiceClient.DeleteFunction(request);

        // 等待操作完成
        response = response.PollUntilCompleted();

        if (response.IsCompleted)
        {
            Console.WriteLine("函数删除成功！");
        }
        else
        {
            Console.WriteLine("函数删除失败！");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"删除函数时发生错误：{ex.Message}");
    }
}

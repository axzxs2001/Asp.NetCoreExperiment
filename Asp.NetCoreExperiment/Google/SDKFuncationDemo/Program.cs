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

//await Upload();
CreateFunction();

async Task Upload()
{

    //var credential = GoogleCredential.FromFile("C:\\Users\\axzxs\\AppData\\Roaming\\gcloud\\application_default_credentials.json");
    var storageClient = await StorageClient.CreateAsync();

    using var fileStream = File.OpenRead("code.zip");

    var result = await storageClient.UploadObjectAsync("smartapi-test", "testdir/code.zip", null, source: fileStream);
}

async Task SetPolcy()
{
    ServicesClient servicesClient = await ServicesClient.CreateAsync();
    // 获取当前的 IAM 策略
    Policy policy = await servicesClient.GetIamPolicyAsync("gsw");

    // 添加 allUsers 成员，并授予 roles/run.invoker 角色
    Binding binding = new Binding
    {
        Role = "roles/run.invoker",
        Members = { "allUsers" }
    };
    policy.Bindings.Add(binding);
    SetIamPolicyRequest setIamPolicyRequest = new SetIamPolicyRequest
    {
        Resource = serviceFullName,
        Policy = policy
    };
    Policy updatedPolicy = await servicesClient.SetIamPolicyAsync(setIamPolicyRequest);

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
                    StorageSource = new StorageSource
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
    // 构建服务的完整名称
    string serviceFullName = $"projects/{projectId}/locations/{location}/services/{serviceName}";
    
    


    // 创建函数 
    var response = functionServiceClient.CreateFunction(parent ,request.Function,functionId);

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

//var request = new CreateFunctionRequest();
//request.Function = new Function()
//{
//    Name = "smartapi001",

//    BuildConfig = new Google.Cloud.Functions.V2.BuildConfig()
//    {
//        Source = new Google.Cloud.Functions.V2.Source()
//        {
//            StorageSource = new Google.Cloud.Functions.V2.StorageSource()
//            {
//                Bucket = "qa-smartapi",
//                Object = "tests/code.zip"
//            }
//        }
//    },
//    //https://asia-northeast1-sre-common-test-379805.cloudfunctions.net/function-1
//    EventTrigger = new EventTrigger()
//    {
//        Trigger = "HTTPS",
//        EventType = "google.storage.object.finalize",

//    }
//};

//FunctionServiceClient functionServiceClient = new FunctionServiceClientBuilder().Build();
//functionServiceClient.CreateFunction(request);

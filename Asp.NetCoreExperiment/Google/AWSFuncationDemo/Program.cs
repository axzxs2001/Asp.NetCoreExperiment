using Amazon.Lambda;
using Amazon.Lambda.Model;

string functionName = "MyLambdaFunction";
string zipFilePath = "MyLambdaFunction.zip";
string handler = "MyLambdaFunction::MyLambdaFunction.Function::FunctionHandler";
string roleArn = "arn:aws:iam::123456789012:role/execution_role";
Runtime runtime = Runtime.Dotnet6;


using (var client = new AmazonLambdaClient())
{
    var request = new CreateFunctionRequest
    {
        FunctionName = functionName,
        Handler = handler,
        Role = roleArn,
        Runtime = runtime,
        Architectures = new List<string> { Architecture.Arm64 },
        Code = new FunctionCode
        {            
            ZipFile = new MemoryStream(File.ReadAllBytes(zipFilePath))
        },
      
    };   

    try
    {
        var response = await client.CreateFunctionAsync(request);
        Console.WriteLine($"Function ARN: {response.FunctionArn}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error creating function: {ex.Message}");
    }
}

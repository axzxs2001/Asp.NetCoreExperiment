// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Google.Api.Service service = new Google.Api.Service();
var create = new Google.Cloud.Functions.V2.CreateFunctionRequest();
create.Function = new Google.Cloud.Functions.V2.Function();
create.Function.
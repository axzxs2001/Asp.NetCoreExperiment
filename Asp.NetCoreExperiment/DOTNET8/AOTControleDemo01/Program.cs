
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

var poco=JsonSerializer.Deserialize<MyPoco>("""{"Id" : 42, "AnotherId" : -1 }""");
// JsonException : The JSON property 'AnotherId' could not be mapped to any .NET member contained in type 'MyPoco'.
Console.WriteLine(poco.Id);

[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Disallow)]
public class MyPoco {
    public int Id { get; set; }
}



/*
var customer = new { ID = 1, PizCode="1234567",FirstName="素伟",LastName="桂" };

Console.WriteLine(JsonSerializer.Serialize(customer, new JsonSerializerOptions { 
    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    Encoder=JavaScriptEncoder.UnsafeRelaxedJsonEscaping
}));

Console.WriteLine(JsonSerializer.Serialize(customer, new JsonSerializerOptions {
    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseUpper,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
}));

Console.WriteLine(JsonSerializer.Serialize(customer, new JsonSerializerOptions {
    PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
}));

Console.WriteLine(JsonSerializer.Serialize(customer, new JsonSerializerOptions {
    PropertyNamingPolicy = JsonNamingPolicy.KebabCaseUpper,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
}));
*/
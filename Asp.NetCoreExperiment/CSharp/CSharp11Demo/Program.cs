//原始字符串
var firstName = "桂";
var lastName = "素伟";
var json0 =
    $$"""
    {
         "firstName": "{{firstName}}",
         "lastName": "{{lastName}}",
         "sex": "male",
         "age": 15         
     }
    """;
Console.WriteLine(json0);


var json1 =
    $$"""
    <?xml version="1.0"?>
    <Person>
      <FirstName>{{firstName}}</FirstName>
      <LastName>{{lastName}}</LastName>
      <Sex>male</Sex>
      <Age>15</Age>
    </Person>
    """;
Console.WriteLine(json1);

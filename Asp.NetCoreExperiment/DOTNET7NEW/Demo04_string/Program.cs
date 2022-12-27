

using System.Globalization;

CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("zh-cn");
Console.WriteLine($"|{"12345",-10}|{"12345",10}|");
Console.WriteLine($"{DateTime.Now:yyyy-MM-dd}");

Console.WriteLine($"{123:D10}");

Console.WriteLine($"{12.32:C2}");

var ci = new CultureInfo("zh-cn");

Console.WriteLine($"{12.32}");

Console.WriteLine($"{12.32.ToString("C2", ci)}");


var speedOfLight = 299792.458;
FormattableString message = $"The speed of light is {speedOfLight:N3} km/s.";



var messageInCurrentCulture = message.ToString();
var specificCulture = System.Globalization.CultureInfo.GetCultureInfo("en-IN");
var messageInSpecificCulture = message.ToString(specificCulture);

var messageInInvariantCulture = FormattableString.Invariant(message);

Console.WriteLine($"{System.Globalization.CultureInfo.CurrentCulture,-10} {messageInCurrentCulture}");
Console.WriteLine($"{specificCulture,-10} {messageInSpecificCulture}");
Console.WriteLine($"{"Invariant",-10} {messageInInvariantCulture}");


Console.ReadLine();

var classString = """
                  public class Customer
                  {
                      public string Name { get; set; }
                      public string Address { get; set; }
                      public string City { get; set; }
                      public string Region { get; set; }
                      public string PostalCode { get; set; }
                      public string EMail { get; set; }
                      public string Tel { get; set; }
                  }
                  """;
Console.WriteLine("C# class：\r\n{0}", classString);

var jsonString = """
                 {
                     "firstName": "John",
                     "lastName": "Smith",
                     "sex": "male",
                     "age": 25,
                     "address": 
                     {
                         "streetAddress": "21 2nd Street",
                         "city": "New York",
                         "state": "NY",
                         "postalCode": "10021"
                     },
                     "phoneNumber": 
                     [
                         {
                           "type": "home",
                           "number": "212 555-1234"
                         },
                         {
                           "type": "fax",
                           "number": "646 555-4567"
                         }
                     ]
                 } 
                 """;
Console.WriteLine("json：\r\n{0}", jsonString);

var xmlString = """
                <?xml version="1.0"?>
                <address_book>
                  <person gender="f">
                    <name>Jane Doe</name>
                    <address>
                      <street>123 Main St.</street>
                      <city>San Francisco</city>
                      <state>CA</state>
                      <zip>94117</zip>
                    </address>
                    <phone area_code=415>555-1212</phone>
                  </person>
                  <person gender="m">
                    <name>John Smith</name>
                    <phone area_code=510>555-1234</phone>
                    <email>johnsmith@somewhere.com</email>
                  </person>
                </address_book>
                """;
Console.WriteLine("xml：\r\n{0}", xmlString);


var yamlString = """
               url: http://www.yiibai.com              
               server:
                   host: http://www.yiibai.com              
               server:
                   - 120.168.0.21
                   - 120.168.0.22
                   - 120.168.0.23
             
               pi: 3.14   
               hasChild: true  
               name: '你好YAML' 
               """;
Console.WriteLine("yaml：\r\n{0}", yamlString);

Console.ReadLine();
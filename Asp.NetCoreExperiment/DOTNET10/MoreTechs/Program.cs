using System.Net;
using System.Security;

using var secure=new SecureString();
secure.AppendChar('a');
secure.AppendChar('b');
secure.AppendChar('c');
secure.AppendChar('d');
secure.AppendChar('e');

var cred=new NetworkCredential("user",secure);
Console.WriteLine(cred.SecurePassword.ToString());
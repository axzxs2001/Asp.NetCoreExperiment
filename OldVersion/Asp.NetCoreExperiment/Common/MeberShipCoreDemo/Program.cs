using System;
using Microsoft.AspNetCore.Identity;
namespace MeberShipCoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var securityStamp = Guid.NewGuid().ToString();
            Console.WriteLine(securityStamp);
            var passwordHasher = new PasswordHasher<IdentityUser>(Microsoft.Extensions.Options.Options.Create(new PasswordHasherOptions() { CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2 }));
            var hashPassword = passwordHasher.HashPassword(new IdentityUser { SecurityStamp = securityStamp }, "!abc123");
            Console.WriteLine(hashPassword);
            Console.WriteLine(passwordHasher.VerifyHashedPassword(new IdentityUser() { SecurityStamp = securityStamp }, hashPassword, "!abc123").ToString());
        }
    }
 
}

using System;
using Microsoft.AspNetCore.Identity;
namespace MeberShipCoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var guid = Guid.NewGuid().ToString();
            Console.WriteLine(guid);
            var passwordHasher = new PasswordHasher<IdentityUser>(Microsoft.Extensions.Options.Options.Create(new PasswordHasherOptions() { CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2 }));
            var hashPassword = passwordHasher.HashPassword(new IdentityUser { SecurityStamp = guid }, "!abc123");
            Console.WriteLine(hashPassword);
            Console.WriteLine(passwordHasher.VerifyHashedPassword(new IdentityUser() { SecurityStamp = guid }, hashPassword, "!abc123").ToString());
        }
    }

}

using Exam.Models;
using System.Security.Cryptography;

namespace Exam
{
    public class UserMutation
    {
       
        public async Task<User> AddUser(User user, [Service]ExamContext context, CancellationToken cancellationToken)
        {
            var password = GetRandomString(8);
            user.Password = System.Text.Encoding.UTF8.GetString(SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(password + user.Salt)));

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync(cancellationToken);
            user.Password = password;
            return user;
        }

        string GetRandomString(int length)
        {
            string result = "";
            for (int i = 0; i < length; i++)
            {
                char c = (char)new Random(Guid.NewGuid().GetHashCode()).Next(48, 123);
                result += c;
            }
            return result;
        }
    }
}

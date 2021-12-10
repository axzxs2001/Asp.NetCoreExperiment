using Exam.Models;

namespace Exam
{
    public class Mutation
    {
        [Payload]
        public async Task<User> AddUser(User user, ExamContext context, CancellationToken cancellationToken)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync(cancellationToken);
            return user;
        }
    }
}

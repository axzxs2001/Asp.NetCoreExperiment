using Dapper;
using Microsoft.Data.SqlClient;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IQuestionService, QuestionService>();
var app = builder.Build();

app.MapPost("/question", async (IQuestionService questionService, QuestionModel question) =>
{
    return await questionService.AddQuestionAsync(question);
});

app.MapPut("/question", async (IQuestionService questionService, QuestionModel question) =>
{
    return await questionService.ModifyQuestionAsync(question);
});

app.MapGet("/question/{id}", async (IQuestionService questionService, int id) =>
{
    return await questionService.GetQuestionAsync(id);

});

app.MapDelete("/question/{id}", async (IQuestionService questionService, int id) =>
{
    return await questionService.DeleteQuestionAsync(id);
});

app.Run();

public interface IQuestionService
{
    Task<QuestionModel> GetQuestionAsync(int id);
    Task<bool> AddQuestionAsync(QuestionModel question);
    Task<bool> DeleteQuestionAsync(int id);
    Task<bool> ModifyQuestionAsync(QuestionModel question);
}
public class QuestionService : IQuestionService
{
    private readonly SqlConnection _connection;
    public QuestionService(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ExamDatabase");
        _connection = new SqlConnection(connectionString);

    }
    public async Task<bool> AddQuestionAsync(QuestionModel question)
    {
        var sql = @"INSERT INTO [Questions]
           ([Question]
           ,[Score]
           ,[QuestionTypeID]
           ,[SujectTypeID])
     VALUES
           (@Question
           ,@Score
           ,@QuestionTypeID
           ,@SujectTypeID)";
        return (await _connection.ExecuteAsync(sql, question)) > 0;
    }

    public async Task<bool> DeleteQuestionAsync(int id)
    {
        var sql = @"delete from questions where id=@id";
        return (await _connection.ExecuteAsync(sql, new { id })) > 0;
    }

    public async Task<QuestionModel> GetQuestionAsync(int id)
    {
        var sql = @"select * from questions where id=@id";
        return await _connection.QuerySingleAsync<QuestionModel>(sql, new { id });
    }

    public async Task<bool> ModifyQuestionAsync(QuestionModel question)
    {
        var sql = @"UPDATE [dbo].[Questions]
   SET [Question] = @Question
      ,[Score] = @Score
      ,[QuestionTypeID] = @QuestionTypeID
      ,[SujectTypeID] = @SujectTypeID
 WHERE ID=@ID";
        return (await _connection.ExecuteAsync(sql, question)) > 0;
    }
}

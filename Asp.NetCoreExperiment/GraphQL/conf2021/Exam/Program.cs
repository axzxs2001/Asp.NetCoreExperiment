using Exam;
using Exam.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ExamContext>(options =>
      options.UseSqlServer(builder.Configuration.GetConnectionString("ExamDatabase")));

builder.Services.AddGraphQLServer().AddQueryType<Query>().AddProjections().AddFiltering().AddSorting();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.MapGraphQL("/graphql");
app.Run();

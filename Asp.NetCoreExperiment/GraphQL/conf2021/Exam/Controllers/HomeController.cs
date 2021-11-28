using Exam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Exam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExamContext _context;

        public HomeController(ILogger<HomeController> logger, ExamContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet("/InitData")]
        public async Task<bool> ExamPapers()
        {
            //添加试卷
            var examPagers = new ExamPaper[]
            {
                new ExamPaper
                {
                    Id=1,
                    Title="C#初级试题一（2021）",
                    Memo="",
                    TotalScore=100,
                    QuestionCount=10,
                    CreateTime=DateTime .Now
                },
                new ExamPaper
                {
                    Id=2,
                    Title="C#初级试题二（2021）",
                    Memo="",
                    TotalScore=100,
                    QuestionCount=10,
                    CreateTime=DateTime .Now
                },
                new ExamPaper
                {
                    Id=3,
                    Title="C#初级试题三（2021）",
                    Memo="",
                    TotalScore=100,
                    QuestionCount=10,
                    CreateTime=DateTime .Now
                },
            };
            await _context.ExamPapers.AddRangeAsync(examPagers);
            await _context.SaveChangesAsync();

            return true;
        }

    }

    class HomeControllerTerst
    {
        public void FFF()
        {
            var ContextOptions = new DbContextOptionsBuilder<ExamContext>()
               .UseSqlite(CreateInMemoryDatabase())
               .Options;
            var context = new ExamContext(ContextOptions);
            var homeCon = new HomeController(null, context);
        }
        DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            return connection;
        }
    }
}
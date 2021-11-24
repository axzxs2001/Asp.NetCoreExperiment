using Exam.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IExamContext _context;

        public HomeController(ILogger<HomeController> logger, IExamContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet("/exampapers")]
        public List<ExamPaper> ExamPapers()
        {
            return _context.ExamPapers.ToList();
        }

    }
}
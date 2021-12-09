
using Exam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Linq;

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
            try
            {
                #region 添加题型
                var questionType = new QuestionType
                {
                    TypeName = "单项选择题"
                };
                QuestionType[] questionTypes ={
                questionType,
                new QuestionType
                {
                    TypeName = "多项选择题"
                },
                new QuestionType
                {
                    TypeName = "判断题"
                }
             };
                await _context.QuestionTypes.AddRangeAsync(questionTypes);
                await _context.SaveChangesAsync();
                #endregion
                #region 添加科目类型
                var subjectType = new SubjectType
                {
                    TypeName = "C Sharp(C#)"
                };
                SubjectType[] subjectTypes = {
                subjectType,
                new SubjectType
                {
                     TypeName="SQL Server"
                }
            };
                await _context.SubjectTypes.AddRangeAsync(subjectTypes);
                await _context.SaveChangesAsync();
                #endregion
                #region 添加试卷
                var examPaper = new ExamPaper
                {
                    Title = "C#初级试题一（2021）",
                    Memo = "",            
                    CreateTime = DateTime.Now
                };
                ExamPaper[] examPagers ={
                examPaper,
                new ExamPaper
                {
                    Title="C#初级试题二（2021）",
                    Memo="",                 
                    CreateTime=DateTime .Now
                },

                new ExamPaper
                {

                    Title="C#初级试题三（2021）",
                    Memo="",                
                    CreateTime=DateTime .Now
                },
            };
                await _context.ExamPapers.AddRangeAsync(examPagers);
                await _context.SaveChangesAsync();
                #endregion
                #region 添加试题
                var question1 = new Question
                {
                    Question1 = "C#语言取消了（  ）语法。",
                    QuestionTypeId = questionType.Id,
                    SujectTypeId = subjectType.Id,
                    Score = 10
                };

                var question2 = new Question
                {
                    Question1 = @"以下类MyClass的属性count属于（ ）属性。
class MyClass
{
     int i;
     int count { get { return i; } }
}",
                    QuestionTypeId = questionType.Id,
                    SujectTypeId = subjectType.Id,
                    Score = 10
                };
                var question3 = new Question
                {
                    Question1 = "（ ）语句只能在循环语句的循环体语句序列中使用。",
                    QuestionTypeId = questionType.Id,
                    SujectTypeId = subjectType.Id,
                    Score = 10
                };
                var question4 = new Question
                {
                    Question1 = "在C#应用程序中，一般在程序的开头使用关键字（ ）来引入命名空间。",
                    QuestionTypeId = questionType.Id,
                    SujectTypeId = subjectType.Id,
                    Score = 10
                };
                var question5 = new Question
                {
                    Question1 = "异常处理使用时，一般将可能出现异常的语句放在（ ）代码块中。",
                    QuestionTypeId = questionType.Id,
                    SujectTypeId = subjectType.Id,
                    Score = 10
                };
                var question6 = new Question
                {
                    Question1 = "WinForms程序中，如果复选框控件的 Checked属性值设置为 True，表示（ ）。",
                    QuestionTypeId = questionType.Id,
                    SujectTypeId = subjectType.Id,
                    Score = 10
                };
                var question7 = new Question
                {
                    Question1 = "在ADO.NET中，SqlConnection 类所在的命名空间是（ ）。",
                    QuestionTypeId = questionType.Id,
                    SujectTypeId = subjectType.Id,
                    Score = 10
                };
                var question8 = new Question
                {
                    Question1 = "下面哪个类是用来以字节格式读写文件（ ）。",
                    QuestionTypeId = questionType.Id,
                    SujectTypeId = subjectType.Id,
                    Score = 10
                };
                var question9 = new Question
                {
                    Question1 = "C#程序从上机到得到结果的几个操作步骤依次是( )。",
                    QuestionTypeId = questionType.Id,
                    SujectTypeId = subjectType.Id,
                    Score = 10
                };
                var question10 = new Question
                {
                    Question1 = "下面的转换中不是隐式转换的是（ ）。",
                    QuestionTypeId = questionType.Id,
                    SujectTypeId = subjectType.Id,
                    Score = 10
                };
                Question[] questions = {
               question1,
               question2,
               question3,
               question4,
               question5,
               question6,
               question7,
               question8,
               question9,
               question10
            };
                await _context.Questions.AddRangeAsync(questions);
                await _context.SaveChangesAsync();
                #endregion
                #region 添加答案
                Answer[] answers =
                {
                new Answer
                {
                    QuestionId=question1.Id,
                    IsTrue=false,
                    Sequre="A",
                    Answer1="循环"
                },
                new Answer
                {
                    QuestionId=question1.Id,
                    IsTrue=true,
                    Sequre="B",
                    Answer1="指针 "
                },
                new Answer
                {
                    QuestionId=question1.Id,
                    IsTrue=false,
                    Sequre="C",
                    Answer1="判断 "
                },
                new Answer
                {
                    QuestionId=question1.Id,
                    IsTrue=false,
                    Sequre="D",
                    Answer1="数组"
                },
                new Answer
                {
                    QuestionId=question2.Id,
                    IsTrue=true,
                    Sequre="A",
                    Answer1="只读"
                },
                new Answer
                {
                    QuestionId=question2.Id,
                    IsTrue=false,
                    Sequre="B",
                    Answer1="只写"
                },
                new Answer
                {
                    QuestionId=question2.Id,
                    IsTrue=false,
                    Sequre="C",
                    Answer1="可读写"
                },
                new Answer
                {
                    QuestionId=question2.Id,
                    IsTrue=false,
                    Sequre="D",
                    Answer1="不可读不可写"
                },
                new Answer
                {
                    QuestionId=question3.Id,
                    IsTrue=true,
                    Sequre="A",
                    Answer1="break"
                },
                new Answer
                {
                    QuestionId=question3.Id,
                    IsTrue=false,
                    Sequre="B",
                    Answer1="goto"
                },
                new Answer
                {
                    QuestionId=question3.Id,
                    IsTrue=false,
                    Sequre="C",
                    Answer1="return "
                },
                new Answer
                {
                    QuestionId=question3.Id,
                    IsTrue=false,
                    Sequre="D",
                    Answer1="continue"
                },
               new Answer
                {
                    QuestionId=question4.Id,
                    IsTrue=false,
                    Sequre="A",
                    Answer1="class"
                },
                new Answer
                {
                    QuestionId=question4.Id,
                    IsTrue=true,
                    Sequre="B",
                    Answer1="using"
                },
                new Answer
                {
                    QuestionId=question4.Id,
                    IsTrue=false,
                    Sequre="C",
                    Answer1="in"
                },
                new Answer
                {
                    QuestionId=question4.Id,
                    IsTrue=false,
                    Sequre="D",
                    Answer1="this"
                },
               new Answer
                {
                    QuestionId=question5.Id,
                    IsTrue=false,
                    Sequre="A",
                    Answer1="click"
                },
                new Answer
                {
                    QuestionId=question5.Id,
                    IsTrue=false,
                    Sequre="B",
                    Answer1="catch"
                },
                new Answer
                {
                    QuestionId=question5.Id,
                    IsTrue=true,
                    Sequre="C",
                    Answer1="try"
                },
                new Answer
                {
                    QuestionId=question5.Id,
                    IsTrue=false,
                    Sequre="D",
                    Answer1="show"
                },
               new Answer
                {
                    QuestionId=question6.Id,
                    IsTrue=true,
                    Sequre="A",
                    Answer1="该复选框被选中"
                },
                new Answer
                {
                    QuestionId=question6.Id,
                    IsTrue=false,
                    Sequre="B",
                    Answer1="该复选框不被选中"
                },
                new Answer
                {
                    QuestionId=question6.Id,
                    IsTrue=false,
                    Sequre="C",
                    Answer1="不显示该复选框的文本信息"
                },
                new Answer
                {
                    QuestionId=question6.Id,
                    IsTrue=false,
                    Sequre="D",
                    Answer1="显示该复选框的文本信息"
                },
               new Answer
                {
                    QuestionId=question7.Id,
                    IsTrue=false,
                    Sequre="A",
                    Answer1="System"
                },
                new Answer
                {
                    QuestionId=question7.Id,
                    IsTrue=false,
                    Sequre="B",
                    Answer1="System.Data"
                },
                new Answer
                {
                    QuestionId=question7.Id,
                    IsTrue=false,
                    Sequre="C",
                    Answer1="System.Data.OleDb"
                },
                new Answer
                {
                    QuestionId=question7.Id,
                    IsTrue=true,
                    Sequre="D",
                    Answer1="System.Data.SqlClient"
                },
               new Answer
                {
                    QuestionId=question8.Id,
                    IsTrue=true,
                    Sequre="A",
                    Answer1="FileStream类 "
                },
                new Answer
                {
                    QuestionId=question8.Id,
                    IsTrue=false,
                    Sequre="B",
                    Answer1="StreamReade"
                },
                new Answer
                {
                    QuestionId=question8.Id,
                    IsTrue=false,
                    Sequre="C",
                    Answer1="BinaryWriter类"
                },
                new Answer
                {
                    QuestionId=question8.Id,
                    IsTrue=false,
                    Sequre="D",
                    Answer1="BinaryReader"
                },
               new Answer
                {
                    QuestionId=question9.Id,
                    IsTrue=true,
                    Sequre="A",
                    Answer1="输入、编译、运行"
                },
                new Answer
                {
                    QuestionId=question9.Id,
                    IsTrue=false,
                    Sequre="B",
                    Answer1="编译、连接、运行"
                },
                new Answer
                {
                    QuestionId=question9.Id,
                    IsTrue=false,
                    Sequre="C",
                    Answer1="输入、运行、编辑"
                },
                new Answer
                {
                    QuestionId=question9.Id,
                    IsTrue=false,
                    Sequre="D",
                    Answer1="编辑、编译、连接"
                },
               new Answer
                {
                    QuestionId=question10.Id,
                    IsTrue=false,
                    Sequre="A",
                    Answer1="int转换成short"
                },
                new Answer
                {
                    QuestionId=question10.Id,
                    IsTrue=false,
                    Sequre="B",
                    Answer1="short转换成long"
                },
                new Answer
                {
                    QuestionId=question10.Id,
                    IsTrue=false,
                    Sequre="C",
                    Answer1="char转换成int"
                },
                new Answer
                {
                    QuestionId=question10.Id,
                    IsTrue=true,
                    Sequre="D",
                    Answer1="bytes转换成float"
                },
               new Answer
                {
                    QuestionId=question3.Id,
                    IsTrue=false,
                    Sequre="A",
                    Answer1=""
                }
            };
                await _context.Answers.AddRangeAsync(answers);
                await _context.SaveChangesAsync();
                #endregion

                #region 添加用户
                var user = new User
                {
                    Name = "张三",
                    UserName = "zhangsan",
                    Password = "@f232fd(feef",
                    Salt = "sfw32==",
                    Tel = "13456879562"
                };
                User[] users =
                {
                    user,
                    new User
                    {
                        Name = "李四",
                        UserName = "lisi",
                        Password = "@22ewfd(feef",
                        Salt = "42syt==",
                        Tel = "13456879562"
                    }
                };
                await _context.Users.AddRangeAsync(users);
                await _context.SaveChangesAsync();
                #endregion
                #region 用户
                UserExam[] userExams =
                {
                    new UserExam
                    {
                        UserId=user.Id,
                        ExamPapgerId=examPaper.Id,
                        BeginTime=DateTime .Parse("2021-12-01"),
                        EndTime=DateTime .Parse("2022-12-01"),
                    }
                };
                await _context.UserExams.AddRangeAsync(userExams);
                await _context.SaveChangesAsync();
                #endregion
                #region 试卷试题
                examPaper.Questions.Add(question1);
                examPaper.Questions.Add(question2);
                examPaper.Questions.Add(question3);
                examPaper.Questions.Add(question4);
                examPaper.Questions.Add(question5);
                examPaper.Questions.Add(question6);
                examPaper.Questions.Add(question7);
                examPaper.Questions.Add(question8);
                examPaper.Questions.Add(question9);
                examPaper.Questions.Add(question10);

                foreach (var userExam in userExams)
                {
                    examPaper.UserExams.Add(userExam);
                }
                await _context.SaveChangesAsync();
                #endregion

                return true;
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return false;
            }
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
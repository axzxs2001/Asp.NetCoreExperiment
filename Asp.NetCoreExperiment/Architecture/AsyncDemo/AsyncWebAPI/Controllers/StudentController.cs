using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AsyncWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }
        [HttpDelete("/deleteall")]
        public bool DeleteAll()
        {
            _logger.LogInformation("删除全部");
            using var con = new SqlConnection("server=.;database=TestManageDB;uid=sa;pwd=sa;");
            var sql = @"delete from  [dbo].[Students]";
            var cmd = new SqlCommand(sql, con);
            con.Open();
            var result = cmd.ExecuteNonQuery();
            con.Close();
            return true;
        }

        [HttpPost("/addstudent")]
        public Student AddEntity([FromBody] Student student)
        {
            _logger.LogInformation("同步添加");
            return SavaEntity(student);
        }
        Student SavaEntity(Student student)
        {
            student.StuNo = Guid.NewGuid().ToString();
            using var con = new SqlConnection("server=.;database=TestManageDB;uid=sa;pwd=sa;");
            var sql = @"INSERT INTO [dbo].[Students]
           ([StuNo]
           ,[Name]
           ,[CardID]
           ,[Sex]
           ,[Birthday]
           ,[ClassID]
           )
     VALUES
           (@StuNo
           ,@Name
           ,@CardID
           ,@Sex
           ,@Birthday
           ,@ClassID
           )";
            var cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@StuNo", Value = student.StuNo, SqlDbType = System.Data.SqlDbType.VarChar });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@Name", Value = student.Name, SqlDbType = System.Data.SqlDbType.VarChar });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@CardID", Value = student.CardID, SqlDbType = System.Data.SqlDbType.VarChar });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@Sex", Value = student.Sex, SqlDbType = System.Data.SqlDbType.VarChar });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@Birthday", Value = student.Birthday, SqlDbType = System.Data.SqlDbType.DateTime });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@ClassID", Value = student.ClassID, SqlDbType = System.Data.SqlDbType.Int });
            con.Open();
            var result = cmd.ExecuteNonQuery();
            con.Close();
            return student;
        }

        [HttpPost("/addstudentasync")]
        public async Task<Student> AddEntityAsync([FromBody] Student student)
        {
            _logger.LogInformation("异步添加");
            return await SavaEntityAsync(student);
        }

        async Task<Student> SavaEntityAsync(Student student)
        {
            student.StuNo = Guid.NewGuid().ToString();
            using var con = new SqlConnection("server=.;database=TestManageDB;uid=sa;pwd=sa;");
            var sql = @"INSERT INTO [dbo].[Students]
           ([StuNo]
           ,[Name]
           ,[CardID]
           ,[Sex]
           ,[Birthday]
           ,[ClassID]
           )
     VALUES
           (@StuNo
           ,@Name
           ,@CardID
           ,@Sex
           ,@Birthday
           ,@ClassID
           )";
            var cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@StuNo", Value = student.StuNo, SqlDbType = System.Data.SqlDbType.VarChar });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@Name", Value = student.Name, SqlDbType = System.Data.SqlDbType.VarChar });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@CardID", Value = student.CardID, SqlDbType = System.Data.SqlDbType.VarChar });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@Sex", Value = student.Sex, SqlDbType = System.Data.SqlDbType.VarChar });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@Birthday", Value = student.Birthday, SqlDbType = System.Data.SqlDbType.DateTime });
            cmd.Parameters.Add(new SqlParameter { ParameterName = "@ClassID", Value = student.ClassID, SqlDbType = System.Data.SqlDbType.Int });
            await con.OpenAsync();
            var result = await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
            return student;
        }
    }

    public class Student
    {
        public string StuNo { get; set; }
        public string Name { get; set; }
        public string CardID { get; set; }
        public string Sex { get; set; }
        public DateTime Birthday { get; set; }
        public int ClassID { get; set; }

    }
}

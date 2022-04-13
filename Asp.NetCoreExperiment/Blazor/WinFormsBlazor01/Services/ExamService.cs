using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsBlazor01.Models;

namespace WinFormsBlazor01.Services
{
    public class ExamService : IExamService
    {
        public async Task<IEnumerable<Question>> GetQuestions(string? fileName)
        {
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ExamConnectionString"].ConnectionString))
            {
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    return await con.QueryAsync<Question>("select id,question as questionname,score,questiontypeid,subjecttypeid from questions");
                }
                else
                {
                    return await con.QueryAsync<Question>("select id,question as questionname,score,questiontypeid,subjecttypeid from questions where question like @FindName ", new { FindName = "%" + fileName + "%" });
                }
            }
        }
    }
}

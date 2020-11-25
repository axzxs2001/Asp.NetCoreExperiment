using HotChocolate;
using HotChocolate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDemo03_Grades
{
    /// <summary>
    /// 查询类
    /// </summary>
    public class Query
    {
        public IEnumerable<Grade> GetGrades([Service] IGradeRepository gradeRepository, string stuNo)
        {
            return gradeRepository.GetGrades(stuNo);
        }
    }


}

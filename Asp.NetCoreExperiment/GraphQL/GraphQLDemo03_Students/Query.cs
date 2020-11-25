using HotChocolate;
using HotChocolate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDemo03_Students
{
    /// <summary>
    /// 查询类
    /// </summary>
    public class Query
    {
    
        public IEnumerable<Student> GetStudents([Service] IStudentRepository studentRepository,string stuNo)
        {
            return studentRepository.GetStudents(stuNo);
        }
    }



}

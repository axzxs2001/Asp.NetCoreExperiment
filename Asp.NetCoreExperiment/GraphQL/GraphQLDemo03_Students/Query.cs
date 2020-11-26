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
  

        public IEnumerable<Student> GetStudents([Service] IStudentRepository studentRepository)
        {
            return studentRepository.GetStudents();
        }

        public Student GetStudent(string stuNo, [Service] IStudentRepository studentRepository)
        {
            return studentRepository.GetStudent(stuNo);
        }
    }



}

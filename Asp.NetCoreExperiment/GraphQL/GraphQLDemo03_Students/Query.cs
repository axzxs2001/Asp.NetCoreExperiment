using HotChocolate;
using System.Collections.Generic;


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
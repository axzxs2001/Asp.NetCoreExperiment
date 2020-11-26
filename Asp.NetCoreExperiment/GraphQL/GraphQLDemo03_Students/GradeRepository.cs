using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDemo03_Students
{

    public interface IStudentRepository
    {
        IEnumerable<Student> GetStudents();
        Student GetStudent(string stuNo);
    }
    public class StudentRepository : IStudentRepository
    {
        public IEnumerable<Student> GetStudents()
        {
            var students = new List<Student>() {
                new Student("S0001","小张",20,true),
                new Student("S0002","小李",19,false),
            };
            return students;
        }
        public Student GetStudent(string stuNo)
        {
            var students = new List<Student>() {
                new Student("S0001","小张",20,true),
                new Student("S0002","小李",19,false),
            };
            return students.SingleOrDefault(s => s.StuNo == stuNo);
        }
    }

    public record Student(string StuNo, string Name, int Age, bool Sex);
}

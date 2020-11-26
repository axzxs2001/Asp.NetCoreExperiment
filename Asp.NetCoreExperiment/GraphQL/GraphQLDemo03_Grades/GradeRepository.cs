using System.Collections.Generic;
using System.Linq;

namespace GraphQLDemo03_Grades
{
    public interface IGradeRepository
    {
        IEnumerable<Grade> GetGrades(string stuNo);
        Grade GetGrade(int id);
    }

    public class GradeRepository : IGradeRepository
    {

        public IEnumerable<Grade> GetGrades(string stuNo)
        {
            var grades = new List<Grade>(){
                new Grade(1,"S0001",100,"语文"),
                new Grade(2,"S0001",99,"数学"),
                new Grade(3,"S0002",98,"语文"),
                new Grade(4,"S0002",97,"数学")
            };
            return grades.Where(s => s.stuNo == stuNo);
        }

        public Grade GetGrade(int id)
        {
            var grades = new List<Grade>(){
                new Grade(1,"S0001",100,"语文"),
                new Grade(2,"S0001",99,"数学"),
                new Grade(3,"S0002",98,"语文"),
                new Grade(4,"S0002",97,"数学")
            };
            return grades.SingleOrDefault(s => s.ID == id);
        }
    }
    public record Grade(int ID, string stuNo, float score, string subject);
}

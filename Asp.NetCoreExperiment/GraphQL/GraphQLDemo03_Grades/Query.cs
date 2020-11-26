using HotChocolate;
using System.Collections.Generic;

namespace GraphQLDemo03_Grades
{  
    public class Query
    {
        public IEnumerable<Grade> GetGrades([Service] IGradeRepository gradeRepository, string stuNo)
        {
            return gradeRepository.GetGrades(stuNo);
        }

        public Grade GetGrade([Service] IGradeRepository gradeRepository, int id)
        {
            return gradeRepository.GetGrade(id);
        }
    }
}
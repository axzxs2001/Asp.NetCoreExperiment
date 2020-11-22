using HotChocolate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDemo00
{
    public class Query
    {
        [UseFiltering]
        [UseSorting]
        [UseProjection]
        public List<Student> GetStudents()
        {
            return new List<Student>
            {
                new Student { StuNo="N0001", Name="张三", Age=21, Sex=true },
                new Student { StuNo="N0002", Name="李四", Age=22, Sex=false  },
                new Student { StuNo="N0003", Name="王五", Age=23, Sex=true }
            };
        }
    }

    public class Student
    {
        public string StuNo { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }

        public bool Sex { get; set; }
    }
}

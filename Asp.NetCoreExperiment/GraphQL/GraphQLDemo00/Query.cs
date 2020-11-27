using HotChocolate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDemo00
{
    /// <summary>
    /// 查询类
    /// </summary>
    public class Query
    {
        /// <summary>
        /// 查询学生
        /// </summary>
        /// <returns></returns>
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
    /// <summary>
    /// 学生实体
    /// </summary>
    public class Student
    {
        /// <summary>
        /// 学号
        /// </summary>
        public string StuNo { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public bool Sex { get; set; }
    }



    /// <summary>
    /// 成绩
    /// </summary>
    public class Grade
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 分数
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 科目
        /// </summary>
        public string Subject { get; set; }


    }
    /// <summary>
    /// 查询子类型
    /// </summary>
    public class SubQuery
    {
        /// <summary>
        /// 查询成绩
        /// </summary>
        /// <returns></returns>
        [UseFiltering]
        [UseSorting]
        [UseProjection]
        public List<Grade> GetGrades()
        {
            return new List<Grade>
            {
                new Grade { ID=1, Score=80, Subject="语文" },
                new Grade { ID=2, Score=90, Subject="数学"  },
                new Grade { ID=3, Score=100, Subject="英语"  }
            };
        }
    }
}

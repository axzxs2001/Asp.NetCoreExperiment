using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsBlazor01.Models
{
    public class Question
    {
        public int ID { get; set; }
        public string? QuestionName { get; set; }
        public float Score { get; set; }
        public int QuestionTypeID { get; set; }
        public int SubjectTypeID { get; set; }
    }
}

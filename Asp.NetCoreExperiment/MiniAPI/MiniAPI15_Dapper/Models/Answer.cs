using System;
using System.Collections.Generic;

namespace MiniAPI15_Dapper.Models
{
    public class AnswerModel
    {

        public int Id { get; set; }
        public string? Sequre { get; set; }
        public string? Answer { get; set; }
        public bool IsTrue { get; set; }
        public int QuestionId { get; set; }

    }
}

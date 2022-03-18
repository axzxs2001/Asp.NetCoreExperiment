using System;
using System.Collections.Generic;

public class QuestionModel
{
    public int ID { get; set; }
    public string? Question { get; set; }
    public double Score { get; set; }
    public int QuestionTypeId { get; set; }
    public int SujectTypeId { get; set; }
}


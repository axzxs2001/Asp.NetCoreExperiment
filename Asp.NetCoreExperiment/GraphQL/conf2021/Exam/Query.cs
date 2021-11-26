using Exam.Models;
namespace Exam;

public class Query
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ExamPaper> GetExamPaper([Service] ExamContext context) =>
        context.ExamPapers;
}


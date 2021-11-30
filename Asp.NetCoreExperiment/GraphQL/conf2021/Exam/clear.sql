DELETE FROM UserExamAnswers
DBCC CHECKIDENT ('Exam.dbo.UserExamAnswers',RESEED, 0)

DELETE FROM UserExams
DBCC CHECKIDENT ('Exam.dbo.UserExams',RESEED, 0)

DELETE FROM Users
DBCC CHECKIDENT ('Exam.dbo.Users',RESEED, 0)

DELETE FROM ExamPaperQuestions
DBCC CHECKIDENT ('Exam.dbo.ExamPaperQuestions',RESEED, 0)

DELETE FROM Answers
DBCC CHECKIDENT ('Exam.dbo.Answers',RESEED, 0)

DELETE FROM Questions
DBCC CHECKIDENT ('Exam.dbo.Questions',RESEED, 0)

DELETE FROM QuestionTypes
DBCC CHECKIDENT ('Exam.dbo.QuestionTypes',RESEED, 0)

DELETE FROM SubjectTypes
DBCC CHECKIDENT ('Exam.dbo.SubjectTypes',RESEED, 0)

DELETE FROM ExamPapers
DBCC CHECKIDENT ('Exam.dbo.ExamPapers',RESEED, 0)
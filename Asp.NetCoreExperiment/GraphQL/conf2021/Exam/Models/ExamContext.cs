using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Exam.Models
{
    public partial class ExamContext : DbContext
    {
        public ExamContext()
        {
        }

        public ExamContext(DbContextOptions<ExamContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; } = null!;
        public virtual DbSet<ExamPaper> ExamPapers { get; set; } = null!;
        public virtual DbSet<ExamPaperQuestion> ExamPaperQuestions { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<QuestionType> QuestionTypes { get; set; } = null!;
        public virtual DbSet<SubjectType> SubjectTypes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserExam> UserExams { get; set; } = null!;
        public virtual DbSet<UserExamAnswer> UserExamAnswers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Answer1)
                    .HasMaxLength(200)
                    .HasColumnName("Answer");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.Sequre).HasMaxLength(8);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answers_Questions");
            });

            modelBuilder.Entity<ExamPaper>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Memo).HasMaxLength(1000);

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.Property(e => e.CreateTime)
                .HasColumnName("CreateTime")
                .HasDefaultValueSql("(getdate())"); ;
            });

            modelBuilder.Entity<ExamPaperQuestion>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ExamPaperId).HasColumnName("ExamPaperID");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.HasOne(d => d.ExamPaper)
                    .WithMany(p => p.ExamPaperQuestions)
                    .HasForeignKey(d => d.ExamPaperId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamPaperQuestions_ExamPapers");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.ExamPaperQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExamPaperQuestions_Questions");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Question1)
                    .HasMaxLength(300)
                    .HasColumnName("Question");

                entity.Property(e => e.QuestionTypeId).HasColumnName("QuestionTypeID");

                entity.Property(e => e.SujectTypeId).HasColumnName("SujectTypeID");

                entity.HasOne(d => d.QuestionType)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuestionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Questions_QuestionTypes");

                entity.HasOne(d => d.SujectType)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.SujectTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Questions_SubjectTypes");
            });

            modelBuilder.Entity<QuestionType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TypeName).HasMaxLength(50);
            });

            modelBuilder.Entity<SubjectType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TypeName).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Salt).HasMaxLength(50);

                entity.Property(e => e.Tel).HasMaxLength(11);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<UserExam>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BeginTime).HasColumnType("datetime");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.ExamPapgerId).HasColumnName("ExamPapgerID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.ExamPapger)
                    .WithMany(p => p.UserExams)
                    .HasForeignKey(d => d.ExamPapgerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserExams_ExamPapers");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserExams)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserExams_Users");
            });

            modelBuilder.Entity<UserExamAnswer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AnswerId).HasColumnName("AnswerID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserExamId).HasColumnName("UserExamID");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.UserExamAnswers)
                    .HasForeignKey(d => d.AnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserExamAnswers_Answers");

                entity.HasOne(d => d.UserExam)
                    .WithMany(p => p.UserExamAnswers)
                    .HasForeignKey(d => d.UserExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserExamAnswers_UserExams");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

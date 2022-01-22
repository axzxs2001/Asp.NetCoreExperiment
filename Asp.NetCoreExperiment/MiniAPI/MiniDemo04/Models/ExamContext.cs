using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MiniDemo04.Models
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

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<ExamPaper> ExamPapers { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionType> QuestionTypes { get; set; }
        public virtual DbSet<SubjectType> SubjectTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserExam> UserExams { get; set; }
        public virtual DbSet<UserExamAnswer> UserExamAnswers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasIndex(e => e.QuestionId, "IX_Answers_QuestionID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Answer1)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("Answer");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.Sequre)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answers_Questions");
            });

            modelBuilder.Entity<ExamPaper>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Memo).HasMaxLength(1000);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasMany(d => d.Questions)
                    .WithMany(p => p.ExamPapers)
                    .UsingEntity<Dictionary<string, object>>(
                        "ExamPaperQuestion",
                        l => l.HasOne<Question>().WithMany().HasForeignKey("QuestionId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ExamPaperQuestions_Questions"),
                        r => r.HasOne<ExamPaper>().WithMany().HasForeignKey("ExamPaperId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ExamPaperQuestions_ExamPapers"),
                        j =>
                        {
                            j.HasKey("ExamPaperId", "QuestionId").HasName("PK_ExamPaperQuestions_1");

                            j.ToTable("ExamPaperQuestions");

                            j.HasIndex(new[] { "ExamPaperId" }, "IX_ExamPaperQuestions_ExamPaperID");

                            j.HasIndex(new[] { "QuestionId" }, "IX_ExamPaperQuestions_QuestionID");

                            j.IndexerProperty<int>("ExamPaperId").HasColumnName("ExamPaperID");

                            j.IndexerProperty<int>("QuestionId").HasColumnName("QuestionID");
                        });
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasIndex(e => e.QuestionTypeId, "IX_Questions_QuestionTypeID");

                entity.HasIndex(e => e.SujectTypeId, "IX_Questions_SujectTypeID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Question1)
                    .IsRequired()
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

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SubjectType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Tel)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserExam>(entity =>
            {
                entity.HasIndex(e => e.ExamPapgerId, "IX_UserExams_ExamPapgerID");

                entity.HasIndex(e => e.UserId, "IX_UserExams_UserID");

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
                entity.HasIndex(e => e.AnswerId, "IX_UserExamAnswers_AnswerID");

                entity.HasIndex(e => e.UserExamId, "IX_UserExamAnswers_UserExamID");

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

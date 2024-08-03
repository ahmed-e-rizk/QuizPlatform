using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QuizPlatform.Models;

public partial class QuizPlatformContext : DbContext
{
    public QuizPlatformContext()
    {
    }

    public QuizPlatformContext(DbContextOptions<QuizPlatformContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<ImageStorage> ImageStorages { get; set; }

    public virtual DbSet<Option> Options { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=QuizPlatform;Integrated Security=True;Connect Timeout=300;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Answer__3214EC0707C6AFCF");

            entity.ToTable("Answer");

            entity.Property(e => e.AnswerText).HasMaxLength(500);

            entity.HasOne(d => d.Option).WithOne(p => p.Answers)
                .HasForeignKey<Answer>(d => d.OptionId)
                .HasConstraintName("FK__Answer__OptionId__2D27B809");

            entity.HasOne(d => d.Question).WithOne(p => p.Answers)
                .HasForeignKey<Answer>(d => d.QuestionId)
                .HasConstraintName("FK__Answer__Question__2C3393D0");
        });

        modelBuilder.Entity<ImageStorage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ImageSto__3214EC0799F96BDD");

            entity.ToTable("ImageStorage");

            entity.Property(e => e.FileSize).HasColumnType("numeric(20, 5)");
            entity.Property(e => e.FuLlPath).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.Quiz).WithOne(p => p.ImageStorages)
                .HasForeignKey<ImageStorage>(d => d.QuizId)
                .HasConstraintName("FK__ImageStor__QuizI__300424B4");
        });

        modelBuilder.Entity<Option>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Option__3214EC0763AEA036");

            entity.ToTable("Option");

            entity.HasOne(d => d.Question).WithMany(p => p.Options)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__Option__Question__29572725");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3214EC07E4429BE8");

            entity.ToTable("Question");

            entity.HasOne(d => d.Quiz).WithMany(p => p.Questions)
                .HasForeignKey(d => d.QuizId)
                .HasConstraintName("FK__Question__QuizId__267ABA7A");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Quiz__3214EC072C4292A0");

            entity.ToTable("Quiz");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RefreshT__3214EC07580D219D");

            entity.ToTable("RefreshToken");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ExpireDate).HasColumnType("datetime");
            entity.Property(e => e.Jti).HasMaxLength(500);
            entity.Property(e => e.Token).HasMaxLength(500);

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__RefreshTo__UserI__34C8D9D1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07B77775A5");

            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Password).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    public virtual void Commit()
    {
        SaveChanges();
    }

    public virtual async Task<int> CommitAsync()
    {
        return await SaveChangesAsync();
    }

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }
}

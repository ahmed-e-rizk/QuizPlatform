﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuizPlatform.Models;

#nullable disable

namespace QuizPlatform.Migrations
{
    [DbContext(typeof(QuizPlatformContext))]
    partial class QuizPlatformContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QuizPlatform.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AnswerText")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("OptionId")
                        .HasColumnType("int");

                    b.Property<int?>("QuestionId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Answer__3214EC0707C6AFCF");

                    b.HasIndex("OptionId")
                        .IsUnique()
                        .HasFilter("[OptionId] IS NOT NULL");

                    b.HasIndex("QuestionId")
                        .IsUnique()
                        .HasFilter("[QuestionId] IS NOT NULL");

                    b.ToTable("Answer", (string)null);
                });

            modelBuilder.Entity("QuizPlatform.Models.ImageStorage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("FileSize")
                        .HasColumnType("numeric(20, 5)");

                    b.Property<string>("FuLlPath")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("QuizId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__ImageSto__3214EC0799F96BDD");

                    b.HasIndex("QuizId")
                        .IsUnique()
                        .HasFilter("[QuizId] IS NOT NULL");

                    b.ToTable("ImageStorage", (string)null);
                });

            modelBuilder.Entity("QuizPlatform.Models.Option", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsChecked")
                        .HasColumnType("bit");

                    b.Property<string>("OptionText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Option__3214EC0763AEA036");

                    b.HasIndex("QuestionId");

                    b.ToTable("Option", (string)null);
                });

            modelBuilder.Entity("QuizPlatform.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnswerType")
                        .HasColumnType("int");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("QuizId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Question__3214EC07E4429BE8");

                    b.HasIndex("QuizId");

                    b.ToTable("Question", (string)null);
                });

            modelBuilder.Entity("QuizPlatform.Models.Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ImageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id")
                        .HasName("PK__Quiz__3214EC072C4292A0");

                    b.ToTable("Quiz", (string)null);
                });

            modelBuilder.Entity("QuizPlatform.Models.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("ExpireDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Jti")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Token")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__RefreshT__3214EC07580D219D");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshToken", (string)null);
                });

            modelBuilder.Entity("QuizPlatform.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<bool?>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Mobile")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__Users__3214EC07B77775A5");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("QuizPlatform.Models.Answer", b =>
                {
                    b.HasOne("QuizPlatform.Models.Option", "Option")
                        .WithOne("Answers")
                        .HasForeignKey("QuizPlatform.Models.Answer", "OptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Answer__OptionId__2D27B809");

                    b.HasOne("QuizPlatform.Models.Question", "Question")
                        .WithOne("Answers")
                        .HasForeignKey("QuizPlatform.Models.Answer", "QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Answer__Question__2C3393D0");

                    b.Navigation("Option");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("QuizPlatform.Models.ImageStorage", b =>
                {
                    b.HasOne("QuizPlatform.Models.Quiz", "Quiz")
                        .WithOne("ImageStorages")
                        .HasForeignKey("QuizPlatform.Models.ImageStorage", "QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__ImageStor__QuizI__300424B4");

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("QuizPlatform.Models.Option", b =>
                {
                    b.HasOne("QuizPlatform.Models.Question", "Question")
                        .WithMany("Options")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Option__Question__29572725");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("QuizPlatform.Models.Question", b =>
                {
                    b.HasOne("QuizPlatform.Models.Quiz", "Quiz")
                        .WithMany("Questions")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Question__QuizId__267ABA7A");

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("QuizPlatform.Models.RefreshToken", b =>
                {
                    b.HasOne("QuizPlatform.Models.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__RefreshTo__UserI__34C8D9D1");

                    b.Navigation("User");
                });

            modelBuilder.Entity("QuizPlatform.Models.Option", b =>
                {
                    b.Navigation("Answers")
                        .IsRequired();
                });

            modelBuilder.Entity("QuizPlatform.Models.Question", b =>
                {
                    b.Navigation("Answers")
                        .IsRequired();

                    b.Navigation("Options");
                });

            modelBuilder.Entity("QuizPlatform.Models.Quiz", b =>
                {
                    b.Navigation("ImageStorages")
                        .IsRequired();

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("QuizPlatform.Models.User", b =>
                {
                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}

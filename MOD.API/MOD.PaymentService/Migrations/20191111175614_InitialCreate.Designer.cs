﻿// <auto-generated />
using System;
using MOD.PaymentService.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MOD.PaymentService.Migrations
{
    [DbContext(typeof(PaymentContext))]
    [Migration("20191111175614_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MOD.PaymentService.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Amount");

                    b.Property<int>("MentorId");

                    b.Property<string>("MentorName")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Remarks")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("SkillName")
                        .HasColumnType("varchar(50)");

                    b.Property<float>("TotalAmountToMentor");

                    b.Property<int>("TrainingId");

                    b.Property<string>("TxtType")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("MentorId");

                    b.HasIndex("TrainingId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("MOD.PaymentService.Models.Technology", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Prerequisites")
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Toc")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("MOD.PaymentService.Models.Training", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("AmountReceived");

                    b.Property<float>("AverageRating");

                    b.Property<float>("CommisionAmount");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("EndTime")
                        .HasColumnType("varchar(60)");

                    b.Property<float>("Fees");

                    b.Property<int>("MentorId");

                    b.Property<string>("MentorName")
                        .HasColumnType("varchar(60)");

                    b.Property<int>("Progress");

                    b.Property<int>("Rating");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("StartTime")
                        .HasColumnType("varchar(60)");

                    b.Property<string>("Status")
                        .HasColumnType("varchar(60)");

                    b.Property<int>("TechnologyId");

                    b.Property<string>("TechnologyName")
                        .HasColumnType("varchar(60)");

                    b.Property<int?>("UserId");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("TechnologyId")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Training");
                });

            modelBuilder.Entity("MOD.PaymentService.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<bool>("ConfirmedSignUp");

                    b.Property<long>("ContactNumber");

                    b.Property<string>("FirstName")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LinkedInUrl")
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(180)");

                    b.Property<string>("RegistrationCode")
                        .HasColumnType("varchar(180)");

                    b.Property<bool>("ResetPassword");

                    b.Property<DateTime>("ResetPasswordDate");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("varchar(1)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("YearsOfExperience");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("MOD.PaymentService.Models.Payment", b =>
                {
                    b.HasOne("MOD.PaymentService.Models.User", "Mentor")
                        .WithMany()
                        .HasForeignKey("MentorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MOD.PaymentService.Models.Training", "Training")
                        .WithMany()
                        .HasForeignKey("TrainingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MOD.PaymentService.Models.Training", b =>
                {
                    b.HasOne("MOD.PaymentService.Models.Technology", "Technology")
                        .WithOne("Training")
                        .HasForeignKey("MOD.PaymentService.Models.Training", "TechnologyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MOD.PaymentService.Models.User")
                        .WithOne("Training")
                        .HasForeignKey("MOD.PaymentService.Models.Training", "UserId");
                });
#pragma warning restore 612, 618
        }
    }
}

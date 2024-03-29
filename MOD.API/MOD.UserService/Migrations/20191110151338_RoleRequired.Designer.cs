﻿// <auto-generated />
using System;
using MOD.UserService.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MOD.UserService.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20191110151338_RoleRequired")]
    partial class RoleRequired
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MOD.UserService.Models.User", b =>
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

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}

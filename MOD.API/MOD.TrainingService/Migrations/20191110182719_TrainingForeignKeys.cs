using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOD.TrainingService.Migrations
{
    public partial class TrainingForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Technologies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(30)", nullable: false),
                    Toc = table.Column<string>(type: "varchar(50)", nullable: true),
                    Prerequisites = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technologies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "varchar(50)", nullable: false),
                    Password = table.Column<string>(type: "varchar(180)", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", nullable: true),
                    LastName = table.Column<string>(type: "varchar(50)", nullable: true),
                    ContactNumber = table.Column<long>(nullable: false),
                    RegistrationCode = table.Column<string>(type: "varchar(180)", nullable: true),
                    Role = table.Column<string>(type: "varchar(1)", nullable: false),
                    LinkedInUrl = table.Column<string>(type: "varchar(150)", nullable: true),
                    YearsOfExperience = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ConfirmedSignUp = table.Column<bool>(nullable: false),
                    ResetPasswordDate = table.Column<DateTime>(nullable: false),
                    ResetPassword = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Training",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<string>(type: "varchar(60)", nullable: true),
                    Progress = table.Column<int>(nullable: false),
                    CommisionAmount = table.Column<float>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    AverageRating = table.Column<float>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<string>(type: "varchar(60)", nullable: true),
                    EndTime = table.Column<string>(type: "varchar(60)", nullable: true),
                    AmountReceived = table.Column<float>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(type: "varchar(60)", nullable: true),
                    MentorId = table.Column<int>(nullable: false),
                    MentorName = table.Column<string>(type: "varchar(60)", nullable: true),
                    TechnologyId = table.Column<int>(nullable: false),
                    TechnologyName = table.Column<string>(type: "varchar(60)", nullable: true),
                    Fees = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Training_Technologies_TechnologyId",
                        column: x => x.TechnologyId,
                        principalTable: "Technologies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Training_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Training_TechnologyId",
                table: "Training",
                column: "TechnologyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Training_UserId",
                table: "Training",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Training");

            migrationBuilder.DropTable(
                name: "Technologies");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

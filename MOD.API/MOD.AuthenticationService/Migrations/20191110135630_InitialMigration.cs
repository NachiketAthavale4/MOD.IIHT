using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOD.AuthenticationService.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Role = table.Column<string>(type: "varchar(1)", nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

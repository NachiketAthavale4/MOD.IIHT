using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MOD.TechnologyService.Migrations
{
    public partial class ForeignKeyTraining : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_Training_TechnologyId",
                table: "Training",
                column: "TechnologyId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Training");
        }
    }
}

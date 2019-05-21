using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LokiLoggerReporter.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Time = table.Column<DateTime>(nullable: false),
                    LogLevel = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Class = table.Column<string>(nullable: true),
                    Method = table.Column<string>(nullable: true),
                    Line = table.Column<int>(nullable: false),
                    LogTyp = table.Column<int>(nullable: false),
                    Exception = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}

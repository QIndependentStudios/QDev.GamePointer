using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QDev.GamePointer.Persist.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WatchedExecutions",
                columns: table => new
                {
                    WatchedExecutionId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExecutionType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchedExecutions", x => x.WatchedExecutionId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchedExecutions");
        }
    }
}

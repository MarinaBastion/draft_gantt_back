using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gantt_backend.Migrations
{
    public partial class BasePlane : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BaseEnd",
                table: "Tasks",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BaseStart",
                table: "Tasks",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseEnd",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "BaseStart",
                table: "Tasks");
        }
    }
}

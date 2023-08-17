using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gantt_backend.Migrations
{
    public partial class addValueIdToTaskValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ValueId",
                table: "TaskValues",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskValues_ValueId",
                table: "TaskValues",
                column: "ValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskValues_Values_ValueId",
                table: "TaskValues",
                column: "ValueId",
                principalTable: "Values",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskValues_Values_ValueId",
                table: "TaskValues");

            migrationBuilder.DropIndex(
                name: "IX_TaskValues_ValueId",
                table: "TaskValues");

            migrationBuilder.DropColumn(
                name: "ValueId",
                table: "TaskValues");
        }
    }
}

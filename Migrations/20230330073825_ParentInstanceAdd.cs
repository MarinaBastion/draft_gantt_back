using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gantt_backend.Migrations
{
    public partial class ParentInstanceAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name1",
                table: "Fields");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentInstanceId",
                table: "Instances",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instances_ParentInstanceId",
                table: "Instances",
                column: "ParentInstanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instances_Instances_ParentInstanceId",
                table: "Instances",
                column: "ParentInstanceId",
                principalTable: "Instances",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instances_Instances_ParentInstanceId",
                table: "Instances");

            migrationBuilder.DropIndex(
                name: "IX_Instances_ParentInstanceId",
                table: "Instances");

            migrationBuilder.DropColumn(
                name: "ParentInstanceId",
                table: "Instances");

            migrationBuilder.AddColumn<string>(
                name: "Name1",
                table: "Fields",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

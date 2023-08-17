using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gantt_backend.Migrations
{
    public partial class TaskaluesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Values_Instances_InstanceId",
                table: "Values");

            migrationBuilder.AlterColumn<Guid>(
                name: "InstanceId",
                table: "Values",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "ValueInstanceId",
                table: "Values",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectTypeId",
                table: "Tasks",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProjectTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Decsription = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FieldId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskId = table.Column<Guid>(type: "uuid", nullable: true),
                    InstanceId = table.Column<Guid>(type: "uuid", nullable: true),
                    TextData = table.Column<string>(type: "varchar(1024)", nullable: true),
                    NumericData = table.Column<decimal>(type: "numeric(30,6)", nullable: true),
                    BoolData = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskValues_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskValues_Instances_InstanceId",
                        column: x => x.InstanceId,
                        principalTable: "Instances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskValues_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectTypeFields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    FieldId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTypeFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectTypeFields_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTypeFields_ProjectTypes_ProjectTypeId",
                        column: x => x.ProjectTypeId,
                        principalTable: "ProjectTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Values_ValueInstanceId",
                table: "Values",
                column: "ValueInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectTypeId",
                table: "Tasks",
                column: "ProjectTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTypeFields_FieldId",
                table: "ProjectTypeFields",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTypeFields_ProjectTypeId",
                table: "ProjectTypeFields",
                column: "ProjectTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskValues_FieldId",
                table: "TaskValues",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskValues_InstanceId",
                table: "TaskValues",
                column: "InstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskValues_TaskId",
                table: "TaskValues",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_ProjectTypes_ProjectTypeId",
                table: "Tasks",
                column: "ProjectTypeId",
                principalTable: "ProjectTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Values_Instances_InstanceId",
                table: "Values",
                column: "InstanceId",
                principalTable: "Instances",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Values_Instances_ValueInstanceId",
                table: "Values",
                column: "ValueInstanceId",
                principalTable: "Instances",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_ProjectTypes_ProjectTypeId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Values_Instances_InstanceId",
                table: "Values");

            migrationBuilder.DropForeignKey(
                name: "FK_Values_Instances_ValueInstanceId",
                table: "Values");

            migrationBuilder.DropTable(
                name: "ProjectTypeFields");

            migrationBuilder.DropTable(
                name: "TaskValues");

            migrationBuilder.DropTable(
                name: "ProjectTypes");

            migrationBuilder.DropIndex(
                name: "IX_Values_ValueInstanceId",
                table: "Values");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ProjectTypeId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ValueInstanceId",
                table: "Values");

            migrationBuilder.DropColumn(
                name: "ProjectTypeId",
                table: "Tasks");

            migrationBuilder.AlterColumn<Guid>(
                name: "InstanceId",
                table: "Values",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Values_Instances_InstanceId",
                table: "Values",
                column: "InstanceId",
                principalTable: "Instances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

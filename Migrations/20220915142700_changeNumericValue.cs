using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gantt_backend.Migrations
{
    public partial class changeNumericValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "NumericData",
                table: "Values",
                type: "numeric(30,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,6)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "NumericData",
                table: "Values",
                type: "numeric(10,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(30,6)",
                oldNullable: true);
        }
    }
}

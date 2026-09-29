using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectPractice.Migrations
{
    /// <inheritdoc />
    public partial class AddDisciplineDirection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "c_is_humanitarian",
                table: "cd_discipline",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Флаг гуманитарного направления дисциплины");

            migrationBuilder.AddColumn<bool>(
                name: "c_is_technical",
                table: "cd_discipline",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Флаг технического направления дисциплины");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "c_is_humanitarian",
                table: "cd_discipline");

            migrationBuilder.DropColumn(
                name: "c_is_technical",
                table: "cd_discipline");
        }
    }
}

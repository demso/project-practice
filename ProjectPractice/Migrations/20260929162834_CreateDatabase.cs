using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjectPractice.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cd_discipline",
                columns: table => new
                {
                    discipline_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи дисциплины")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_discipline_name = table.Column<string>(type: "varchar", maxLength: 200, nullable: false, comment: "Название дисциплины"),
                    c_is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false, comment: "Флаг мягкого удаления записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_discipline_discipline_id", x => x.discipline_id);
                });

            migrationBuilder.CreateTable(
                name: "cd_specialty",
                columns: table => new
                {
                    specialty_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи специальности")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_specialty_title = table.Column<string>(type: "varchar", maxLength: 200, nullable: false, comment: "Название специальности"),
                    c_specialty_code = table.Column<string>(type: "varchar", maxLength: 20, nullable: false, comment: "Код специальности"),
                    c_is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false, comment: "Флаг мягкого удаления записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_specialty_specialty_id", x => x.specialty_id);
                });

            migrationBuilder.CreateTable(
                name: "cd_group",
                columns: table => new
                {
                    group_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи группы")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_group_name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Название группы"),
                    c_group_course = table.Column<int>(type: "int", nullable: false, comment: "Курс (год обучения) группы"),
                    c_is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false, comment: "Флаг мягкого удаления записи"),
                    SpecialtyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_group_group_id", x => x.group_id);
                    table.ForeignKey(
                        name: "fk_cd_group_f_specialty_id",
                        column: x => x.SpecialtyId,
                        principalTable: "cd_specialty",
                        principalColumn: "specialty_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cd_student",
                columns: table => new
                {
                    student_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи студента")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_student_firstname = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Имя студента"),
                    c_student_lastname = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Фамилия студента"),
                    c_is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false, comment: "Флаг мягкого удаления записи"),
                    GroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_student_student_id", x => x.student_id);
                    table.ForeignKey(
                        name: "fk_cd_student_f_group_id",
                        column: x => x.GroupId,
                        principalTable: "cd_group",
                        principalColumn: "group_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cd_grade",
                columns: table => new
                {
                    grade_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи оценки")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_grade_value = table.Column<int>(type: "int", nullable: false, comment: "Значение: -1 — незачёт, 0 — нет оценки, 1 — зачёт, 2–5 — оценка"),
                    StudentId = table.Column<int>(type: "integer", nullable: false),
                    DisciplineId = table.Column<int>(type: "integer", nullable: false),
                    c_is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false, comment: "Флаг мягкого удаления записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_grade_grade_id", x => x.grade_id);
                    table.CheckConstraint("ck_cd_grade_c_grade_value", "c_grade_value IN (-1, 0, 1, 2, 3, 4, 5)");
                    table.ForeignKey(
                        name: "fk_cd_grade_f_discipline_id",
                        column: x => x.DisciplineId,
                        principalTable: "cd_discipline",
                        principalColumn: "discipline_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cd_grade_f_student_id",
                        column: x => x.StudentId,
                        principalTable: "cd_student",
                        principalColumn: "student_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_cd_discipline_c_discipline_name",
                table: "cd_discipline",
                column: "c_discipline_name");

            migrationBuilder.CreateIndex(
                name: "idx_cd_grade_f_discipline_id",
                table: "cd_grade",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "idx_cd_grade_f_student_id",
                table: "cd_grade",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "idx_cd_group_c_group_course",
                table: "cd_group",
                column: "c_group_course");

            migrationBuilder.CreateIndex(
                name: "idx_cd_group_c_group_name",
                table: "cd_group",
                column: "c_group_name");

            migrationBuilder.CreateIndex(
                name: "IX_cd_group_SpecialtyId",
                table: "cd_group",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "idx_cd_specialty_c_specialty_code",
                table: "cd_specialty",
                column: "c_specialty_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_cd_student_c_student_lastname",
                table: "cd_student",
                column: "c_student_lastname");

            migrationBuilder.CreateIndex(
                name: "idx_cd_student_f_group_id",
                table: "cd_student",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cd_grade");

            migrationBuilder.DropTable(
                name: "cd_discipline");

            migrationBuilder.DropTable(
                name: "cd_student");

            migrationBuilder.DropTable(
                name: "cd_group");

            migrationBuilder.DropTable(
                name: "cd_specialty");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectPractice.Database.Helpers;
using ProjectPractice.Models;

namespace ProjectPractice.Database.Configurations;

public class GradeConfiguration : IEntityTypeConfiguration<Grade>
{
    //Название таблицы, которое будет отображаться в БД
    private const string TableName = "cd_grade";

    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        //Задаем первичный ключ
        builder
            .HasKey(p => p.Id)
            .HasName($"pk_{TableName}_grade_id");

        //Для целочисленного первичного ключа задаем автогенерацию (к каждой новой записи будет добавлять +1)
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Id)
            .HasColumnName("grade_id")
            .HasComment("Идентификатор записи оценки");

        //Зачёты эмулируются через оценки: -1 — незачёт, 0 — нет оценки, 1 — зачёт, 2–5 — оценка
        builder.Property(p => p.Value)
            .IsRequired()
            .HasColumnName("c_grade_value")
            .HasColumnType(ColumnType.Int)
            .HasComment("Значение: -1 — незачёт, 0 — нет оценки, 1 — зачёт, 2–5 — оценка");

        builder.Property(p => p.IsDeleted)
            .IsRequired()
            .HasColumnName("c_is_deleted")
            .HasDefaultValue(false)
            .HasComment("Флаг мягкого удаления записи");

        //Внешний ключ на студента: у студента много оценок
        builder.ToTable(TableName)
            .HasOne(p => p.Student)
            .WithMany(p => p.Grades)
            .HasForeignKey(p => p.StudentId)
            .HasConstraintName($"fk_{TableName}_f_student_id")
            .OnDelete(DeleteBehavior.Cascade);

        //Внешний ключ на дисциплину: по дисциплине много оценок
        builder.ToTable(TableName)
            .HasOne(p => p.Discipline)
            .WithMany(p => p.Grades)
            .HasForeignKey(p => p.DisciplineId)
            .HasConstraintName($"fk_{TableName}_f_discipline_id")
            .OnDelete(DeleteBehavior.Cascade);

        //Чек-констрейнт замкнутого множества допустимых значений оценки
        builder.ToTable(TableName, t =>
            t.HasCheckConstraint($"ck_{TableName}_c_grade_value",
                "c_grade_value IN (-1, 0, 1, 2, 3, 4, 5)"));

        builder.ToTable(TableName)
            .HasIndex(p => p.StudentId, $"idx_{TableName}_f_student_id");

        builder.ToTable(TableName)
            .HasIndex(p => p.DisciplineId, $"idx_{TableName}_f_discipline_id");
    }
}

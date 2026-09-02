using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectPractice.Database.Helpers;
using ProjectPractice.Models;

namespace ProjectPractice.Database.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    //Название таблицы, которое будет отображаться в БД
    private const string TableName = "cd_student";

    public void Configure(EntityTypeBuilder<Student> builder)
    {
        //Задаем первичный ключ
        builder
            .HasKey(p => p.StudentId)
            .HasName($"pk_{TableName}_student_id");

        //Для целочисленного первичного ключа задаем автогенерацию (к каждой новой записи будет добавлять +1)
        builder.Property(p => p.StudentId)
            .ValueGeneratedOnAdd();

        //Расписываем как будут называться колонки в БД, а так же их обязательность и тд
        builder.Property(p => p.StudentId)
            .HasColumnName("student_id")
            .HasComment("Идентификатор записи студента");

        //HasComment добавит комментарий, который будет отображаться в СУБД (добавлять по желанию)
        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasColumnName("c_student_firstname")
            .HasColumnType(ColumnType.String).HasMaxLength(100)
            .HasComment("Имя студента");

        builder.Property(p => p.LastName)
            .IsRequired()
            .HasColumnName("c_student_lastname");

        builder.ToTable(TableName)
            .HasOne(p => p.Group)
            .WithMany()
            .HasForeignKey(p => p.GroupId)
            .HasConstraintName("fk_f_group_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(TableName)
            .HasIndex(p => p.GroupId, $"idx_{TableName}_fk_f_group_id");

        builder.Navigation(p => p.Group)
            .AutoInclude();
    }
}
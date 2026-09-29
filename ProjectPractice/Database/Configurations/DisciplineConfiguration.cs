using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectPractice.Database.Helpers;
using ProjectPractice.Models;

namespace ProjectPractice.Database.Configurations;

public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
{
    // название таблицы в бд
    private const string TableName = "cd_discipline";

    public void Configure(EntityTypeBuilder<Discipline> builder)
    {
        // первичный ключ
        builder
            .HasKey(p => p.Id)
            .HasName($"pk_{TableName}_discipline_id");

        // автоинкремент для ключа
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Id)
            .HasColumnName("discipline_id")
            .HasComment("Идентификатор записи дисциплины");

        builder.Property(p => p.Name)
            .IsRequired()
            .HasColumnName("c_discipline_name")
            .HasColumnType(ColumnType.String).HasMaxLength(200)
            .HasComment("Название дисциплины");

        builder.Property(p => p.IsHumanitarian)
            .IsRequired()
            .HasColumnName("c_is_humanitarian")
            .HasDefaultValue(false)
            .HasComment("Флаг гуманитарного направления дисциплины");

        builder.Property(p => p.IsTechnical)
            .IsRequired()
            .HasColumnName("c_is_technical")
            .HasDefaultValue(false)
            .HasComment("Флаг технического направления дисциплины");

        builder.Property(p => p.IsDeleted)
            .IsRequired()
            .HasColumnName("c_is_deleted")
            .HasDefaultValue(false)
            .HasComment("Флаг мягкого удаления записи");

        builder.ToTable(TableName)
            .HasIndex(p => p.Name, $"idx_{TableName}_c_discipline_name");
    }
}

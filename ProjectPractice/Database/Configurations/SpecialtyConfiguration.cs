using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectPractice.Database.Helpers;
using ProjectPractice.Models;

namespace ProjectPractice.Database.Configurations;

public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
{
    //Название таблицы, которое будет отображаться в БД
    private const string TableName = "cd_specialty";

    public void Configure(EntityTypeBuilder<Specialty> builder)
    {
        //Задаем первичный ключ
        builder
            .HasKey(p => p.Id)
            .HasName($"pk_{TableName}_specialty_id");

        //Для целочисленного первичного ключа задаем автогенерацию (к каждой новой записи будет добавлять +1)
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Id)
            .HasColumnName("specialty_id")
            .HasComment("Идентификатор записи специальности");

        builder.Property(p => p.Title)
            .IsRequired()
            .HasColumnName("c_specialty_title")
            .HasColumnType(ColumnType.String).HasMaxLength(200)
            .HasComment("Название специальности");

        builder.Property(p => p.Code)
            .IsRequired()
            .HasColumnName("c_specialty_code")
            .HasColumnType(ColumnType.String).HasMaxLength(20)
            .HasComment("Код специальности");

        builder.Property(p => p.IsDeleted)
            .IsRequired()
            .HasColumnName("c_is_deleted")
            .HasDefaultValue(false)
            .HasComment("Флаг мягкого удаления записи");

        builder.ToTable(TableName)
            .HasIndex(p => p.Code, $"idx_{TableName}_c_specialty_code")
            .IsUnique();
    }
}

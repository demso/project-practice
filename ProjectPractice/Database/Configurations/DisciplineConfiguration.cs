using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectPractice.Database.Helpers;
using ProjectPractice.Models;

namespace ProjectPractice.Database.Configurations;

public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
{
    //Название таблицы, которое будет отображаться в БД
    private const string TableName = "cd_discipline";

    public void Configure(EntityTypeBuilder<Discipline> builder)
    {
        //Задаем первичный ключ
        builder
            .HasKey(p => p.Id)
            .HasName($"pk_{TableName}_discipline_id");

        //Для целочисленного первичного ключа задаем автогенерацию (к каждой новой записи будет добавлять +1)
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

        builder.Property(p => p.IsDeleted)
            .IsRequired()
            .HasColumnName("c_is_deleted")
            .HasDefaultValue(false)
            .HasComment("Флаг мягкого удаления записи");

        builder.ToTable(TableName)
            .HasIndex(p => p.Name, $"idx_{TableName}_c_discipline_name");
    }
}

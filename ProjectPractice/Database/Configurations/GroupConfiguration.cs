using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectPractice.Database.Helpers;
using ProjectPractice.Models;

namespace ProjectPractice.Database.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    //Название таблицы, которое будет отображаться в БД
    private const string TableName = "cd_group";

    public void Configure(EntityTypeBuilder<Group> builder)
    {
        //Задаем первичный ключ
        builder
            .HasKey(p => p.GroupId)
            .HasName($"pk_{TableName}_group_id");

        //Для целочисленного первичного ключа задаем автогенерацию (к каждой новой записи будет добавлять +1)
        builder.Property(p => p.GroupId)
            .ValueGeneratedOnAdd();

        //Расписываем как будут называться колонки в БД, а так же их обязательность и тд
        builder.Property(p => p.GroupId)
            .HasColumnName("group_id")
            .HasComment("Идентификатор записи группы");

        builder.Property(p => p.GroupName)
            .IsRequired()
            .HasColumnName("c_group_name")
            .HasColumnType(ColumnType.String).HasMaxLength(100)
            .HasComment("Название группы");

        //Индекс по названию группы для ускорения поиска
        builder.ToTable(TableName)
            .HasIndex(p => p.GroupName, $"idx_{TableName}_c_group_name");
    }
}

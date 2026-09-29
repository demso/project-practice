using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectPractice.Database.Helpers;
using ProjectPractice.Models;

namespace ProjectPractice.Database.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    // название таблицы в бд
    private const string TableName = "cd_group";

    public void Configure(EntityTypeBuilder<Group> builder)
    {
        // первичный ключ
        builder
            .HasKey(p => p.Id)
            .HasName($"pk_{TableName}_group_id");

        // автоинкремент для ключа
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        // названия колонок и обязательность
        builder.Property(p => p.Id)
            .HasColumnName("group_id")
            .HasComment("Идентификатор записи группы");

        builder.Property(p => p.Name)
            .IsRequired()
            .HasColumnName("c_group_name")
            .HasColumnType(ColumnType.String).HasMaxLength(100)
            .HasComment("Название группы");

        builder.Property(p => p.Course)
            .IsRequired()
            .HasColumnName("c_group_course")
            .HasColumnType(ColumnType.Int)
            .HasComment("Курс (год обучения) группы");

        builder.Property(p => p.IsDeleted)
            .IsRequired()
            .HasColumnName("c_is_deleted")
            .HasDefaultValue(false)
            .HasComment("Флаг мягкого удаления записи");

        // внешний ключ на специальность один ко многим
        builder.ToTable(TableName)
            .HasOne(p => p.Specialty)
            .WithMany(p => p.Groups)
            .HasForeignKey(p => p.SpecialtyId)
            .HasConstraintName($"fk_{TableName}_f_specialty_id")
            .OnDelete(DeleteBehavior.Cascade);

        // индекс по названию для поиска
        builder.ToTable(TableName)
            .HasIndex(p => p.Name, $"idx_{TableName}_c_group_name");

        // индекс по курсу для фильтра
        builder.ToTable(TableName)
            .HasIndex(p => p.Course, $"idx_{TableName}_c_group_course");
    }
}

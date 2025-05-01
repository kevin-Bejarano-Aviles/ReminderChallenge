using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReminderChallenge.Repository.Entities;

namespace ReminderChallenge.Repository.EntityConfigurations;

internal class ReminderEntityTypeConfiguration : IEntityTypeConfiguration<Reminder>
{
    public void Configure(EntityTypeBuilder<Reminder> builder)
    {
        builder.ToTable("Reminder");

        ConfigurePrimaryKey(builder);
        ConfigureProperties(builder);
        ConfigureDefaultEntityValues(builder);
        ConfigureSoftDeletableEntity(builder);
    }

    private static void ConfigurePrimaryKey(EntityTypeBuilder<Reminder> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("reminder_id")
            .ValueGeneratedNever();
        
    }

    private static void ConfigureProperties(EntityTypeBuilder<Reminder> builder)
    {

        builder.Property(x => x.TypeExpiration)
            .HasColumnName("expiration_type");

        builder.Property(x => x.ExpirationDate)
            .HasColumnName("expiration_date");

        builder.Property(x => x.Description)
            .HasColumnName("description");

        builder.Property(x => x.CondominiumId)
            .HasColumnName("condominium_id");
    }

    private static void ConfigureDefaultEntityValues(EntityTypeBuilder<Reminder> builder)
    {
        builder.Property(x => x.CreateAtUtc)
            .HasColumnName("created_at")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.UpdatedAtUtc)
            .HasColumnName("updated_at")
            .IsRequired(false);
    }

    private static void ConfigureSoftDeletableEntity(EntityTypeBuilder<Reminder> builder)
    {
        builder.HasQueryFilter(x => x.DeletedAtUtc == null);

        builder.Property(x => x.DeletedAtUtc)
            .HasColumnName("deleted_at") 
            .IsRequired(false);
    }
}

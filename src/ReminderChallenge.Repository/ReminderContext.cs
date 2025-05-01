using Microsoft.EntityFrameworkCore;
using ReminderChallenge.Repository.Entities;
using ReminderChallenge.Repository.EntityConfigurations;

namespace ReminderChallenge.Repository;

public class ReminderContext : DbContext
{
    public DbSet<Reminder> Reminder {  get; set; }

    public ReminderContext(DbContextOptions<ReminderContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ReminderEntityTypeConfiguration());
    }
}

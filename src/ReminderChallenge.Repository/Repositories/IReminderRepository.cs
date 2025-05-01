using ReminderChallenge.Repository.Entities;

namespace ReminderChallenge.Repository.Repositories;

public interface IReminderRepository
{
    Task<Reminder> AddSync(Reminder reminder);
    Task<Reminder> GetByIdAsync(Guid id);
    Task<IEnumerable<Reminder>> GetAllAsync();
}

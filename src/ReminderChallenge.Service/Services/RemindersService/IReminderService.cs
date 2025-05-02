using ReminderChallenge.Service.Dtos;

namespace ReminderChallenge.Service.Services.RemindersService;

public interface IReminderService
{
    Task<ReminderDto> GetReminderById(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<ReminderDto>> GetAllReminders(CancellationToken cancellationToken);
    Task<Guid> CreateReminder(
        int typeExpiration,
        DateTime expirationDate,
        string description,
        int condominiumId,
        CancellationToken cancellationToken);
    Task UpdateReminder(
        Guid reminderId,
        int typeExpiration,
        DateTime expirationDate,
        string description,
        int condominiumId,
        CancellationToken cancellationToken);
    Task DeleteReminder(Guid reminderId, CancellationToken cancellationToken);
    Task<List<ReminderDto>> GetUpcomingReminders(int days, CancellationToken cancellationToken);
    Task<List<ReminderDto>> GetExpiredReminders(CancellationToken cancellationToken);

}

using ReminderChallenge.Service.Dtos;

namespace ReminderChallenge.Service.Services;

public interface IReminderService
{
    Task<ReminderDto> GetReminderById(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<ReminderDto>> GetAllReminders(CancellationToken cancellationToken);

    Task<ReminderDto> CreateReminder(
        string typeExpiration,
        DateTime expirationDate,
        string description,
        int condominiumId,
        CancellationToken cancellationToken);

    Task UpdateReminder(
        Guid reminderId,
        string typeExpiration,
        DateTime expirationDate,
        string description,
        int condominiumId,
        CancellationToken cancellationToken);
    
    Task DeleteReminder(Guid reminderId, CancellationToken cancellationToken);
}

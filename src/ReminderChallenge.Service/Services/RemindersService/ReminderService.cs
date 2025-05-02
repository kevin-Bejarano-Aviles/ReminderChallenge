using ReminderChallenge.Repository.Entities;
using ReminderChallenge.Repository.Repositories;
using ReminderChallenge.Service.Dtos;
using ReminderChallenge.Service.Extensions;
using ReminderChallenge.Service.Helper;

namespace ReminderChallenge.Service.Services.RemindersService;

public class ReminderService : IReminderService
{
    private readonly IReminderRepository _reminderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReminderService(IReminderRepository reminderRepository, IUnitOfWork unitOfWork)
    {
        _reminderRepository = reminderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> CreateReminder(
        int typeExpiration,
        DateTime expirationDate,
        string description,
        int condominiumId,
        CancellationToken cancellationToken)
    {

        if (!Enum.IsDefined(typeof(TypeExpirationEnum), typeExpiration))
            throw new ArgumentException("Tipo de vencimiento invalido");

        var expiration = (TypeExpirationEnum)typeExpiration;

        var newReminder = Reminder.Create(
            expiration,
            expirationDate,
            description,
            condominiumId
            );

        var reminder = await _reminderRepository.AddSync(newReminder);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return reminder.Id;

    }

    public async Task<IEnumerable<ReminderDto>> GetAllReminders(CancellationToken cancellationToken)
    {
        var reminders = await _reminderRepository.GetAllAsync();

        return reminders.Select(x => x.ToDto());
    }

    public async Task<ReminderDto> GetReminderById(Guid id, CancellationToken cancellationToken)
    {
        var reminder = await _reminderRepository.GetByIdAsync(id);

        return reminder.ToDto();
    }

    public async Task UpdateReminder(
        Guid reminderId,
        int typeExpiration,
        DateTime expirationDate,
        string description,
        int condominiumId,
        CancellationToken cancellationToken)
    {

        if (!Enum.IsDefined(typeof(TypeExpirationEnum), typeExpiration))
            throw new ArgumentException("Tipo de recordatorio invalido");

        var expiration = (TypeExpirationEnum)typeExpiration;

        var reminder = await _reminderRepository.GetByIdAsync(reminderId);

        reminder.Update(
        expiration,
        expirationDate,
        description,
        condominiumId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteReminder(Guid reminderId, CancellationToken cancellationToken)
    {
        var reminder = await _reminderRepository.GetByIdAsync(reminderId);

        reminder.SoftDelete();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<ReminderDto>> GetUpcomingReminders(int days, CancellationToken cancellationToken)
    {
        var allReminders = await _reminderRepository.GetAllAsync();
        var today = DateTime.UtcNow.Date;
        var limitDate = today.AddDays(days);

        var remindersToNotifications = allReminders.Where(x => x.ExpirationDate.Date >= today && x.ExpirationDate.Date <= limitDate).ToList();

        return remindersToNotifications.Select(x => x.ToDto()).ToList();

    }

    public async Task<List<ReminderDto>> GetExpiredReminders(CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow.Date;
        var fromDate = today.AddDays(-30);

        var allReminders = await _reminderRepository.GetAllAsync();

        var remindersExpired = allReminders
         .Where(x => x.ExpirationDate.Date < today && x.ExpirationDate.Date >= fromDate)
         .ToList();

        return remindersExpired.Select(x => x.ToDto()).ToList();
    }
}

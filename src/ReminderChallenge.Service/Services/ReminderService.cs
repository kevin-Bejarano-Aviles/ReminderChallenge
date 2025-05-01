using ReminderChallenge.Repository.Entities;
using ReminderChallenge.Repository.Repositories;
using ReminderChallenge.Service.Dtos;
using ReminderChallenge.Service.Extensions;

namespace ReminderChallenge.Service.Services;

public class ReminderService : IReminderService
{
    private readonly IReminderRepository _reminderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReminderService(IReminderRepository reminderRepository, IUnitOfWork unitOfWork)
    {
        _reminderRepository = reminderRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ReminderDto> CreateReminder(
        string typeExpiration, 
        DateTime expirationDate, 
        string description, 
        int condominiumId, 
        CancellationToken cancellationToken)
    {

        var newReminder = Reminder.Create(
            typeExpiration,
            expirationDate, 
            description, 
            condominiumId
            );

        var reminder = await _reminderRepository.AddSync(newReminder);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return reminder.ToDto();
        
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
        string typeExpiration, 
        DateTime expirationDate, 
        string description, 
        int condominiumId, 
        CancellationToken cancellationToken)
    {
        var reminder = await _reminderRepository.GetByIdAsync(reminderId);

        reminder.Update(
        typeExpiration,
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
}

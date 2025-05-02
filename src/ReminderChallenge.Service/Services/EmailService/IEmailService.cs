using ReminderChallenge.Repository.Entities;
using ReminderChallenge.Service.Dtos;

namespace ReminderChallenge.Service.Services.EmailService;

public interface IEmailService
{
    Task SendUpcomingExpirationEmailAsync(ReminderDto reminder, CancellationToken cancellationToken);
    Task SendExpiredReminderEmailAsync(ReminderDto reminder, CancellationToken cancellationToken);
}

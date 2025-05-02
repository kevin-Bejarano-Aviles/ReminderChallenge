using ReminderChallenge.Repository.Entities;
using ReminderChallenge.Service.Dtos;

namespace ReminderChallenge.Service.Services.PushNotificationService;

public interface IPushNotificationService
{
    Task SendUpcomingExpirationPushNotificationAsync(ReminderDto reminder, CancellationToken cancellationToken);
    Task SendExpiredReminderPushNotificationAsync(ReminderDto reminder, CancellationToken cancellationToken);
}

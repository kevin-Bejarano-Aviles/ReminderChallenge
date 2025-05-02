using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReminderChallenge.Service.Configuration;
using ReminderChallenge.Service.Services.EmailService;
using ReminderChallenge.Service.Services.PushNotificationService;
using ReminderChallenge.Service.Services.RemindersService;

namespace ReminderChallenge.Service.Services.NotificationService;

public class NotificationService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<NotificationService> _logger;
    private readonly ReminderNotificationSettings _notificationSettings;

    public NotificationService(
        IServiceScopeFactory serviceScopeFactory, 
        ILogger<NotificationService> logger,
        IOptions<ReminderNotificationSettings> notificationSettings)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
        _notificationSettings = notificationSettings.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("ReminderNotifierService iniciado.");

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var reminderService = scope.ServiceProvider.GetRequiredService<IReminderService>();
                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                    var pushService = scope.ServiceProvider.GetRequiredService<IPushNotificationService>();

                    var remindersUpcoming = await reminderService.GetUpcomingReminders(_notificationSettings.DaysBeforeExpiration, cancellationToken);
                    var remindersExpired = await reminderService.GetExpiredReminders(cancellationToken);

                    var tasks = new List<Task>();

                    if (remindersUpcoming.Any())
                    {
                        _logger.LogInformation($"Se encontraron {remindersUpcoming.Count} recordatorios próximos a vencer.");
                        foreach (var reminder in remindersUpcoming)
                        {
                            tasks.Add(emailService.SendUpcomingExpirationEmailAsync(reminder, cancellationToken));
                            tasks.Add(pushService.SendUpcomingExpirationPushNotificationAsync(reminder, cancellationToken));
                        }
                    }
                    else
                    {
                        _logger.LogInformation("No hay recordatorios próximos a vencer.");
                    }

                    if (remindersExpired.Any())
                    {
                        _logger.LogInformation($"Se encontraron {remindersExpired.Count} recordatorios vencidos.");
                        foreach (var reminder in remindersExpired)
                        {
                            tasks.Add(emailService.SendExpiredReminderEmailAsync(reminder, cancellationToken));
                            tasks.Add(pushService.SendExpiredReminderPushNotificationAsync(reminder, cancellationToken));
                        }
                    }
                    else
                    {
                        _logger.LogInformation("No hay recordatorios vencidos.");
                    }

                    await Task.WhenAll(tasks);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ejecutando ReminderNotifierService.");
            }

            await Task.Delay(TimeSpan.FromHours(_notificationSettings.CheckIntervalInHours), cancellationToken); 
        }

        _logger.LogInformation("ReminderNotifierService detenido.");
    }
}

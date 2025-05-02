using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReminderChallenge.Service.Configuration;
using ReminderChallenge.Service.Dtos;
using ReminderChallenge.Service.Helpers;

namespace ReminderChallenge.Service.Services.PushNotificationService;

public class PushNotificationService : IPushNotificationService
{
    private readonly ILogger<PushNotificationService> _logger;
    private readonly PushNotificationSettings _settings;

    public PushNotificationService(
        ILogger<PushNotificationService> logger,
        IOptions<PushNotificationSettings> settings)
    {
        _logger = logger;
        _settings = settings.Value;
    }
    public async Task SendUpcomingExpirationPushNotificationAsync(ReminderDto reminder, CancellationToken cancellationToken)
    {

        if (!FirebaseManager.IsInitialized)
        {
            _logger.LogWarning("Firebase no está inicializado. Notificación no enviada.");
            return;
        }

        try
        {
            var today = DateTime.UtcNow.Date;
            var cantDays = (reminder.ExpirationDate.Date - today).Days;

            var message = new Message()
            {
                Notification = new Notification()
                {
                    Title = $"Recordatorio: Servicio {reminder.TypeExpiration.ToLower()} por vencer",
                    Body = $"Le avisamos que su servicio de: '{reminder.TypeExpiration.ToLower()}' vence dentro de {cantDays} dias"
                },
                Topic = _settings.Topic
            };

            
            await FirebaseMessaging.DefaultInstance.SendAsync(message, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error");
            _logger.LogError($"Error al enviar la notificación: {ex.Message}");
        }   
    }

    public async Task SendExpiredReminderPushNotificationAsync(ReminderDto reminder, CancellationToken cancellationToken)
    {

        if (!FirebaseManager.IsInitialized)
        {
            _logger.LogWarning("Firebase no está inicializado. Notificación no enviada.");
            return;
        }

        try
        {
            var today = DateTime.UtcNow.Date;
            var cantDays = (today - reminder.ExpirationDate.Date).Days;

            var message = new Message()
            {
                Notification = new Notification()
                {
                    Title = $"Aviso: Su servicio {reminder.TypeExpiration.ToLower()} se vencio",
                    Body = $"Le avisamos que su servicio de '{reminder.TypeExpiration.ToLower()}' se vencio hace {cantDays} dias"
                },
                Topic = _settings.Topic
            };


            await FirebaseMessaging.DefaultInstance.SendAsync(message, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error");
            _logger.LogError($"Error al enviar la notificación: {ex.Message}");
        }
    }
}

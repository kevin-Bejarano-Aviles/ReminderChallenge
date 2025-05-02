using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReminderChallenge.Service.Configuration;
using ReminderChallenge.Service.Dtos;
using System.Net;
using System.Net.Mail;

namespace ReminderChallenge.Service.Services.EmailService;

public class EmailService : IEmailService
{
    private readonly MailSettings _settings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(
        IOptions<MailSettings> settings,
        ILogger<EmailService> logger)
    {
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task SendUpcomingExpirationEmailAsync(ReminderDto reminder, CancellationToken cancellationToken)
    {
        try
        {
            var today = DateTime.UtcNow.Date;
            var cantDays = (reminder.ExpirationDate.Date - today).Days;

            var mail = new MailMessage(_settings.From, _settings.To)
            {
                Subject = $"Recordatorio: Servicio {reminder.TypeExpiration.ToLower()} por vencer",
                Body = $"Le avisamos que su servicio de '{reminder.TypeExpiration.ToLower()}' vence dentro de {cantDays} dias"
            };

            using var smtpClinet = new SmtpClient(_settings.SmtpHost)
            {
                Port = _settings.Port,
                Credentials = new NetworkCredential(_settings.User, _settings.Password),
                EnableSsl = _settings.EnableSsl
            };

            await smtpClinet.SendMailAsync(mail, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error");
            _logger.LogError($"Error al enviar el email: {ex.Message}");
        }

    }

    public async Task SendExpiredReminderEmailAsync(ReminderDto reminder, CancellationToken cancellationToken)
    {
        try
        {
            var today = DateTime.UtcNow.Date;
            var cantDays = (today - reminder.ExpirationDate.Date).Days;

            var mail = new MailMessage(_settings.From, _settings.To)
            {
                Subject = $"Aviso: Su servicio {reminder.TypeExpiration.ToLower()} se vencio",
                Body = $"Le avisamos que su servicio de '{reminder.TypeExpiration.ToLower()}' se vencio hace {cantDays} dias"
            };

            using var smtpClinet = new SmtpClient(_settings.SmtpHost)
            {
                Port = _settings.Port,
                Credentials = new NetworkCredential(_settings.User, _settings.Password),
                EnableSsl = _settings.EnableSsl
            };

            await smtpClinet.SendMailAsync(mail, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error");
            _logger.LogError($"Error al enviar el email: {ex.Message}");
        }

    }
}

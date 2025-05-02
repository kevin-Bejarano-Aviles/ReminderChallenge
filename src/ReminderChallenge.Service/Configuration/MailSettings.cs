namespace ReminderChallenge.Service.Configuration;

public class MailSettings
{
    public string From { get; set; }
    public string To { get; set; }  
    public string SmtpHost { get; set; }
    public int Port { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public bool EnableSsl { get; set; }
}
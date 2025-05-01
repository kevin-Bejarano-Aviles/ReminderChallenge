namespace ReminderChallenge.Repository.Entities;

public class Reminder
{
    public Guid Id { get; set; }
    public string TypeExpiration { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string Description { get; set; }
    public int CondominiumId { get; set; }

    public DateTime CreateAtUtc { get; private set; }
    public DateTime? UpdatedAtUtc { get; private set; }
    public DateTime? DeletedAtUtc { get; private set; }

    
}

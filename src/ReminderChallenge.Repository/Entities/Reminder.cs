using ReminderChallenge.Service.Helper;

namespace ReminderChallenge.Repository.Entities;

public class Reminder
{
    public Guid Id { get; set; }
    public TypeExpirationEnum TypeExpiration { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string Description { get; set; }
    public int CondominiumId { get; set; }
    public DateTime CreateAtUtc { get; private set; }
    public DateTime? UpdatedAtUtc { get; private set; }
    public DateTime? DeletedAtUtc { get; private set; }



    private Reminder(Guid id)
    {
        Id = id;  
        CreateAtUtc = DateTime.UtcNow;
    }

    public static Reminder Create(
        TypeExpirationEnum typeExpiration,
        DateTime expirationDate,
        string description,
        int condominiumId
        )
    {
        return new Reminder(Guid.NewGuid())
        {
            TypeExpiration = typeExpiration,
            ExpirationDate = expirationDate,
            Description = description,
            CondominiumId = condominiumId
        };
    }
    
    public void Update(
        TypeExpirationEnum typeExpiration,
        DateTime expirationDate,
        string description,
        int condominiumId
        )
    {
        TypeExpiration = typeExpiration;
        ExpirationDate = expirationDate;
        Description = description;
        CondominiumId = condominiumId;

        MarkAsUpdated();
    }

    public void SoftDelete()
    {
        if (DeletedAtUtc != null)
            return;

        DeletedAtUtc = DateTime.UtcNow;
    }
    private void MarkAsUpdated() => UpdatedAtUtc = DateTime.UtcNow;
}

namespace ReminderChallenge.Service.Dtos;

public sealed record ReminderDto(
    Guid id,
    string TypeExpiration,
    DateTime ExpirationDate,
    string Description,
    int CondominiumId
    );

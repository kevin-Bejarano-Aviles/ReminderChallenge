using ReminderChallenge.Repository.Entities;
using ReminderChallenge.Service.Dtos;

namespace ReminderChallenge.Service.Extensions;

public static class ReminderExtension
{
    public static ReminderDto ToDto (this Reminder reminder)
    {
        return new ReminderDto(
            reminder.Id,
            reminder.TypeExpiration,
            reminder.ExpirationDate,
            reminder.Description,
            reminder.CondominiumId
        );
    }
}

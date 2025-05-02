using System.Text.Json.Serialization;

namespace ReminderChallenge.API.RequestModels.Reminder;

public sealed record ReminderRequest(
    [property: JsonPropertyName("tipo_vencimiento")]
    int TypeExpiration,

    [property: JsonPropertyName("fecha_vencimiento")]
    DateTime ExpirationDate,

    [property: JsonPropertyName("descripcion")]
    string Description,

    [property: JsonPropertyName("consorcio_id")]
    int CondominiumId
);



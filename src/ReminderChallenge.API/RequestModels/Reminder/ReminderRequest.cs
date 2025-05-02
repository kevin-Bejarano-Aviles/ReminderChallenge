using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReminderChallenge.API.RequestModels.Reminder;

public sealed record ReminderRequest(
    [param: Required(ErrorMessage = "El tipo de vencimiento es obligatorio.")]
    [property: JsonPropertyName("tipo_vencimiento")]
    int TypeExpiration,

    [param: Required(ErrorMessage = "La fecha de vencimiento es obligatoria.")]
    [property: JsonPropertyName("fecha_vencimiento")]
    DateTime ExpirationDate,

    [param: Required(ErrorMessage = "La descripción es obligatoria.")]
    [property: JsonPropertyName("descripcion")]
    string Description,

    [param: Required(ErrorMessage = "El ID del consorcio es obligatorio.")]
    [property: JsonPropertyName("consorcio_id")]
    int CondominiumId
);



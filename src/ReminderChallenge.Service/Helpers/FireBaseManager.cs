using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Logging;

namespace ReminderChallenge.Service.Helpers;

public static class FirebaseManager
{
    public static bool IsInitialized { get; private set; } = false;

    public static void Initialize(string credentialsPath, ILogger logger)
    {
        try
        {
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(credentialsPath)
            });

            IsInitialized = true;
            logger.LogInformation("Firebase inicializado correctamente.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "No se pudo inicializar Firebase. Verifica el archivo de credenciales.");
            IsInitialized = false;
        }
    }
}

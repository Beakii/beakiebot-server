using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace beakiebot_server.Models
{
    public class AzureKeyVaultClient
    {
        private readonly SecretClient _secretClient;

        private Uri KeyVaultUri {  get; set; }
        public string? TwitchClientId { get; set; }
        public string? TwitchClientSecret { get; set; }
        public string RedirectUrl { get; set; } = "https://localhost:7176/auth/login"; //Replace with azure keyvault value for prod

        public AzureKeyVaultClient(IConfiguration configuration)
        {
            KeyVaultUri = new(configuration["KeyVault:KeyVaultUrl"]!);
            _secretClient = new(KeyVaultUri, new DefaultAzureCredential());

            var idResponse = _secretClient.GetSecret("TwitchClientId");
            TwitchClientId = idResponse.Value.Value.ToString();

            var secretResponse = _secretClient.GetSecret("TwitchClientSecret");
            TwitchClientSecret = secretResponse.Value.Value.ToString();
        }
    }
}

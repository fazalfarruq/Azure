using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;

namespace MyAddressBookPlus
{
    public static class KeyVaultService
    {
        public static string DefaultConnection { get; set; }

        public static async Task<string> GetToken(string authority, string resource, string scope)
        {
            string token = string.Empty;

            try
            {
                var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json");
                var configuration = builder.Build();

                var authContext = new AuthenticationContext(authority);
                ClientCredential clientCred = new ClientCredential(configuration["ConnectionStrings:ClientId"],
                            configuration["ConnectionStrings:ClientSecret"]);
                AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

                if (result == null)
                    throw new InvalidOperationException("Failed to obtain the JWT token");

                token = result.AccessToken;
            }
            catch (Exception e)
            {
                throw e;
            }
            return token;
        }

        public static async Task<string> GetSecret()
        {
            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();

            try
            {
                var keyVaultClient = new KeyVaultClient(
                    new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

                var secret = await keyVaultClient.GetSecretAsync("https://darksky01.vault.azure.net/secrets/DefaultConnection")
                    .ConfigureAwait(false);

                DefaultConnection = secret.Value;

            }
            catch (Exception e)
            {
                throw e;
            }

            return DefaultConnection;
        }
    }
}
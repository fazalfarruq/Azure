using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeyVaultLib
{
    public static class KeyVaultCache
    {
        public static string BaseUri { get; set; } = @"https://darksky01.vault.azure.net/secrets/";

        private static KeyVaultClient _keyVaultClient = null;
        private static Dictionary<string, string> KeyVaultSecretsCache = new Dictionary<string, string>();

        public static KeyVaultClient KeyVaultClient
        {
            get
            {
                if (_keyVaultClient is null)
                {
                    var token = new AzureServiceTokenProvider();
                    _keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(token.KeyVaultTokenCallback));
                }
                return _keyVaultClient;
            }
        }

        public async static Task<string> GetCachedKeyVaultSecret(string secretName)
        {
            try
            {
                if (!KeyVaultSecretsCache.ContainsKey(secretName))
                {
                    var secret = await KeyVaultClient.GetSecretAsync($"{BaseUri}{secretName}").ConfigureAwait(false);
                    KeyVaultSecretsCache.Add(secretName, secret.Value);
                }
                return KeyVaultSecretsCache.ContainsKey(secretName) ? KeyVaultSecretsCache[secretName] : string.Empty;
            }
            catch (Exception e)
            {

            }
            return string.Empty;
        }
    }
}

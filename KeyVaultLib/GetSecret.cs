using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KeyVaultLib
{
    public static class GetSecret
    {
        public static async Task<string> DefaultConnectionString() {
            return await KeyVaultCache.GetCachedKeyVaultSecret("DefaultConnection");
        }
    }
}

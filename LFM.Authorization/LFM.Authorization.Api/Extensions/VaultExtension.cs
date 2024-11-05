using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;

namespace LFM.Authorization.Extensions;

public static class VaultExtension
{
    public static IServiceCollection AddLfmVault(this IServiceCollection services, IConfiguration configuration)
    {
        IAuthMethodInfo authMethod = new TokenAuthMethodInfo(vaultToken: configuration["Vault:Token"]);
        
        VaultClientSettings vaultClientSettings = new(configuration["https://vault.local:8200"], authMethod);
        IVaultClient vaultClient = new VaultClient(vaultClientSettings);

        return services;
    }
}
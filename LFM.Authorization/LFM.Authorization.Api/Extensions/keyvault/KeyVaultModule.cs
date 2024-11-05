using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LFM.Common.KeyVault;

public static class KeyVaultModule
{
    public static IConfigurationBuilder AddLfmKeyVault(this IConfigurationBuilder configurationBuilder, Action<VaultOptions> options)
    {
        var vaultOptions = new VaultConfigurationSource(options);
        return configurationBuilder.Add(vaultOptions);
    }
}
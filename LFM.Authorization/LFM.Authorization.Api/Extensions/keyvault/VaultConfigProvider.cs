using Microsoft.Extensions.Configuration;
using VaultSharp;
using VaultSharp.V1.AuthMethods.AppRole;
using VaultSharp.V1.Commons;
using VaultSharp.V1.SecretsEngines;

namespace LFM.Common.KeyVault;

public class VaultConfigurationProvider : ConfigurationProvider
{
    private readonly VaultOptions _config;
    private readonly IVaultClient _client;

    public VaultConfigurationProvider(VaultOptions config)
    {
        _config = config;
        _client = new VaultClient(
            new VaultClientSettings(_config.Address, new AppRoleAuthMethodInfo(_config.Role, _config.Secret))
        );
    }

    public override void Load()
    {
        LoadAsync().Wait();
    }

    public async Task LoadAsync()
    {
        await GetDatabaseCredentials();
    }

    public async Task GetDatabaseCredentials()
    {
        var userId = "";
        var password = "";

        Secret<UsernamePasswordCredentials> dynamicDatabaseCredentials =
            await _client.V1.Secrets.Database.GetCredentialsAsync(_config.Role);

        userId = dynamicDatabaseCredentials.Data.Username;
        password = dynamicDatabaseCredentials.Data.Password;


        Data.Add("database:userId", userId);
        Data.Add("database:password", password);
    }
}

public class VaultConfigurationSource : IConfigurationSource
{
    private readonly VaultOptions _config;

    public VaultConfigurationSource(Action<VaultOptions> config)
    {
        _config = new VaultOptions();
        config.Invoke(_config);
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new VaultConfigurationProvider(_config);
    }
}
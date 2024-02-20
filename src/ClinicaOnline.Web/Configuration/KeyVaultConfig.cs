using System;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicaOnline.Web.Configuration
{
    public static class KeyVaultConfig
    {
        public static IServiceCollection AddKeyVault(this IServiceCollection services, ConfigurationManager configuration)
        {
            var keyVaultUri = configuration.GetSection("AzureKeyVault:BaseUrl").Value;
            
            configuration.AddAzureKeyVault(new Uri(keyVaultUri), new DefaultAzureCredential());

            return services;
        }
    }
}
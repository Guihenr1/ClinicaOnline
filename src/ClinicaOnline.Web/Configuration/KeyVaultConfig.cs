using System;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicaOnline.Web.Configuration
{
    public static class KeyVaultConfig
    {
        public static IServiceCollection AddKeyVault(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var keyVaultUri = builder.Configuration.GetSection("AzureKeyVault:BaseUrl").Value;
            var tenantId = builder.Configuration.GetSection("AzureKeyVault:TenantId").Value;
            var clientId = builder.Configuration.GetSection("AzureKeyVault:ClientId").Value;
            var clientSecret = builder.Configuration.GetSection("AzureKeyVault:ClientSecret").Value;

            var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            var client = new SecretClient(new Uri(keyVaultUri), credential);
              
            builder.Configuration.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());

            return services;
        }
    }
}
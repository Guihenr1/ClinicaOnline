using System;
using ClinicaOnline.Application.Interfaces;
using ClinicaOnline.Application.Services;
using ClinicaOnline.Core.Notification;
using ClinicaOnline.Core.Repositories;
using ClinicaOnline.Core.Repositories.Base;
using ClinicaOnline.Infrastructure.Data;
using ClinicaOnline.Infrastructure.Repositories;
using ClinicaOnline.Infrastructure.Repositories.Base;
using ClinicaOnline.Web.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicaOnline.Web.Configuration
{
    public static class DependencyInjectionConfig
    {
        private class LazilyResolved<T> : Lazy<T>
        {
            public LazilyResolved(IServiceProvider serviceProvider)
                : base(serviceProvider.GetRequiredService<T>)
            {
            }
        }

        public static void ConfigureAspnetRunServices(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureDatabases(services, configuration);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(Lazy<>), typeof(LazilyResolved<>));
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IParceiroRepository, ParceiroRepository>();
            services.AddScoped<IParceiroService, ParceiroService>();
            services.AddScoped<IMedicoRepository, MedicoRepository>();
            services.AddScoped<IMedicoService, MedicoService>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IPacienteService, PacienteService>();
            services.AddScoped<NotificationContext>();

            services.AddTransient<IAuthorizationHandler, ApiKeyRequirementHandler>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        }

        private static void ConfigureDatabases(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PostgreSql")));
        }
    }
}
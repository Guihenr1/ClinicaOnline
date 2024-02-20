using System;
using System.Linq;
using System.Threading.Tasks;
using ClinicaOnline.Core.Configuration;
using ClinicaOnline.Domain.Entities;
using ClinicaOnline.Core.Utils;
using Microsoft.EntityFrameworkCore;

namespace ClinicaOnline.Infrastructure.Data
{
    public class ContextSeed
    {
        public static async Task Seed(Context context)
        {
            context.Database.Migrate();
            context.Database.EnsureCreated();

            if (!context.Usuarios.Any())
            {
                context.Usuarios.AddRange(GetPreconfiguredUsers());
                await context.SaveChangesAsync();
            }
        }

        private static Usuario GetPreconfiguredUsers()
        {
            return new Usuario()
            {
                Id = Guid.NewGuid(),
                Email = "contato@builtcode.com",
                Nome = "admin",
                Senha = Security.GenerateHash("123456", Settings.Salt),
                Eperfil = 0
            };
        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using ClinicaOnline.Core.Entities;
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
                Email = "contato@builtcode.com.br",
                Nome = "admin",
                Senha = "123456",
                Eperfil = 0
            };
        }
    }
}
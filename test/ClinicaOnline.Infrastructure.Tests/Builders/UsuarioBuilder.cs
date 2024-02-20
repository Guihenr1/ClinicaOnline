using System;
using ClinicaOnline.Domain.Entities;
using ClinicaOnline.Domain.Enums;

namespace ClinicaOnline.Infrastructure.Tests.Builders
{
    public class UsuarioBuilder
    {
        private Usuario _usuario;
        public Guid Id => Guid.NewGuid();
        public string Email => "guilherme@gmail.com";
        public string Nome => "Guilherme";
        public string Senha => "Abc123!";
        public Perfil Eperfil => Perfil.Admin;

        public UsuarioBuilder()
        {
            _usuario = WithDefaultValues();
        }

        public Usuario WithDefaultValues()
        {
            return new Usuario() {
                Id = Id,
                Email = Email,
                Nome = Nome,
                Senha = Senha,
                Eperfil = Eperfil
            };
        }
    }
}
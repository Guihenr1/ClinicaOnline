using System;
using ClinicaOnline.Domain.Entities;

namespace ClinicaOnline.Infrastructure.Tests.Builders
{
    public class ParceiroBuilder
    {
        private Parceiro _parceiro;
        public Guid Id => Guid.NewGuid();
        public string Nome => "Guilherme";
        public Guid ApiKey => Guid.NewGuid();

        public ParceiroBuilder()
        {
            _parceiro = WithDefaultValues();
        }

        public Parceiro WithDefaultValues()
        {
            return new Parceiro() {
                Id = Id,
                Nome = Nome,
                ApiKey = ApiKey
            };
        }
    }
}
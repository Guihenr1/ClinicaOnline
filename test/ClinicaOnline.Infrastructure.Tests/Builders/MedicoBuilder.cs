using System;
using ClinicaOnline.Core.Entities;

namespace ClinicaOnline.Infrastructure.Tests.Builders
{
    public class MedicoBuilder
    {
        private Medico _medico;
        public Guid Id => Guid.NewGuid();
        public string Nome => "Guilherme";
        public string Crm => "12345";
        public string UfCrm => "SP";
        public string Especialidade => "Clinico Geral";

        public MedicoBuilder()
        {
            _medico = WithDefaultValues();
        }

        public Medico WithDefaultValues()
        {
            return new Medico() {
                Id = Id,
                Nome = Nome,
                Crm = Crm,
                UfCrm = UfCrm, 
                Especialidade = Especialidade
            };
        }
    }
}
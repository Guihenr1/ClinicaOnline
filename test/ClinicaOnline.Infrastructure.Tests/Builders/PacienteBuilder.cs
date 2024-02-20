using System;
using ClinicaOnline.Domain.Entities;

namespace ClinicaOnline.Infrastructure.Tests.Builders
{
    public class PacienteBuilder
    {
        private Paciente _paciente;
        public Guid Id => Guid.NewGuid();
        public string Nome => "Guilherme";
        public string Cpf => "97048683082";
        public string Telefone => "119844443333";
        public Guid MedicoId => Guid.NewGuid();

        public PacienteBuilder()
        {
            _paciente = WithDefaultValues();
        }

        public Paciente WithDefaultValues()
        {
            return new Paciente() {
                Id = Id,
                Nome = Nome,
                Cpf = Cpf,
                Telefone = Telefone, 
                MedicoId = MedicoId
            };
        }
    }
}
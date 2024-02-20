using System;
using System.Text.Json.Serialization;
using ClinicaOnline.Domain.Entities.Base;

namespace ClinicaOnline.Domain.Entities
{
    public class Paciente : EntityBase
    {
        public string Nome { get; set; }
        
        public string Cpf { get; set; }
        
        public string Telefone { get; set; }

        public Guid MedicoId { get; set; }
        
        [JsonIgnore]
        public Medico Medico { get; set; }
    }
}
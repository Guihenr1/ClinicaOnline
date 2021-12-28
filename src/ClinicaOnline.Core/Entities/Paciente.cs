using System.Text.Json.Serialization;
using ClinicaOnline.Core.Entities.Base;

namespace ClinicaOnline.Core.Entities
{
    public class Paciente : EntityBase
    {
        public string Nome { get; set; }
        
        public string Cpf { get; set; }
        
        public string Telefone { get; set; }
        
        public Medico Medico { get; set; }
    }
}
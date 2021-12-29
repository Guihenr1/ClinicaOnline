using System.Collections.Generic;
using System.Text.Json.Serialization;
using ClinicaOnline.Core.Entities.Base;

namespace ClinicaOnline.Core.Entities
{
    public class Medico : EntityBase
    {
        public string Nome { get; set; }
        
        public string Crm { get; set; }
        
        public string UfCrm { get; set; }
        
        public string Especialidade { get; set; }

        [JsonIgnore]
        public List<Paciente> Pacientes { get; set; }
    }
}
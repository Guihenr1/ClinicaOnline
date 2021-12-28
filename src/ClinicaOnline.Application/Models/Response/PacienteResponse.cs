using ClinicaOnline.Application.Models.Response.Base;

namespace ClinicaOnline.Application.Models.Response
{
    public class PacienteResponse : Validator
    {
        public string Nome { get; set; }
        
        public string Cpf { get; set; }
        
        public string Telefone { get; set; }
        
        public MedicoResponse Medico { get; set; }
    }
}
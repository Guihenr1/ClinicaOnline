using System;
using ClinicaOnline.Application.Models.Response.Base;

namespace ClinicaOnline.Application.Models.Response
{
    public class MedicoResponse : Validator
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        
        public string Crm { get; set; }
        
        public string UfCrm { get; set; }
        
        public string Especialidade { get; set; }
    }
}
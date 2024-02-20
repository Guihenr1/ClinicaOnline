using System;
using ClinicaOnline.Domain.Entities.Base;

namespace ClinicaOnline.Domain.Entities
{
    public class Parceiro : EntityBase
    {
        public string Nome { get; set; }
        
        public Guid ApiKey { get; set; }
    }
}
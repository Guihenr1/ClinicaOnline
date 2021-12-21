using System;
using ClinicaOnline.Core.Entities.Base;

namespace ClinicaOnline.Core.Entities
{
    public class Parceiro : EntityBase
    {
        public string Nome { get; set; }
        
        public Guid ApiKey { get; set; }
    }
}
using ClinicaOnline.Core.Entities.Base;
using ClinicaOnline.Core.Enums;

namespace ClinicaOnline.Core.Entities
{
    public class Usuario : EntityBase
    {
        public string Email { get; set; }
        
        public string Nome { get; set; }
        
        public string Senha { get; set; }
        
        public Perfil Eperfil { get; set; }
    }
}
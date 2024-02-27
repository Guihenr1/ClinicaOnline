using ClinicaOnline.Domain.Entities.Base;
using ClinicaOnline.Domain.Enums;

namespace ClinicaOnline.Domain.Entities
{
    public class Usuario : EntityBase
    {
        public string Email { get; set; }

        public string Nome { get; set; }

        public string Senha { get; set; }

        public Perfil Eperfil { get; set; }
    }
}
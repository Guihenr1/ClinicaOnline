using System.ComponentModel.DataAnnotations;
using ClinicaOnline.Application.Models.Response.Base;
using ClinicaOnline.Core.Enums;

namespace ClinicaOnline.Application.Models.Request
{
    public class UserRequest
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        [EmailAddress(ErrorMessage = "Campo inválido")]
        [MaxLength(255, ErrorMessage = "Tamanho máximo de {1} caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(255, ErrorMessage = "Tamanho máximo de {1} caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(255, ErrorMessage = "Tamanho máximo de {1} caracteres")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [EnumDataType(typeof(Perfil), ErrorMessage = "Perfil inválido")]
        public Perfil Eperfil { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Foto { get; set; }
    }
}
using System;
using ClinicaOnline.Application.Models.Response.Base;
using ClinicaOnline.Domain.Enums;

namespace ClinicaOnline.Application.Models.Response
{
    public class UserResponse : Validator
    {
        public Guid Id { get; set; }
                
        public string Email { get; set; }
        
        public string Nome { get; set; }
        
        public string Senha { get; set; }
        
        public Perfil Eperfil { get; set; }
    }
}
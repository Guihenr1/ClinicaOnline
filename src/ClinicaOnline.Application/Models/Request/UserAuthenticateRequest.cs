using System.ComponentModel.DataAnnotations;

namespace ClinicaOnline.Application.Models.Request
{
    public class UserAuthenticateRequest
    {
        [Required (ErrorMessage = "Campo obrigatório")]
        public string email { get; set; }
        
        [Required (ErrorMessage = "Campo obrigatório")]
        public string password { get; set; }
        
        
    }
}
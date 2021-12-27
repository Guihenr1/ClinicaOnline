using System.ComponentModel.DataAnnotations;

namespace ClinicaOnline.Application.Models.Request
{
    public class ParceiroRequest
    {
        [Required (ErrorMessage = "Campo obrigatório")]
        [MaxLength(255, ErrorMessage = "Tamanho máximo de {1} caracteres")]
        public string Nome { get; set; }
        
        
    }
}
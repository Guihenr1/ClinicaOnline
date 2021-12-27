using System.ComponentModel.DataAnnotations;

namespace ClinicaOnline.Application.Models.Request
{
    public class MedicoRequest
    {
        [Required (ErrorMessage = "Campo obrigatório")]
        [MaxLength(255, ErrorMessage = "Tamanho máximo de {1} caracteres")]
        public string Nome { get; set; }
        
        [Required (ErrorMessage = "Campo obrigatório")]
        [MaxLength(50, ErrorMessage = "Tamanho máximo de {1} caracteres")]
        public string Crm { get; set; }
        
        [Required (ErrorMessage = "Campo obrigatório")]
        [MaxLength(2, ErrorMessage = "Tamanho máximo de {1} caracteres")]
        public string UfCrm { get; set; }
        
        [Required (ErrorMessage = "Campo obrigatório")]
        [MaxLength(255, ErrorMessage = "Tamanho máximo de {1} caracteres")]
        public string Especialidade { get; set; }
    }
}
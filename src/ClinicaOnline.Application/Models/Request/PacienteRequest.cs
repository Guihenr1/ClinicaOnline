using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicaOnline.Application.Models.Request
{
    public class PacienteRequest 
    {
        [MaxLength(255, ErrorMessage = "Tamanho máximo de {1} caracteres")]
        public string Nome { get; set; }
        
        [Required (ErrorMessage = "Campo obrigatório")]
        [MaxLength(11, ErrorMessage = "Tamanho máximo de {1} caracteres")]
        public string Cpf { get; set; }

        [MaxLength(20, ErrorMessage = "Tamanho máximo de {1} caracteres")]
        public string Telefone { get; set; }
        
        [Required (ErrorMessage = "Campo obrigatório")]
        public Guid MedicoId { get; set; }
    }
}
using ClinicaOnline.Application.Models.Response.Base;

namespace ClinicaOnline.Application.Models.Response
{
    public class ParceiroUpdateApiKeyResponse : Validator
    {
        public string Id { get; set; }
        
        public string Nome { get; set; }
        
        public string ApiKey { get; set; }
        
        
    }
}
using System.Collections.Generic;

namespace ClinicaOnline.Application.Models.Response.Base
{
    public abstract class Validator
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; }
        
        public Validator()
        {
            IsValid = true;
            Errors = new List<string>();
        }

        public void AddError(string error){
            Errors.Add(error);
            IsValid = false;
        }
    }
}
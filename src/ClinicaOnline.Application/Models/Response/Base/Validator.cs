using System.Collections.Generic;

namespace ClinicaOnline.Application.Models.Response.Base
{
    public abstract class Validator
    {
        private List<string> Errors { get; set; }
        
        public Validator()
        {
            Errors = new List<string>();
        }

        public void AddError(string error){
            Errors.Add(error);
        }

        public bool IsValid () {
            return Errors.Count == 0;
        } 

        public string[] GetErrors(){
            return Errors.ToArray();
        }
    }
}
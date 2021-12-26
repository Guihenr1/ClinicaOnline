using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Application.Models.Response;
using ClinicaOnline.Core.Entities;

namespace ClinicaOnline.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UserAuthenticateResponse> Authenticate(UserAuthenticateRequest user);
        Task<List<Usuario>> GetAll();
    }
}
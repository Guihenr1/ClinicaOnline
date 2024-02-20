using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Application.Models.Response;
using ClinicaOnline.Domain.Entities;

namespace ClinicaOnline.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UserAuthenticateResponse> Authenticate(UserAuthenticateRequest user);
        Task<IReadOnlyList<Usuario>> GetAll();
        Task<UserResponse> Add(UserRequest user);
    }
}
using System.Threading.Tasks;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Application.Models.Response;

namespace ClinicaOnline.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UserAuthenticateResponse> Authenticate(UserAuthenticateRequest user);
    }
}
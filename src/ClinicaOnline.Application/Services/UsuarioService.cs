using System;
using System.Globalization;
using System.Threading.Tasks;
using ClinicaOnline.Application.Interfaces;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Application.Models.Response;
using ClinicaOnline.Core.Configuration;
using ClinicaOnline.Core.Entities;
using ClinicaOnline.Core.Repositories;

namespace ClinicaOnline.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepository _userRepository;
        public UsuarioService(IUsuarioRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserAuthenticateResponse> Authenticate(UserAuthenticateRequest model)
        {
            var response = new UserAuthenticateResponse();

            var user = await _userRepository.GetUserByEmailAndPassword(
                new Usuario { Senha = model.password, Email = model.email });

            if (user == null){
                response.AddError("Usuário não encontrado");
                return response;
            }

            var expires = Settings.TokenExpires;
            var token = TokenService.GenerateToken(user, expires);

            return new UserAuthenticateResponse {
                accessToken = token,
                expiresIn = expires.ToString("yyyy-MM-ddTHH:mm:ss"),
                userInfo = new UserInfo {
                    email = user.Email,
                    name = user.Nome,
                    perfil = user.Eperfil.ToString()
                }
            };
        }
    }
}
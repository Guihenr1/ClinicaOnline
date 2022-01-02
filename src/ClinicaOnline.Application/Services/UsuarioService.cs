using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Application.Interfaces;
using ClinicaOnline.Application.Mapper;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Application.Models.Response;
using ClinicaOnline.Core.Configuration;
using ClinicaOnline.Core.Entities;
using ClinicaOnline.Core.Repositories;
using ClinicaOnline.Core.Utils;
using Microsoft.Extensions.Configuration;

namespace ClinicaOnline.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepository _userRepository;
        public IConfiguration _configuration { get; }
        public UsuarioService(IUsuarioRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<UserResponse> Add(UserRequest user)
        {
            var response = new UserResponse();
            var mapped = ObjectMapper.Mapper.Map<Usuario>(user);
            mapped.Id = Guid.NewGuid();

            if (await _userRepository.CheckEmailExists(user.Email)){
                response.AddError("Email já está em uso");
                return response;
            }

            mapped.Senha = Security.GenerateHash(user.Senha, Settings.Salt);
            
            var userAdd = await _userRepository.Add(mapped);
            return ObjectMapper.Mapper.Map<UserResponse>(userAdd);
        }

        public async Task<UserAuthenticateResponse> Authenticate(UserAuthenticateRequest model)
        {
            var response = new UserAuthenticateResponse();
            model.password = Security.GenerateHash(model.password, Settings.Salt);

            var user = await _userRepository.GetUserByEmailAndPassword(
                new Usuario { Senha = model.password, Email = model.email });

            if (user == null){
                response.AddError("Email ou senha incorretos");
                return response;
            }

            var expires = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["TokenExpires"]));
            var token = TokenService.GenerateToken(user, expires, _configuration);

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

        public async Task<IReadOnlyList<Usuario>> GetAll()
        {
            return await _userRepository.GetAll();
        }
    }
}
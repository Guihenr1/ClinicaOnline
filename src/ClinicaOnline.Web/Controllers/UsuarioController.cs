using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Application.Interfaces;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Application.Models.Response;
using ClinicaOnline.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ClinicaOnline.Web.Controllers
{
    [Route("v1/usuario")]
    public class UsuarioController : Controller
    {
        readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Realizar o login do usuário.
        /// </summary>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody]UserAuthenticateRequest user)
        {
            var response = new UserAuthenticateResponse();

            try
            {
                Log.Information("Inicio do login do usuário");
                if (!ModelState.IsValid) return BadRequest(ModelState);
                
                response = await _usuarioService.Authenticate(user);
                Log.Information("Resposta do login do usuário: {@response}", response);
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao tentar logar usuario: {@ex}", ex);
                return BadRequest();
            }

            return Ok(response);
        }

        /// <summary>
        /// Listar todos os usuários.
        /// </summary>
        [HttpGet]
        [Route("get-all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            IReadOnlyList<Usuario> response = new List<Usuario>();

            try
            {
                Log.Information("Inicio do obter todos os usuários");
                response = await _usuarioService.GetAll();
                Log.Information("Resultado do obter todos os usuários: {@response}", response);
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao tentar obter todos os usuários: {@ex}", ex);
                return BadRequest();
            }

            return Ok(response);
        }

        /// <summary>
        /// Adicionar um novo usuário.
        /// </summary>
        [HttpPost]
        [Route("add-usuario")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody]UserRequest user)
        {
            var response = new UserResponse();
            try
            {
                Log.Information("Inicio do adicionar usuário");
                if (!ModelState.IsValid) return BadRequest(ModelState);

                response = await _usuarioService.Add(user);
                Log.Information("Fim do adicionar usuário: {@response}", response);
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao tentar adicionar usuario: {@ex}", ex);
                return BadRequest();
            }

            return Ok(response);
        }
    }
}
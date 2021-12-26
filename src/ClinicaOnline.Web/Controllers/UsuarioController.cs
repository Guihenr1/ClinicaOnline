using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Application.Interfaces;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Application.Models.Response;
using ClinicaOnline.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<UserAuthenticateResponse> Authenticate([FromBody]UserAuthenticateRequest user)
        {
            return await _usuarioService.Authenticate(user);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<List<Usuario>> GetAll()
        {
            return await _usuarioService.GetAll();
        }
    }
}
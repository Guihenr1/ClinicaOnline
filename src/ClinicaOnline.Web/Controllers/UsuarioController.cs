using System.Threading.Tasks;
using ClinicaOnline.Application.Interfaces;
using ClinicaOnline.Application.Models.Request;
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

        /// <summary>
        /// Realizar o login do usuário.
        /// </summary>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody]UserAuthenticateRequest user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            return Ok(await _usuarioService.Authenticate(user));
        }

        /// <summary>
        /// Listar todos os usuários.
        /// </summary>
        [HttpGet]
        [Route("get-all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _usuarioService.GetAll());
        }

        /// <summary>
        /// Adicionar um novo usuário.
        /// </summary>
        [HttpPost]
        [Route("add-usuario")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromBody]UserRequest user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _usuarioService.Add(user));
        }
    }
}
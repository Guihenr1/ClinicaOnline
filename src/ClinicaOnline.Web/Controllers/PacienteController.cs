using System;
using System.Threading.Tasks;
using ClinicaOnline.Application.Interfaces;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Application.Models.Response;
using ClinicaOnline.Web.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaOnline.Web.Controllers
{
    [Route("v1/paciente")]
    [Authorize(Roles = "Admin,Atendente")]
    public class PacienteController : MainController
    {
        readonly IPacienteService _pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _pacienteService.GetAll());
        }

        [HttpPost]
        [Route("add-paciente")]
        public async Task<IActionResult> Add([FromBody]PacienteRequest paciente)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return CustomResponse(await _pacienteService.Add(paciente));
        }
    }
}
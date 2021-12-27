using System;
using System.Threading.Tasks;
using ClinicaOnline.Application.Interfaces;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Core.Entities;
using ClinicaOnline.Web.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaOnline.Web.Controllers
{
    [Route("v1/medico")]
    [Authorize(Roles = "Admin,Atendente")]
    public class MedicoController : MainController
    {
        readonly IMedicoService _medicoService;

        public MedicoController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _medicoService.GetAll());
        }

        [HttpPost]
        [Route("add-medico")]
        public async Task<IActionResult> Add([FromBody] MedicoRequest medico)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return CustomResponse(await _medicoService.Add(medico));
        }
    }
}
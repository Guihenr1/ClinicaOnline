using System;
using System.Threading.Tasks;
using ClinicaOnline.Application.Interfaces;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Web.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaOnline.Web.Controllers
{
    [Route("v1/medico")]
    public class MedicoController : MainController
    {
        readonly IMedicoService _medicoService;

        public MedicoController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Atendente")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _medicoService.GetAll());
        }
    }
}
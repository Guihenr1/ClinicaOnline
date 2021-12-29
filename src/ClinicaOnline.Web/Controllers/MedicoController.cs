using System;
using System.ComponentModel.DataAnnotations;
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
        [Route("get-all")]
        [Authorize(Roles = "Admin,Atendente")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _medicoService.GetAll());
        }

        [HttpPost]
        [Route("add-medico")]
        [Authorize(Roles = "Admin,Atendente")]
        public async Task<IActionResult> Add([FromBody] MedicoRequest medico)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return CustomResponse(await _medicoService.Add(medico));
        }

        [HttpPut]
        [Route("update-medico/{id}")]
        [Authorize(Roles = "Admin,Atendente")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] MedicoRequest medico)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _medicoService.Update(id, medico);

            if (!result.IsValid())
                return CustomResponse(result);
            else
                return NoContent();
        }

        [HttpDelete]
        [Route("delete-medico/{id}")]
        [Authorize(Roles = "Admin,Atendente")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _medicoService.Delete(id);

            if (!result.IsValid())
                return CustomResponse(result);
            else
                return NoContent();
        }

        [HttpGet]
        [Route("get-all-for-partners")]
        [Authorize(Policy = "ApiKeyPolicy")]
        public async Task<IActionResult> GetAllForPartners([FromHeader(Name = "x-api-key")][Required]string xApiKey, string ufCrm)
        {
            return Ok(await _medicoService.GetAllForPartners(ufCrm));
        }
    }
}
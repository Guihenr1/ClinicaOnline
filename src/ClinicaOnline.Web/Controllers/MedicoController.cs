using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ClinicaOnline.Application.Interfaces;
using ClinicaOnline.Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaOnline.Web.Controllers
{
    [Route("v1/medico")]
    public class MedicoController : Controller
    {
        readonly IMedicoService _medicoService;

        public MedicoController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        /// <summary>
        /// Listar todos os médicos.
        /// </summary>
        [HttpGet]
        [Route("get-all")]
        [Authorize(Roles = "Admin,Atendente")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _medicoService.GetAll());
        }

        /// <summary>
        /// Adicionar um novo médico.
        /// </summary>
        [HttpPost]
        [Route("add-medico")]
        [Authorize(Roles = "Admin,Atendente")]
        public async Task<IActionResult> Add([FromBody] MedicoRequest medico)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _medicoService.Add(medico));
        }

        /// <summary>
        /// Atualizar um médico.
        /// </summary>
        /// <param name="id">Id do médico que deseja atualizar.</param>
        [HttpPut]
        [Route("update-medico/{id}")]
        [Authorize(Roles = "Admin,Atendente")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] MedicoRequest medico)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _medicoService.Update(id, medico);

            return NoContent();
        }


        /// <summary>
        /// Excluir um médico.
        /// </summary>
        /// <param name="id">Id do médico que deseja excluir.</param>
        [HttpDelete]
        [Route("delete-medico/{id}")]
        [Authorize(Roles = "Admin,Atendente")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _medicoService.Delete(id);

            return NoContent();
        }

        /// <summary>
        /// Obter todos os médico para parceiros. Deve-se utilizar uma Api-Key de parceiro válida. 
        /// </summary>
        /// <param name="xApiKey">Api-Key do parceiro.</param>
        /// <param name="ufCrm">Filtrar pelo estado do médico.</param>
        [HttpGet]
        [Route("get-all-for-partners")]
        [Authorize(Policy = "ApiKeyPolicy")]
        public async Task<IActionResult> GetAllForPartners([FromHeader(Name = "x-api-key")][Required] string xApiKey, string ufCrm)
        {
            return Ok(await _medicoService.GetAllForPartners(ufCrm));
        }
    }
}
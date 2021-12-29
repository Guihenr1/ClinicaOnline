using System;
using System.Threading.Tasks;
using ClinicaOnline.Application.Interfaces;
using ClinicaOnline.Application.Models.Request;
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

        /// <summary>
        /// Listar todos os pacientes.
        /// </summary>
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _pacienteService.GetAll());
        }

        /// <summary>
        /// Adicionar um novo paciente.
        /// </summary>
        [HttpPost]
        [Route("add-paciente")]
        public async Task<IActionResult> Add([FromBody]PacienteRequest paciente)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return CustomResponse(await _pacienteService.Add(paciente));
        }

        /// <summary>
        /// Editar um paciente.
        /// </summary>
        /// <param name="id">Id do paciente que deseja atualizar.</param>
        [HttpPut]
        [Route("update-paciente/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody]PacienteRequest paciente)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _pacienteService.Update(id, paciente);

            if (!result.IsValid())
                return CustomResponse(result);
            else
                return NoContent();
        }

        /// <summary>
        /// Excluir um paciente.
        /// </summary>
        /// <param name="id">Id do paciente que deseja excluir.</param>
        [HttpDelete]
        [Route("delete-paciente/{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _pacienteService.Delete(id);

            if (!result.IsValid())
                return CustomResponse(result);
            else
                return NoContent();
        }
    }
}
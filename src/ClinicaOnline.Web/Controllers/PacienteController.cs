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
    [Route("v1/paciente")]
    [Authorize(Roles = "Admin,Atendente")]
    public class PacienteController : Controller
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
            IReadOnlyList<Paciente> response = new List<Paciente>();
            try
            {
                Log.Information("Inicio do obter todos os pacientes");
                response = await _pacienteService.GetAll();
                Log.Information("Fim do obter todos os pacientes");
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao obter todos os pacientes: {@ex}", ex);
                return BadRequest();
            }
            return Ok(response);
        }

        /// <summary>
        /// Adicionar um novo paciente.
        /// </summary>
        [HttpPost]
        [Route("add-paciente")]
        public async Task<IActionResult> Add([FromBody]PacienteRequest paciente)
        {
            var response = new PacienteResponse();
            try
            {
                Log.Information("Inicio do adicionar paciente");
                if (!ModelState.IsValid) return BadRequest(ModelState);

                response = await _pacienteService.Add(paciente);
                Log.Information("Fim do adicionar paciente");
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao adicionar paciente: {@ex}", ex);
                return BadRequest();
            }
            return Ok(response);
        }

        /// <summary>
        /// Editar um paciente.
        /// </summary>
        /// <param name="id">Id do paciente que deseja atualizar.</param>
        [HttpPut]
        [Route("update-paciente/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody]PacienteRequest paciente)
        {
            try
            {
                Log.Information("Inicio do editar paciente");
                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _pacienteService.Update(id, paciente);
                Log.Information("Fim do editar paciente");
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao editar paciente: {@ex}", ex);
                return BadRequest();
            }
            
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
            try
            {
                Log.Information("Inicio do excluir paciente");
                await _pacienteService.Delete(id);
                Log.Information("Fim do excluir paciente");
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao excluir paciente: {@ex}", ex);
                return BadRequest();
            }

            return NoContent();
        }
    }
}
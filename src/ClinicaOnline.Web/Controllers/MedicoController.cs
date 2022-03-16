using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            IReadOnlyList<Medico> response = new List<Medico>();     
            try
            {
                Log.Information("Inicio do obter todos os medicos");
                response = await _medicoService.GetAll();
                Log.Information("Fim do obter todos os medicos: {@response}", response);
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao tentar obter todos os medicos: {@ex}", ex);
                return BadRequest();
            }
            return Ok(response);
        }

        /// <summary>
        /// Adicionar um novo médico.
        /// </summary>
        [HttpPost]
        [Route("add-medico")]
        [Authorize(Roles = "Admin,Atendente")]
        public async Task<IActionResult> Add([FromBody] MedicoRequest medico)
        {
            var response = new MedicoResponse();
            try
            {
                Log.Information("Inicio do adicionar medico");
                if (!ModelState.IsValid) return BadRequest(ModelState);

                response = await _medicoService.Add(medico);
                Log.Information("Fim do adicionar medico {@response}", response);
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao tentar adicionar medico: {@ex}", ex);
                return BadRequest();
            }
            return Ok(response);
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
            try
            {
                Log.Information("Inicio do editar medico");
                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _medicoService.Update(id, medico);
                Log.Information("Fim do editar medico");
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao tentar editar medico: {@ex}", ex);
                return BadRequest();
            }

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
            try
            {
                Log.Information("Inicio do excluir medico");
                await _medicoService.Delete(id);
                Log.Information("Fim do excluir medico");
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao tentar excluir medico: {@ex}", ex);
                return BadRequest();
            }

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
            IReadOnlyList<Medico> response = new List<Medico>();
            try
            {
                Log.Information("Inicio do obter todos os medicos para parceiros");
                response = await _medicoService.GetAllForPartners(ufCrm);
                Log.Information("Fim do obter todos os medicos para parceiros: {@response}", response);
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao tentar obter todos os medicos para parceiros: {@ex}", ex);
                return BadRequest();
            }
            return Ok(response);
        }
    }
}
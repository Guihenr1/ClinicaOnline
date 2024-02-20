using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Application.Interfaces;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Application.Models.Response;
using ClinicaOnline.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ClinicaOnline.Web.Controllers
{
    [Route("v1/parceiro")]
    [Authorize(Roles = "Admin")]
    public class ParceiroController : Controller
    {
        readonly IParceiroService _parceiroService;

        public ParceiroController(IParceiroService parceiroService)
        {
            _parceiroService = parceiroService;
        }

        /// <summary>
        /// Listar todos os parceiros.
        /// </summary>
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            IReadOnlyList<Parceiro> response = new List<Parceiro>();
            try
            {
                Log.Information("Inicio do obter todos os parceiros");
                response = await _parceiroService.GetAll();
                Log.Information("Fim do obter todos os parceiros: {@response}", response);
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao tentar obter todos os parceiros: {@ex}", ex);
                return BadRequest();
            }
            return Ok(response);
        }

        /// <summary>
        /// Adicionar um novo parceiro.
        /// </summary>
        [HttpPost]
        [Route("add-parceiro")]
        public async Task<IActionResult> Add([FromBody]ParceiroRequest parceiro)
        {
            var response = new Parceiro();
            try
            {
                Log.Information("Inicio do adicionar parceiro");
                if (!ModelState.IsValid) return BadRequest(ModelState);

                response = await _parceiroService.Add(parceiro);
                Log.Information("Fim do adicionar parceiro: {@response}", response);
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao tentar adicionar parceiro: {@ex}", ex);
                return BadRequest();
            }

            return Ok(response);
        }

        /// <summary>
        /// Atualizar a Api-Key de um parceiro.
        /// </summary>
        /// <param name="id">Id do parceiro que deseja atualizar a Api-Key.</param>
        [HttpPatch]
        [Route("update-apikey/{id}")]
        public async Task<IActionResult> UpdateApiKey([FromRoute]Guid id)
        {
            var response = new ParceiroUpdateApiKeyResponse();
            try
            {
                Log.Information("Inicio do atualizar APIKEY");
                response = await _parceiroService.UpdateApiKey(id);
                Log.Information("Fim do atualizar APIKEY");
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao tentar atualizar APIKEY: {@ex}", ex);
                return BadRequest();
            }
            return Ok(response);
        }
    }
}
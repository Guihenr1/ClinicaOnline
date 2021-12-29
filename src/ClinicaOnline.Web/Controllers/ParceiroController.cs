using System;
using System.Threading.Tasks;
using ClinicaOnline.Application.Interfaces;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Web.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaOnline.Web.Controllers
{
    [Route("v1/parceiro")]
    [Authorize(Roles = "Admin")]
    public class ParceiroController : MainController
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
            return Ok(await _parceiroService.GetAll());
        }

        /// <summary>
        /// Adicionar um novo parceiro.
        /// </summary>
        [HttpPost]
        [Route("add-parceiro")]
        public async Task<IActionResult> Add([FromBody]ParceiroRequest parceiro)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _parceiroService.Add(parceiro));
        }

        /// <summary>
        /// Atualizar a Api-Key de um parceiro.
        /// </summary>
        /// <param name="id">Id do parceiro que deseja atualizar a Api-Key.</param>
        [HttpPatch]
        [Route("update-apikey/{id}")]
        public async Task<IActionResult> UpdateApiKey([FromRoute]Guid id)
        {
            return CustomResponse(await _parceiroService.UpdateApiKey(id));
        }
    }
}
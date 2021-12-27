using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Application.Interfaces;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Core.Entities;
using ClinicaOnline.Core.Repositories;

namespace ClinicaOnline.Application.Services
{
    public class ParceiroService : IParceiroService
    {
        private IParceiroRepository _parceiroRepository;
        public ParceiroService(IParceiroRepository parceiroRepository)
        {
            _parceiroRepository = parceiroRepository;
        }

        public async Task<Parceiro> Add(ParceiroRequest model)
        {
            var parceiro = new Parceiro() {
                Id = Guid.NewGuid(),
                Nome = model.Nome,
                ApiKey = Guid.NewGuid()
            };

            return await _parceiroRepository.Add(parceiro);
        }

        public async Task<IReadOnlyList<Parceiro>> GetAll()
        {
            return await _parceiroRepository.GetAll();
        }
    }
}
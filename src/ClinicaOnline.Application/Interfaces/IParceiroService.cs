using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Application.Models.Response;
using ClinicaOnline.Core.Entities;

namespace ClinicaOnline.Application.Interfaces
{
    public interface IParceiroService
    {
        Task<IReadOnlyList<Parceiro>> GetAll();
        Task<Parceiro> Add(ParceiroRequest model);
        Task<ParceiroUpdateApiKeyResponse> UpdateApiKey(Guid id);
        Task<bool> CheckApiKey(Guid apiKey);
    }
}
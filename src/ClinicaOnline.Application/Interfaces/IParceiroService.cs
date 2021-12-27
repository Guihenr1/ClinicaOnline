using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Core.Entities;

namespace ClinicaOnline.Application.Interfaces
{
    public interface IParceiroService
    {
        Task<IReadOnlyList<Parceiro>> GetAll();
        Task<Parceiro> Add(ParceiroRequest model);
    }
}
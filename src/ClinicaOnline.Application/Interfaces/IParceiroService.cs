using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Core.Entities;

namespace ClinicaOnline.Application.Interfaces
{
    public interface IParceiroService
    {
        Task<List<Parceiro>> GetAll();
    }
}
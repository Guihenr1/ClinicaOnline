using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Core.Entities;

namespace ClinicaOnline.Application.Interfaces
{
    public interface IMedicoService
    {
        Task<IReadOnlyList<Medico>> GetAll();
    }
}
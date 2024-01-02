using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ClinicaOnline.Application.Interfaces;
using ClinicaOnline.Application.Mapper;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Application.Models.Response;
using ClinicaOnline.Core.Entities;
using ClinicaOnline.Core.Notification;
using ClinicaOnline.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ClinicaOnline.Application.Services
{
    public class MedicoService : IMedicoService
    {
        private IMedicoRepository _medicoRepository;
        private Lazy<IPacienteService> _pacienteService;
        private NotificationContext _notificationContext;
        private readonly IConfiguration _configuration;
        public MedicoService(IMedicoRepository medicoRepository, Lazy<IPacienteService> pacienteService, NotificationContext notificationContext, IConfiguration configuration)
        {
            _medicoRepository = medicoRepository;
            _pacienteService = pacienteService;
            _notificationContext = notificationContext;
            _configuration = configuration;
        }

        public async Task<IReadOnlyList<Medico>> GetAll()
        {
            return await _medicoRepository.GetAll();
        }

        public async Task Update(Guid id, MedicoRequest model)
        {
            var checkMedicoExist = await _medicoRepository.GetById(id);
            if (checkMedicoExist == null)
            {
                _notificationContext.AddNotification(Guid.NewGuid().ToString(), "Médico não encontrado");
                return;
            }

            var checkCrmAndUfCrm = await _medicoRepository.GetByCrmAndUfCrm(model.Crm, model.UfCrm);
            if (checkCrmAndUfCrm != null && checkCrmAndUfCrm.Id != id)
            {
                _notificationContext.AddNotification(Guid.NewGuid().ToString(), "CRM e Uf já cadastrados para outro médico");
                return;
            }

            var checkSpeciality = await ValidateDoctorSpeciality(model.Especialidade);
            if (!checkSpeciality)
            {
                _notificationContext.AddNotification(Guid.NewGuid().ToString(), "Especialidade inválida");
                return;
            }

            var medico = ObjectMapper.Mapper.Map<Medico>(model);
            medico.Id = id;
            await _medicoRepository.Update(medico);
        }

        public async Task<MedicoResponse> Add(MedicoRequest medico)
        {
            var response = new MedicoResponse();

            var checkCrmAndUfCrm = await _medicoRepository.GetByCrmAndUfCrm(medico.Crm, medico.UfCrm);
            if (checkCrmAndUfCrm != null)
            {
                _notificationContext.AddNotification(Guid.NewGuid().ToString(), "CRM e Uf CRM já cadastrados");
                return response;
            }

            var checkSpeciality = await ValidateDoctorSpeciality(medico.Especialidade);
            if (!checkSpeciality)
            {
                _notificationContext.AddNotification(Guid.NewGuid().ToString(), "Especialidade inválida");
                return response;
            }

            var medicoAdd = await _medicoRepository.Add(ObjectMapper.Mapper.Map<Medico>(medico));

            return ObjectMapper.Mapper.Map<MedicoResponse>(medicoAdd);
        }

        public async Task Delete(Guid medicoId)
        {
            var response = new MedicoResponse();

            var pacientes = await _pacienteService.Value.GetPacientesByMedicoId(medicoId);
            if (pacientes.Any())
            {
                _notificationContext.AddNotification(Guid.NewGuid().ToString(), "Não é possível excluir médicos com pacientes associados");
                return;
            }

            var medico = await _medicoRepository.GetById(medicoId);
            if (medico == null)
            {
                _notificationContext.AddNotification(Guid.NewGuid().ToString(), "Médico não encontrado");
                return;
            }

            await _medicoRepository.Delete(medico);
        }

        public async Task<Medico> GetBydId(Guid id)
        {
            return await _medicoRepository.GetById(id);
        }

        public async Task<IReadOnlyList<Medico>> GetAllForPartners(string ufCrm)
        {
            var medicos = await _medicoRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(ufCrm))
                medicos = medicos.Where(x => x.UfCrm.ToLower() == ufCrm.ToLower()).ToList();

            return medicos;
        }

        private async Task<bool> ValidateDoctorSpeciality(string doctorSpeciality)
        {
            var isValid = "false";
            var functionUrl = _configuration.GetSection("AzureFunctions")["ValidateDoctorSpeciality"];
            var functionKey = _configuration.GetSection("AzureFunctions")["ValidateDoctorSpecialityKey"];

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-functions-key", functionKey);
            var response = await client.PostAsJsonAsync(functionUrl, new { doctorSpeciality });

            if (response.IsSuccessStatusCode)
            {
                isValid = await response.Content.ReadAsStringAsync();
            }

            return Convert.ToBoolean(isValid);
        }
    }
}
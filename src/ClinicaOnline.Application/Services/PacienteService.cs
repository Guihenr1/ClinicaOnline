using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Application.Interfaces;
using ClinicaOnline.Application.Mapper;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Application.Models.Response;
using ClinicaOnline.Core.Entities;
using ClinicaOnline.Core.Notification;
using ClinicaOnline.Core.Repositories;
using ClinicaOnline.Core.Utils;

namespace ClinicaOnline.Application.Services
{
    public class PacienteService : IPacienteService
    {
        private IPacienteRepository _pacienteRepository;
        private IMedicoService _medicoService;
        private NotificationContext _notificationContext;
        public PacienteService(IPacienteRepository pacienteRepository, IMedicoService medicoService, NotificationContext notificationContext)
        {
            _pacienteRepository = pacienteRepository;
            _medicoService = medicoService;
            _notificationContext = notificationContext;
        }

        public async Task<IReadOnlyList<Paciente>> GetPacientesByMedicoId(Guid medicoId)
        {
            return await _pacienteRepository.GetPacientesByMedicoId(medicoId);
        }

        public async Task<IReadOnlyList<Paciente>> GetAll()
        {
            return await _pacienteRepository.GetAll();
        }

        public async Task<PacienteResponse> Add(PacienteRequest model)
        {
            var response = new PacienteResponse();

            if (await _pacienteRepository.GetByCpf(model.Cpf) != null){
			    _notificationContext.AddNotification(Guid.NewGuid().ToString(), "Cpf já cadastrado");
                return response;
            }

            if (!Cpf.ValidateCpf(model.Cpf)){
			    _notificationContext.AddNotification(Guid.NewGuid().ToString(), "Cpf inválido");
                return response;
            }        

            var mapped = ObjectMapper.Mapper.Map<Paciente>(model);
            mapped.Id = Guid.NewGuid();

            mapped.Medico = await _medicoService.GetBydId(model.MedicoId);
            if (mapped.Medico == null) {
			    _notificationContext.AddNotification(Guid.NewGuid().ToString(), "Médico não encontrado");
                return response;
            }

            var pacienteAdd = await _pacienteRepository.Add(mapped);

            return ObjectMapper.Mapper.Map<PacienteResponse>(pacienteAdd);
        }

        public async Task Update(Guid id, PacienteRequest model)
        {
            var getByCpf = await _pacienteRepository.GetByCpf(model.Cpf);
            if (getByCpf != null && getByCpf.Id != id){
			    _notificationContext.AddNotification(Guid.NewGuid().ToString(), "Cpf já cadastrado");
                return;
            }

            if (!Cpf.ValidateCpf(model.Cpf)){
			    _notificationContext.AddNotification(Guid.NewGuid().ToString(), "Cpf inválido");
                return;
            }        

            var mapped = ObjectMapper.Mapper.Map<Paciente>(model);
            mapped.Id = id;

            var medico = await _medicoService.GetBydId(model.MedicoId);
            if (medico == null) {
			    _notificationContext.AddNotification(Guid.NewGuid().ToString(), "Médico não encontrado");
                return;
            }

            await _pacienteRepository.Update(mapped);
        }

        public async Task Delete(Guid id)
        {
            var paciente = await _pacienteRepository.GetById(id);

            if (paciente == null) {
			    _notificationContext.AddNotification(Guid.NewGuid().ToString(), "Paciente não encontrado");
                return;
            }

            await _pacienteRepository.Delete(paciente);
        }
    }
}
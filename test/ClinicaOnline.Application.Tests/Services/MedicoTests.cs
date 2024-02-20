using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Application.Interfaces;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Application.Services;
using ClinicaOnline.Core.Entities;
using ClinicaOnline.Core.Notification;
using ClinicaOnline.Core.Repositories;
using Moq;
using Xunit;
using AutoMapper;
using ClinicaOnline.Application.Models.Response;
using Microsoft.Extensions.Configuration;

namespace ClinicaOnline.Application.Tests.Services
{
    public class MedicoTests
    {
        private Mock<IMedicoRepository> _mockMedicoRepository;
        private Mock<IPacienteService> _mockPacienteService;
        private Mock<Lazy<IPacienteService>> _mockLazyPacienteService;
        private Mock<NotificationContext> _mockNotificationContext;
        private Mock<IConfiguration> _mockConfiguration;

        public MedicoTests()
        {
            _mockMedicoRepository = new Mock<IMedicoRepository>();
            _mockPacienteService = new Mock<IPacienteService>();
            _mockLazyPacienteService = new Mock<Lazy<IPacienteService>>();
            _mockNotificationContext = new Mock<NotificationContext>();
            _mockConfiguration = new Mock<IConfiguration>();
        }  

        [Fact]
        public async Task Get_All_Medicos()
        {
            var medicoService = new MedicoService(
                _mockMedicoRepository.Object, _mockLazyPacienteService.Object, 
                _mockNotificationContext.Object, _mockConfiguration.Object
            );
            var medicoList = await medicoService.GetAll();

            _mockMedicoRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public async Task Update_Medico()
        {
            var medicoService = new MedicoService(
                _mockMedicoRepository.Object, _mockLazyPacienteService.Object, 
                _mockNotificationContext.Object, _mockConfiguration.Object
            );
            var medico = new Medico(){
                Id = Guid.NewGuid()
            };
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<MedicoRequest, Medico>(It.IsAny<MedicoRequest>()))
                .Returns(new Medico());
            _mockMedicoRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(Task.FromResult(medico));
            _mockMedicoRepository.Setup(x => x.GetByCrmAndUfCrm(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(medico));

            await medicoService.Update(medico.Id, new MedicoRequest());

            _mockMedicoRepository.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);
            _mockMedicoRepository.Verify(x => x.GetByCrmAndUfCrm(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _mockMedicoRepository.Verify(x => x.Update(It.IsAny<Medico>()), Times.Once);
        }

        [Fact]
        public async Task Update_Medico_Not_Found()
        {
            var medicoService = new MedicoService(
                _mockMedicoRepository.Object, _mockLazyPacienteService.Object, 
                _mockNotificationContext.Object, _mockConfiguration.Object
            );
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<MedicoRequest, Medico>(It.IsAny<MedicoRequest>()))
                .Returns(new Medico());
                
            await medicoService.Update(Guid.NewGuid(), new MedicoRequest());

            _mockNotificationContext.Verify(x => 
                x.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Update_Medico_CRM_UF_Already_Exist()
        {
            var medicoService = new MedicoService(
                _mockMedicoRepository.Object, _mockLazyPacienteService.Object, 
                _mockNotificationContext.Object, _mockConfiguration.Object
            );
            var medico = new Medico(){
                Id = Guid.NewGuid(),
                UfCrm = "SP",
                Crm = "12345"
            };
            var medico2 = new Medico(){
                Id = Guid.NewGuid(),
                UfCrm = "SP",
                Crm = "12345"
            };
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<MedicoRequest, Medico>(It.IsAny<MedicoRequest>()))
                .Returns(new Medico());
            _mockMedicoRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(Task.FromResult(medico));
            _mockMedicoRepository.Setup(x => x.GetByCrmAndUfCrm(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(medico2));

            await medicoService.Update(medico.Id, new MedicoRequest());

            _mockNotificationContext.Verify(x => 
                x.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Add_Medico()
        {
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<MedicoRequest, Medico>(It.IsAny<MedicoRequest>()))
                .Returns(new Medico());
            mapperMock.Setup(m => m.Map<Medico, MedicoResponse>(It.IsAny<Medico>()))
                .Returns(new MedicoResponse());
            _mockMedicoRepository.Setup(x => x.Add(It.IsAny<Medico>())).Returns(Task.FromResult(new Medico()));
            var medicoService = new MedicoService(
                _mockMedicoRepository.Object, _mockLazyPacienteService.Object, 
                _mockNotificationContext.Object, _mockConfiguration.Object
            );

            await medicoService.Add(new MedicoRequest());

            _mockMedicoRepository.Verify(x => x.Add(It.IsAny<Medico>()), Times.Once);
        }

        [Fact]
        public async Task Add_Medico_Crm_And_Uf_Already_Exists()
        {
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<MedicoRequest, Medico>(It.IsAny<MedicoRequest>()))
                .Returns(new Medico());
            mapperMock.Setup(m => m.Map<Medico, MedicoResponse>(It.IsAny<Medico>()))
                .Returns(new MedicoResponse());
            _mockMedicoRepository.Setup(x => x.GetByCrmAndUfCrm(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(new Medico()));
            var medicoService = new MedicoService(
                _mockMedicoRepository.Object, _mockLazyPacienteService.Object, 
                _mockNotificationContext.Object, _mockConfiguration.Object
            );

            await medicoService.Add(new MedicoRequest());

            _mockNotificationContext.Verify(x => 
                x.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Delete_Medico()
        {
            IReadOnlyList<Paciente> pacienteList = new List<Paciente>();
            _mockMedicoRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(Task.FromResult(new Medico()));
            _mockPacienteService.Setup(x => x.GetPacientesByMedicoId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(pacienteList));
            var lazyPacienteService = new Lazy<IPacienteService>(() => _mockPacienteService.Object);
            var medicoService = new MedicoService(
                _mockMedicoRepository.Object, lazyPacienteService, 
                _mockNotificationContext.Object, _mockConfiguration.Object
            );

            await medicoService.Delete(It.IsAny<Guid>());

            _mockMedicoRepository.Verify(x => x.Delete(It.IsAny<Medico>()), Times.Once);
        }

        [Fact]
        public async Task Delete_Medico_With_Pacientes()
        {
            IReadOnlyList<Paciente> pacienteList = new List<Paciente>(){
                new Paciente()
            };
            _mockPacienteService.Setup(x => x.GetPacientesByMedicoId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(pacienteList));
            var lazyPacienteService = new Lazy<IPacienteService>(() => _mockPacienteService.Object);
            var medicoService = new MedicoService(
                _mockMedicoRepository.Object, lazyPacienteService, 
                _mockNotificationContext.Object, _mockConfiguration.Object
            );

            await medicoService.Delete(It.IsAny<Guid>());

            _mockNotificationContext.Verify(x => 
                x.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Delete_Medico_Not_Found()
        {
            IReadOnlyList<Paciente> pacienteList = new List<Paciente>();
            _mockPacienteService.Setup(x => x.GetPacientesByMedicoId(It.IsAny<Guid>()))
                .Returns(Task.FromResult(pacienteList));
            var lazyPacienteService = new Lazy<IPacienteService>(() => _mockPacienteService.Object);
            var medicoService = new MedicoService(
                _mockMedicoRepository.Object, lazyPacienteService, 
                _mockNotificationContext.Object, _mockConfiguration.Object
            );

            await medicoService.Delete(It.IsAny<Guid>());

            _mockNotificationContext.Verify(x => 
                x.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using ClinicaOnline.Core.Entities;
using ClinicaOnline.Infrastructure.Data;
using ClinicaOnline.Infrastructure.Repositories;
using ClinicaOnline.Infrastructure.Tests.Builders;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ClinicaOnline.Infrastructure.Tests.Repositories
{
    public class PacienteTests
    {
        private readonly Context _context;
        private readonly PacienteRepository _pacienteRepository;
        private PacienteBuilder PacienteBuilder { get; } = new PacienteBuilder();
        private MedicoBuilder MedicoBuilder { get; } = new MedicoBuilder();

        public PacienteTests()
        {
            var dbOptions = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "DbTestsPaciente")
                .Options;
            _context = new Context(dbOptions);
            _pacienteRepository = new PacienteRepository(_context);
        }

        [Fact]
        public async Task Get_Pacientes_By_MedicoId()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var existingPaciente = PacienteBuilder.WithDefaultValues();
            var existingMedico = MedicoBuilder.WithDefaultValues();
            existingPaciente.MedicoId = existingMedico.Id;
            _context.Pacientes.Add(existingPaciente);        
            _context.Medicos.Add(existingMedico);   
            _context.SaveChanges();

            var pacientesFromRepository = await _pacienteRepository.GetPacientesByMedicoId(existingPaciente.MedicoId);
            Assert.Equal(existingPaciente.Id, pacientesFromRepository[0].Id);
            Assert.Equal(existingPaciente.Nome, pacientesFromRepository[0].Nome);
            Assert.Equal(existingPaciente.Cpf, pacientesFromRepository[0].Cpf);
            Assert.Equal(existingPaciente.Telefone, pacientesFromRepository[0].Telefone);
            Assert.Equal(existingPaciente.MedicoId, pacientesFromRepository[0].MedicoId);
        }

        [Fact]
        public async Task Get_All_Pacientes()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var existingPaciente = PacienteBuilder.WithDefaultValues();
            _context.Pacientes.Add(existingPaciente);        
            _context.SaveChanges();

            var pacientesFromRepository = await _pacienteRepository.GetAllAsync();
            var pacienteAdded = pacientesFromRepository.First(x => x.Id == existingPaciente.Id);
            Assert.Equal(existingPaciente.Nome, pacienteAdded.Nome);
            Assert.Equal(existingPaciente.Cpf, pacienteAdded.Cpf);
            Assert.Equal(existingPaciente.Telefone, pacienteAdded.Telefone);
            Assert.Equal(existingPaciente.MedicoId, pacienteAdded.MedicoId);
        }

        [Fact]
        public async Task Add_Paciente()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var existingPaciente = PacienteBuilder.WithDefaultValues();
            var pacienteAdded = await _pacienteRepository.Add(existingPaciente);    
            _context.SaveChanges();

            Assert.Equal(existingPaciente.Id, pacienteAdded.Id);
            Assert.Equal(existingPaciente.Nome, pacienteAdded.Nome);
            Assert.Equal(existingPaciente.Cpf, pacienteAdded.Cpf);
            Assert.Equal(existingPaciente.Telefone, pacienteAdded.Telefone);
            Assert.Equal(existingPaciente.MedicoId, pacienteAdded.MedicoId);
        }

        [Fact]
        public async Task Get_Pacientes_By_CPF()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var existingPaciente = PacienteBuilder.WithDefaultValues();
            _context.Pacientes.Add(existingPaciente);        
            _context.SaveChanges();

            var pacienteFromRepository = await _pacienteRepository.GetByCpf(existingPaciente.Cpf);
            Assert.Equal(existingPaciente.Id, pacienteFromRepository.Id);
            Assert.Equal(existingPaciente.Nome, pacienteFromRepository.Nome);
            Assert.Equal(existingPaciente.Cpf, pacienteFromRepository.Cpf);
            Assert.Equal(existingPaciente.Telefone, pacienteFromRepository.Telefone);
            Assert.Equal(existingPaciente.MedicoId, pacienteFromRepository.MedicoId);
        }

        [Fact]
        public async Task Update_Pacientes()
        {            
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var id = Guid.NewGuid();
            var existingPaciente = PacienteBuilder.WithDefaultValues();
            var existingMedico = MedicoBuilder.WithDefaultValues();
            var existingMedicoUpdate = MedicoBuilder.WithDefaultValues();
            existingPaciente.MedicoId = existingMedico.Id;
            existingPaciente.Id = id;
            _context.Pacientes.Add(existingPaciente); 

            var newName = "Henrique";
            var newCpf = "38810111028";
            var newTelefone = "11988881111";

            await _pacienteRepository.Update(new Paciente{
                Id = id,
                Nome = newName,
                Cpf = newCpf,
                Telefone = newTelefone,
                MedicoId = existingMedicoUpdate.Id
            });

            _context.SaveChanges();

            var pacienteFromRepository = await _pacienteRepository.GetById(id);
            Assert.Equal(existingPaciente.Id, pacienteFromRepository.Id);
            Assert.Equal(newName, pacienteFromRepository.Nome);
            Assert.Equal(newCpf, pacienteFromRepository.Cpf);
            Assert.Equal(newTelefone, pacienteFromRepository.Telefone);
            Assert.Equal(existingMedicoUpdate.Id, pacienteFromRepository.MedicoId);
        }

        [Fact]
        public async Task Delete_Paciente()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var existingPaciente = PacienteBuilder.WithDefaultValues();
            _context.Pacientes.Add(existingPaciente); 

            await _pacienteRepository.Delete(existingPaciente);       
            _context.SaveChanges();

            var checkPacinete = await _pacienteRepository.GetById(existingPaciente.Id);
            Assert.Null(checkPacinete);
        }

        [Fact]
        public async Task Get_Pacientes_By_Id()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var existingPaciente = PacienteBuilder.WithDefaultValues();
            _context.Pacientes.Add(existingPaciente);        
            _context.SaveChanges();

            var pacienteFromRepository = await _pacienteRepository.GetById(existingPaciente.Id);
            Assert.Equal(existingPaciente.Id, pacienteFromRepository.Id);
            Assert.Equal(existingPaciente.Nome, pacienteFromRepository.Nome);
            Assert.Equal(existingPaciente.Cpf, pacienteFromRepository.Cpf);
            Assert.Equal(existingPaciente.Telefone, pacienteFromRepository.Telefone);
            Assert.Equal(existingPaciente.MedicoId, pacienteFromRepository.MedicoId);
        }
    }
}
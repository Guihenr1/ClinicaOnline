using System;
using System.Threading.Tasks;
using ClinicaOnline.Core.Entities;
using ClinicaOnline.Infrastructure.Data;
using ClinicaOnline.Infrastructure.Repositories;
using ClinicaOnline.Infrastructure.Tests.Builders;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace ClinicaOnline.Infrastructure.Tests.Repositories
{
    public class MedicoTests
    {
        private readonly Context _context;
        private readonly MedicoRepository _medicoRepository;
        private readonly ITestOutputHelper _output;
        private MedicoBuilder MedicoBuilder { get; } = new MedicoBuilder();

        public MedicoTests(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "DbTests")
                .Options;
            _context = new Context(dbOptions);
            _context.Database.EnsureDeleted();
            _medicoRepository = new MedicoRepository(_context);
        }

        [Fact]
        public async Task Get_All_Medicos()
        {
            var existingMedico = MedicoBuilder.WithDefaultValues();
            _context.Medicos.Add(existingMedico);           
            _context.SaveChanges();

            var medicosFromRepository = await _medicoRepository.GetAllAsync();
            Assert.Equal(MedicoBuilder.Id, medicosFromRepository[0].Id);
        }

        [Fact]
        public async Task Add_Medico()
        {
            var existingMedico = MedicoBuilder.WithDefaultValues();
            var medicoAdded = await _medicoRepository.Add(existingMedico);

            Assert.Equal(MedicoBuilder.Id, medicoAdded.Id);
        }

        [Fact]
        public async Task Get_Medico_By_Id()
        {
            var existingMedico = MedicoBuilder.WithDefaultValues();
            _context.Medicos.Add(existingMedico);           
            _context.SaveChanges();

            var medicoFromRepository = await _medicoRepository.GetById(existingMedico.Id);
            Assert.Equal(MedicoBuilder.Id, medicoFromRepository.Id);
            Assert.Equal(MedicoBuilder.Nome, medicoFromRepository.Nome);
            Assert.Equal(MedicoBuilder.Crm, medicoFromRepository.Crm);
            Assert.Equal(MedicoBuilder.UfCrm, medicoFromRepository.UfCrm);
            Assert.Equal(MedicoBuilder.Especialidade, medicoFromRepository.Especialidade);
        }

        [Fact]
        public async Task Update_Medico()
        {
            var existingMedico = MedicoBuilder.WithDefaultValues();
            _context.Medicos.Add(existingMedico);           
            _context.SaveChanges();

            var newCrm = "456";
            var newNome = "Guilherme 2";
            var newEspecialidade = "Cardiologista";
            var newUfCrm = "PR";
            await _medicoRepository.Update(new Medico() {
                Id = MedicoBuilder.Id,
                Crm = newCrm,
                Nome = newNome,
                Especialidade = newEspecialidade,
                UfCrm = newUfCrm
            });

            var medicoFromRepository = await _medicoRepository.GetById(MedicoBuilder.Id);
            Assert.Equal(MedicoBuilder.Id, medicoFromRepository.Id);
            Assert.Equal(newNome, medicoFromRepository.Nome);
            Assert.Equal(newCrm, medicoFromRepository.Crm);
            Assert.Equal(newUfCrm, medicoFromRepository.UfCrm);
            Assert.Equal(newEspecialidade, medicoFromRepository.Especialidade);
        }

        [Fact]
        public async Task Delete_Medico()
        {
            var existingMedico = MedicoBuilder.WithDefaultValues();
            var medicoAdded = await _medicoRepository.Add(existingMedico);

            await _medicoRepository.Delete(existingMedico);
            var medicoFromRepository = await _medicoRepository.GetById(existingMedico.Id);

            Assert.Null(medicoFromRepository);
        }

        [Fact]
        public async Task Get_Medico_By_Crm_And_UfCrm()
        {
            var existingMedico = MedicoBuilder.WithDefaultValues();
            _context.Medicos.Add(existingMedico);           
            _context.SaveChanges();

            var medicoFromRepository = await _medicoRepository
                .GetByCrmAndUfCrm(existingMedico.Crm, existingMedico.UfCrm);
            Assert.Equal(MedicoBuilder.Id, medicoFromRepository.Id);
            Assert.Equal(MedicoBuilder.Nome, medicoFromRepository.Nome);
            Assert.Equal(MedicoBuilder.Crm, medicoFromRepository.Crm);
            Assert.Equal(MedicoBuilder.UfCrm, medicoFromRepository.UfCrm);
            Assert.Equal(MedicoBuilder.Especialidade, medicoFromRepository.Especialidade);
        }
    }
}
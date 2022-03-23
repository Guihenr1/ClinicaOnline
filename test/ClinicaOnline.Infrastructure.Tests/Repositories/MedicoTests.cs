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
    public class MedicoTests
    {
        private readonly Context _context;
        private readonly MedicoRepository _medicoRepository;
        private MedicoBuilder MedicoBuilder { get; } = new MedicoBuilder();

        public MedicoTests()
        {
            var dbOptions = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "DbTests")
                .Options;
            _context = new Context(dbOptions);
            _medicoRepository = new MedicoRepository(_context);
        }

        [Fact]
        public async Task Get_All_Medicos()
        {
            var existingMedico = MedicoBuilder.WithDefaultValues();
            _context.Medicos.Add(existingMedico);           
            _context.SaveChanges();

            var medicosFromRepository = await _medicoRepository.GetAllAsync();
            var medicoAdded = medicosFromRepository.First(x => x.Id == existingMedico.Id);
            Assert.Equal(existingMedico.Id, medicoAdded.Id);
            Assert.Equal(existingMedico.Nome, medicoAdded.Nome);
            Assert.Equal(existingMedico.Crm, medicoAdded.Crm);
            Assert.Equal(existingMedico.UfCrm, medicoAdded.UfCrm);
            Assert.Equal(existingMedico.Especialidade, medicoAdded.Especialidade);
        }

        [Fact]
        public async Task Add_Medico()
        {
            var existingMedico = MedicoBuilder.WithDefaultValues();
            var medicoAdded = await _medicoRepository.Add(existingMedico);       
            _context.SaveChanges();

            Assert.Equal(existingMedico.Id, medicoAdded.Id);
            Assert.Equal(existingMedico.Nome, medicoAdded.Nome);
            Assert.Equal(existingMedico.Crm, medicoAdded.Crm);
            Assert.Equal(existingMedico.UfCrm, medicoAdded.UfCrm);
            Assert.Equal(existingMedico.Especialidade, medicoAdded.Especialidade);
        }

        [Fact]
        public async Task Get_Medico_By_Id()
        {
            var existingMedico = MedicoBuilder.WithDefaultValues();
            _context.Medicos.Add(existingMedico);           
            _context.SaveChanges();

            var medicoFromRepository = await _medicoRepository.GetById(existingMedico.Id);
            Assert.Equal(existingMedico.Id, medicoFromRepository.Id);
            Assert.Equal(existingMedico.Nome, medicoFromRepository.Nome);
            Assert.Equal(existingMedico.Crm, medicoFromRepository.Crm);
            Assert.Equal(existingMedico.UfCrm, medicoFromRepository.UfCrm);
            Assert.Equal(existingMedico.Especialidade, medicoFromRepository.Especialidade);
        }

        [Fact]
        public async Task Update_Medico()
        {
            var id = Guid.NewGuid();
            var existingMedico = MedicoBuilder.WithDefaultValues();
            existingMedico.Id = id;
            _context.Medicos.Add(existingMedico);           
            _context.SaveChanges();

            var newCrm = "456";
            var newNome = "Guilherme 2";
            var newEspecialidade = "Cardiologista";
            var newUfCrm = "PR";
            await _medicoRepository.Update(new Medico() {
                Id = id,
                Crm = newCrm,
                Nome = newNome,
                Especialidade = newEspecialidade,
                UfCrm = newUfCrm
            });

            var medicoFromRepository = await _medicoRepository.GetById(id);
            Assert.Equal(id, medicoFromRepository.Id);
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
            _context.Database.EnsureDeleted();

            var existingMedico = MedicoBuilder.WithDefaultValues();
            _context.Medicos.Add(existingMedico);           
            _context.SaveChanges();

            var medicoFromRepository = await _medicoRepository
                .GetByCrmAndUfCrm(existingMedico.Crm, existingMedico.UfCrm);
            Assert.Equal(existingMedico.Id, medicoFromRepository.Id);
            Assert.Equal(existingMedico.Nome, medicoFromRepository.Nome);
            Assert.Equal(existingMedico.Crm, medicoFromRepository.Crm);
            Assert.Equal(existingMedico.UfCrm, medicoFromRepository.UfCrm);
            Assert.Equal(existingMedico.Especialidade, medicoFromRepository.Especialidade);
        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using ClinicaOnline.Domain.Entities;
using ClinicaOnline.Infrastructure.Data;
using ClinicaOnline.Infrastructure.Repositories;
using ClinicaOnline.Infrastructure.Tests.Builders;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ClinicaOnline.Infrastructure.Tests.Repositories
{
    public class ParceiroTests
    {
        private readonly Context _context;
        private readonly ParceiroRepository _parceiroRepository;
        private ParceiroBuilder ParceiroBuilder { get; } = new ParceiroBuilder();

        public ParceiroTests()
        {
            var dbOptions = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "DbTestsParceiro")
                .Options;
            _context = new Context(dbOptions);
            _parceiroRepository = new ParceiroRepository(_context);
        }

        [Fact]
        public async Task Get_All_Paceiros()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var existingParceiro = ParceiroBuilder.WithDefaultValues();
            _context.Parceiros.Add(existingParceiro);           
            _context.SaveChanges();

            var parceirosFromRepository = await _parceiroRepository.GetAllAsync();
            var parceiroAdded = parceirosFromRepository.First(x => x.Id == existingParceiro.Id);
            Assert.Equal(existingParceiro.Id, parceiroAdded.Id);
            Assert.Equal(existingParceiro.Nome, parceiroAdded.Nome);
            Assert.Equal(existingParceiro.ApiKey, parceiroAdded.ApiKey);
        }

        [Fact]
        public async Task Add_Paceiro()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var existingParceiro = ParceiroBuilder.WithDefaultValues();
            await _parceiroRepository.Add(existingParceiro);
            _context.SaveChanges();

            var parceirosFromRepository = await _parceiroRepository.GetAllAsync();
            var parceiroAdded = parceirosFromRepository.First(x => x.Id == existingParceiro.Id);
            Assert.Equal(existingParceiro.Id, parceiroAdded.Id);
            Assert.Equal(existingParceiro.Nome, parceiroAdded.Nome);
            Assert.Equal(existingParceiro.ApiKey, parceiroAdded.ApiKey);
        }

        [Fact]
        public async Task Update_Paceiro()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var existingParceiro = ParceiroBuilder.WithDefaultValues();
            _context.Parceiros.Add(existingParceiro);  

            var newName = "Henrique";
            var newApiKey = Guid.NewGuid();

            await _parceiroRepository.Update(new Parceiro(){
                Id = existingParceiro.Id,
                Nome = newName,
                ApiKey = newApiKey
            });

            _context.SaveChanges();

            var parceirosFromRepository = await _parceiroRepository.GetAllAsync();
            var parceiroAdded = parceirosFromRepository.First(x => x.Id == existingParceiro.Id);
            Assert.Equal(existingParceiro.Id, parceiroAdded.Id);
            Assert.Equal(existingParceiro.Nome, newName);
            Assert.Equal(existingParceiro.ApiKey, newApiKey);
        }

        [Fact]
        public async Task Get_By_Id()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var existingParceiro = ParceiroBuilder.WithDefaultValues();
            _context.Parceiros.Add(existingParceiro);           
            _context.SaveChanges();

            var parceiroFromRepository = await _parceiroRepository.GetById(existingParceiro.Id);
            Assert.Equal(existingParceiro.Id, parceiroFromRepository.Id);
            Assert.Equal(existingParceiro.Nome, parceiroFromRepository.Nome);
            Assert.Equal(existingParceiro.ApiKey, parceiroFromRepository.ApiKey);
        }

        [Fact]
        public async Task Check_Api_Key_Exist()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            
            var existingParceiro = ParceiroBuilder.WithDefaultValues();
            _context.Parceiros.Add(existingParceiro);           
            _context.SaveChanges();

            var checkApiKey = await _parceiroRepository.CheckApiKey(existingParceiro.ApiKey);
            Assert.True(checkApiKey);
        }

        [Fact]
        public async Task Check_Api_Key_Not_Exist()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var existingParceiro = ParceiroBuilder.WithDefaultValues();
            _context.Parceiros.Add(existingParceiro);           
            _context.SaveChanges();

            var checkApiKey = await _parceiroRepository.CheckApiKey(Guid.NewGuid());
            Assert.False(checkApiKey);
        }
    }
}
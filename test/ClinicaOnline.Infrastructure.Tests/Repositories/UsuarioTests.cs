using System.Linq;
using System.Threading.Tasks;
using ClinicaOnline.Infrastructure.Data;
using ClinicaOnline.Infrastructure.Repositories;
using ClinicaOnline.Infrastructure.Tests.Builders;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ClinicaOnline.Infrastructure.Tests.Repositories
{
    public class UsuarioTests
    {
        private readonly Context _context;
        private readonly UsuarioRepository _usuarioRepository;
        private UsuarioBuilder UsuarioBuilder { get; } = new UsuarioBuilder();

        public UsuarioTests()
        {
            var dbOptions = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "DbTestsUsuario")
                .Options;
            _context = new Context(dbOptions);
            _usuarioRepository = new UsuarioRepository(_context);
        }

        [Fact]
        public async Task Get_All_Usuarios()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var existingUsuario = UsuarioBuilder.WithDefaultValues();
            _context.Usuarios.Add(existingUsuario);           
            _context.SaveChanges();

            var usuariosFromRepository = await _usuarioRepository.GetAll();
            var usuarioAdded = usuariosFromRepository.First(x => x.Id == existingUsuario.Id);
            Assert.Equal(existingUsuario.Id, usuarioAdded.Id);
            Assert.Equal(existingUsuario.Email, usuarioAdded.Email);
            Assert.Equal(existingUsuario.Nome, usuarioAdded.Nome);
            Assert.Equal(existingUsuario.Senha, usuarioAdded.Senha);
            Assert.Equal(existingUsuario.Eperfil, usuarioAdded.Eperfil);
        }

        [Fact]
        public async Task Get_Usuario_By_Email_And_Password()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var existingUsuario = UsuarioBuilder.WithDefaultValues();
            _context.Usuarios.Add(existingUsuario);           
            _context.SaveChanges();

            var usuarioFromRepository = await _usuarioRepository.GetUserByEmailAndPassword(existingUsuario);
            Assert.Equal(existingUsuario.Id, usuarioFromRepository.Id);
            Assert.Equal(existingUsuario.Email, usuarioFromRepository.Email);
            Assert.Equal(existingUsuario.Nome, usuarioFromRepository.Nome);
            Assert.Equal(existingUsuario.Senha, usuarioFromRepository.Senha);
            Assert.Equal(existingUsuario.Eperfil, usuarioFromRepository.Eperfil);
        }

        [Fact]
        public async Task Add_Usuario()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var existingUsuario = UsuarioBuilder.WithDefaultValues();
            await _usuarioRepository.Add(existingUsuario);           
            _context.SaveChanges();

            var usuarioFromRepository = await _usuarioRepository.GetUserByEmailAndPassword(existingUsuario);
            Assert.Equal(existingUsuario.Id, usuarioFromRepository.Id);
            Assert.Equal(existingUsuario.Email, usuarioFromRepository.Email);
            Assert.Equal(existingUsuario.Nome, usuarioFromRepository.Nome);
            Assert.Equal(existingUsuario.Senha, usuarioFromRepository.Senha);
            Assert.Equal(existingUsuario.Eperfil, usuarioFromRepository.Eperfil);
        }

        [Fact]
        public async Task Check_Email_Exist()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            
            var existingUsuario = UsuarioBuilder.WithDefaultValues();
            _context.Usuarios.Add(existingUsuario);        
            _context.SaveChanges();

            var usuarioFromRepository = await _usuarioRepository.CheckEmailExists(existingUsuario.Email);
            Assert.True(usuarioFromRepository);
        }

        [Fact]
        public async Task Check_Email_Not_Exist()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var existingUsuario = UsuarioBuilder.WithDefaultValues();
            _context.Usuarios.Add(existingUsuario);        
            _context.SaveChanges();

            var usuarioFromRepository = await _usuarioRepository.CheckEmailExists("test@gmail.com");
            Assert.False(usuarioFromRepository);
        }
    }
}
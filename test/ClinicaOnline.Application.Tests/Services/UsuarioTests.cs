using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Application.Services;
using ClinicaOnline.Core.Notification;
using ClinicaOnline.Domain.Entities;
using ClinicaOnline.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace ClinicaOnline.Application.Tests.Services
{
    public class UsuarioTests
    {
        private Mock<IUsuarioRepository> _mockUsuarioRepository;
        private Mock<NotificationContext> _mockNotificationContext;
        private Mock<IConfiguration> _mockConfiguration;

        public UsuarioTests()
        {
            _mockUsuarioRepository = new Mock<IUsuarioRepository>();
            _mockNotificationContext = new Mock<NotificationContext>();
            _mockConfiguration = new Mock<IConfiguration>();
        }
        
        [Fact]
        public async Task Should_Return_Success_When_Add_User()
        {
            // Arrange
            var usuarioService = new UsuarioService(_mockUsuarioRepository.Object, _mockConfiguration.Object, _mockNotificationContext.Object);
            var userRequest = new UserRequest()
            {
                Email = "contato@builtcode.com",
                Senha = "123456"
            };
            _mockUsuarioRepository.Setup(x => x.CheckEmailExists(It.IsAny<string>())).ReturnsAsync(false);
            _mockUsuarioRepository.Setup(x => x.Add(It.IsAny<Usuario>())).ReturnsAsync(new Usuario()
            {
                Id = Guid.NewGuid(),
                Email = "contato@builtcode.com",
                Senha = "123456"
            });

            // Act
            var result = await usuarioService.Add(userRequest);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Return_Failure_When_Add_User_With_Existing_Email()
        {
            // Arrange
            var usuarioService = new UsuarioService(_mockUsuarioRepository.Object, _mockConfiguration.Object, _mockNotificationContext.Object);
            var userRequest = new UserRequest()
            {
                Email = "contato@builtcode.com",
                Senha = "123456"
            };
            _mockUsuarioRepository.Setup(x => x.CheckEmailExists(It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await usuarioService.Add(userRequest);

            // Assert
            Assert.Equal(result.Id, Guid.Empty);
        }

        [Fact]
        public async Task Should_Return_Failure_When_Add_User_With_Invalid_Email()
        {
            // Arrange
            var usuarioService = new UsuarioService(_mockUsuarioRepository.Object, _mockConfiguration.Object, _mockNotificationContext.Object);
            var userRequest = new UserRequest()
            {
                Email = "invalidemail",
                Senha = "123456"
            };

            // Act
            var result = await usuarioService.Add(userRequest);

            // Assert
            Assert.Null(result);
        }
    }
}
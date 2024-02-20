using System;
using System.Text.RegularExpressions;
using AutoMapper;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Application.Models.Response;
using ClinicaOnline.Domain.Entities;

namespace ClinicaOnline.Application.Mapper
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<ClinicaOnlineDtoMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }

    public class ClinicaOnlineDtoMapper : Profile
    {
        public ClinicaOnlineDtoMapper()
        {
            CreateMap<UserRequest, Usuario>();
            CreateMap<Usuario, UserResponse>();
            CreateMap<Parceiro, ParceiroUpdateApiKeyResponse>();
            CreateMap<MedicoRequest, Medico>();
            CreateMap<Medico, MedicoResponse>();
            CreateMap<PacienteRequest, Paciente>();
            CreateMap<Paciente, PacienteResponse>();
        }
    }
}
using System;
using AutoMapper;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Application.Models.Response;
using ClinicaOnline.Core.Entities;

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
            CreateMap<UserRequest, Usuario>().ReverseMap();
            CreateMap<Usuario, UserResponse>().ReverseMap();
        }
    }
}
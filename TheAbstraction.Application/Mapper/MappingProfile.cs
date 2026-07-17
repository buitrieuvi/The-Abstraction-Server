using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAbstraction.Application.DTOs;

namespace TheAbstraction.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<(string id, string fullName, string userName, string email), UserResponseDTO>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.id))
            .ForMember(d => d.FullName, o => o.MapFrom(s => s.fullName))
            .ForMember(d => d.UserName, o => o.MapFrom(s => s.userName))
            .ForMember(d => d.Email, o => o.MapFrom(s => s.email));
        }
    }
}

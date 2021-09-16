using AutoMapper;
using FacuTheRock.Talks.Net.EFTesting.API.Dtos;
using FacuTheRock.Talks.Net.EFTesting.Database.Models;

namespace FacuTheRock.Talks.Net.EFTesting.API.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TeamDto, Team>().ReverseMap();
            CreateMap<PlayerDto, Player>().ReverseMap();
        }
    }
}

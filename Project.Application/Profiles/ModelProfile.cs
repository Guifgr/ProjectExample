using AutoMapper;
using BRW.Domain.Entities;
using Project.Application.DTOs;

namespace Project.Application.Profiles
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            CreateMap<User, UserGetDto>();
            CreateMap<UserCreateDto, User>();
        }
        
    }
}
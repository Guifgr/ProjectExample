using AutoMapper;
using Project.Application.DTOs;
using Project.Domain.Entities;

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
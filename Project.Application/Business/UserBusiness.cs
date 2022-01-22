using AutoMapper;
using Project.Application.DTOs;
using Project.Application.IBusiness;
using Project.Domain.Entities;
using Project.Domain.Exceptions;
using Project.Domain.IRepository;

namespace Project.Application.Business;

public class UserBusiness : IUserBusiness
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserBusiness(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<UserGetDto>> GetAllUsers()
    {
        var userList = await _userRepository.GetAllUsers();

        if (!userList.Any())
        {
            //Caso a lista não tenha nenhum retorne erro de No content https://http.cat/204    
            throw new NoContentException("Nenhum usuário encontrado");
        }

        return _mapper.Map<List<UserGetDto>>(userList);
    }

    public async Task<UserGetDto> GetUserByGuid(Guid guid)
    {
        var user = await _userRepository.GetUserByGuid(guid);

        if (user == default)
        {
            //Caso não tenha nenhum retorne erro de No content https://http.cat/204    
            throw new NoContentException("Nenhum usuário encontrado");
        }

        return _mapper.Map<UserGetDto>(user);
    }

    public async Task<bool> UserEmailExists(string email)
    {
        var user = await _userRepository.UserEmailExists(email);

        return user != default;
    }

    public async Task<UserGetDto> CreateUser(UserCreateDto userDto)
    {
        var userEmailExists = await UserEmailExists(userDto.Email);
        if (userEmailExists)
        {
            throw new BadRequestException("Email Já cadastrado em nossa base");
        }
        
        var user = _mapper.Map<User>(userDto);
        var userGetDto = await _userRepository.CreateUser(user);
        return _mapper.Map<UserGetDto>(userGetDto);
    }
}
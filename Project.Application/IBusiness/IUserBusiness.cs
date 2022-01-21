using Project.Application.DTOs;

namespace Project.Application.IBusiness;

public interface IUserBusiness
{
    Task<List<UserGetDto>> GetAllUsers();
    Task<UserGetDto> GetUserByGuid(Guid guid);
    Task<UserGetDto> CreateUser(UserCreateDto userDto);
}
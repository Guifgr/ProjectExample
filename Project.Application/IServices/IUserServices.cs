using Project.Application.DTOs;

namespace Project.Application.IServices;

public interface IUserServices
{
    Task<List<UserGetDto>> GetAllUsers();
    Task<UserGetDto> GetUserByGuid(Guid guid);
    Task<UserGetDto> CreateUser(UserCreateDto userDto);
    Task DeleteUser(Guid guid);
}
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs;
using Project.Application.IBusiness;

namespace Project.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersControllers : ControllerBase
{
    private readonly IUserBusiness _userBusiness;

    public UsersControllers(IUserBusiness userBusiness)
    {
        _userBusiness = userBusiness;
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<UserGetDto>> GetUsers()
    {
        return Ok(await _userBusiness.GetAllUsers());
    }

    [HttpGet("[action]/{id:int}", Name = "GetUserById")]
    public async Task<ActionResult<UserGetDto>> GetUserByGuid(Guid guid)
    {
        return await _userBusiness.GetUserByGuid(guid);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult<UserGetDto>> CreateUser(UserCreateDto userDto)
    {
        var userGetDto = await _userBusiness.CreateUser(userDto);
        return CreatedAtRoute(nameof(GetUserByGuid), new {userGetDto.Guid}, userGetDto);
    }
}
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs;
using Project.Application.IBusiness;

namespace Project.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UsersControllers : ControllerBase
{
    private readonly IUserBusiness _userBusiness;

    public UsersControllers(IUserBusiness userBusiness)
    {
        _userBusiness = userBusiness;
    }

    [HttpGet]
    public async Task<ActionResult<UserGetDto>> GetUsers()
    {
        return Ok(await _userBusiness.GetAllUsers());
    }

    [HttpGet("{guid:guid}", Name = "GetUserByGuid")]
    public async Task<ActionResult<UserGetDto>> GetUserByGuid(Guid guid)
    {
        return await _userBusiness.GetUserByGuid(guid);
    }

    [HttpPost]
    public async Task<ActionResult<UserGetDto>> CreateUser(UserCreateDto userDto)
    {
        var userGetDto = await _userBusiness.CreateUser(userDto);
        return CreatedAtRoute(nameof(GetUserByGuid), new {userGetDto.Guid}, userGetDto);
    }
}
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs;
using Project.Application.IServices;

namespace Project.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UsersControllers : ControllerBase
{
    private readonly IUserServices _userServices;

    public UsersControllers(IUserServices userServices)
    {
        _userServices = userServices;
    }

    [HttpGet]
    public async Task<ActionResult<UserGetDto>> GetUsers()
    {
        return Ok(await _userServices.GetAllUsers());
    }

    [HttpGet("{guid:guid}", Name = "GetUserByGuid")]
    public async Task<ActionResult<UserGetDto>> GetUserByGuid(Guid guid)
    {
        return await _userServices.GetUserByGuid(guid);
    }

    [HttpPost]
    public async Task<ActionResult<UserGetDto>> CreateUser(UserCreateDto userDto)
    {
        var userGetDto = await _userServices.CreateUser(userDto);
        return CreatedAtRoute(nameof(GetUserByGuid), new {userGetDto.Guid}, userGetDto);
    }

    [HttpGet("{guid:guid}")]
    public async Task<ActionResult> DeleteUser(Guid guid)
    {
        await _userServices.DeleteUser(guid);
        return NoContent();
    }
}
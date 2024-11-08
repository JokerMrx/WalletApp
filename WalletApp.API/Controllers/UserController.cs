using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WalletApp.API.Contracts.User;
using WalletApp.Core.Models;
using WalletApp.Core.Models.DTOs;
using WalletApp.Core.Repositories;

namespace WalletApp.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : Controller
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly ResponseDto _responseDto = new();

    public UserController(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest registerUserRequest)
    {
        try
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = registerUserRequest.FirstName,
                LastName = registerUserRequest.LastName,
                Email = registerUserRequest.Email,
            };

            var createdUser = await _userRepository.CreateAsync(user);
            _responseDto.Result = createdUser;

            return Ok(_responseDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            _responseDto.Message = ex.Message;
            _responseDto.IsSuccess = false;

            return BadRequest(_responseDto);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);
            
            if (user == null)
            {
                return NotFound("User not found");
            }
            
            _responseDto.Result = user;

            return Ok(_responseDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _responseDto.Message = e.Message;

            return BadRequest(_responseDto);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var user = await _userRepository.DeleteAsync(id);
            _responseDto.Result = user;

            return Ok(_responseDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _responseDto.Message = e.Message;

            return BadRequest(_responseDto);
        }
    }
}
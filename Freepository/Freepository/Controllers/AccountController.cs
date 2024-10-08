using Freepository.DTO_s;
using Freepository.Models;
using Freepository.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Freepository.Controllers;
[Route("api/account")]
[ApiController]


public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var user = await _userManager.FindByNameAsync(loginDto.UserName.ToLower());
        // var userId = await _userManager.GetUserIdAsync(user);
        // var userClaims = await _userManager.GetClaimsAsync(user);
        // var userId = await _userManager.GetUserId(userClaims[0]);
        if (user == null)
            return Unauthorized("Invalid Username!");
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded)
            return Unauthorized("Username not found or password incorrect");
        
        return Ok(new CreateUserDTO
            
        {   
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Token = _tokenService.CreateToken(user)
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validar que el correo electrónico termina en @factoriaf5.org
            if (!registerDto.Email.EndsWith("@factoriaf5.org", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("El correo electrónico debe terminar en @factoriaf5.org");
            }

            var appUser = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email
            };

            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (roleResult.Succeeded)
                {
                    return Ok(
                        new CreateUserDTO()
                        {
                            Id = appUser.Id,
                            UserName = appUser.UserName,
                            Email = appUser.Email,
                            Token = _tokenService.CreateToken(appUser)
                        }
                        );
                }
                else
                {
                    return StatusCode(500, roleResult.Errors);
                }
            }
            else
            {
                return StatusCode(500, createdUser.Errors);
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }
}

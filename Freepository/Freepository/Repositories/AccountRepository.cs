using System.Security.Claims;
using Freepository.DTO_s;
using Microsoft.AspNetCore.Identity;
using Freepository.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Freepository.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountRepository(SignInManager<User> signInManager, UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<object> Login(LoginDTO model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return new { message = "No autorizado" };
        }

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: false);
        {
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return BuildLoginResponse(user);
            }
            else
            {
                {
                    return new { message = "No autorizado" };
                }
            }
        }
    }

    public async Task<object> Register(RegisterDTO model)
    {
        var user = new User
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = string.IsNullOrWhiteSpace(model.FirstName) ? "" : model.FirstName,
            LastName = string.IsNullOrWhiteSpace(model.LastName) ? "" : model.LastName
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "user");
            return new { message = "Usuario registrado correctamente" };
        }
        else
        {
            var errors = result.Errors.Select(e => e.Description);
            return new { message = "Fallo al registrar el usuario", errors };
        } 
    }

    private async Task SignInUserAsync(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults. AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
        };

        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity), authProperties);
    }
    
    
    

    private object BuildLoginResponse(User user)
    {
        return new
        {
            message = "Inicio de sesión exitoso",
            userInfo = new
            {
                userId = user.Id,
                userName = user.UserName,
                email = user.Email,
                firstName = user.FirstName,
                lastName = user.LastName,


            }
        };
    }
}
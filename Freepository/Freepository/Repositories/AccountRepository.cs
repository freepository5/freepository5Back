using Freepository.DTO_s;
using Microsoft.AspNetCore.Identity;
using Freepository.Models;

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
using Freepository.DTO_s;

namespace Freepository.Repositories;

public interface IAccountRepository
{
    Task<object> Login(LoginDTO model);
    Task<object> Register(RegisterDTO model);
    
}
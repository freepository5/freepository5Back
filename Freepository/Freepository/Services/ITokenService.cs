using Freepository.Models;

namespace Freepository.Repositories;

public interface ITokenService
{
    string CreateToken(User user);
}
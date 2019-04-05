using Token.Domain.Entities;

namespace Token.Application.Interfaces
{
    public interface IAuthenticationService
    {
        User Authenticate(string token);
    }
}

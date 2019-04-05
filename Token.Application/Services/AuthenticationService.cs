using Token.Application.Interfaces;
using Token.Domain.Entities;

namespace Token.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string Token = "76102da9-6747-4971-8af5-2c174c601437";

        public User Authenticate(string token)
        {
            if (token == Token)
            {
                return new User
                {
                    Token = Token,
                    Name = "Lucas"
                };
            }

            return null;

            //Vai no repositório
        }
    }
}

using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using Token.Application.Interfaces;
using Token.Domain.Entities;

namespace Token.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationMiddleware(RequestDelegate next, IAuthenticationService authenticationService)
        {
            _next = next;
            _authenticationService = authenticationService;
        }

        public Task Invoke(HttpContext context)
        {
            const string authenticationHeaderName = "Authorization";
            const string tokenSchemeName = "Bearer";


            var result = context.Request.Headers.TryGetValue(authenticationHeaderName, out var header);

            if (result)
            {
                result = AuthenticationHeaderValue.TryParse(header, out var authenticationHeader);

                if (result)
                {
                    var token = authenticationHeader.Parameter;
                    if (token != null && authenticationHeader.Scheme == tokenSchemeName)
                    {
                        var user = _authenticationService.Authenticate(token);

                        if (user != null)
                        {
                            var identity = new ClaimsIdentity();

                            var nameClaim = new Claim(nameof(User.Name), user.Name);
                            var tokenClaim = new Claim(nameof(User.Token), user.Token);

                            identity.AddClaim(nameClaim);
                            identity.AddClaim(tokenClaim);

                            var principal = new ClaimsPrincipal();
                            principal.AddIdentity(identity);


                            context.User = new ClaimsPrincipal(principal);

                            return _next(context);
                        }
                    }
                }
            }

            throw new AuthenticationException();
        }
    }
}

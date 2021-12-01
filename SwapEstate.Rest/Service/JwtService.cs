using System;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SwapEstate.Auth.Impl;
using SevenSem.Identity.Abstract;
using SevenSem.Identity.Abstract.Model;
using SevenSem.Rest.Constants;
using SwapEstate.Auth.Abstract;

namespace SevenSem.Rest.Service
{
    internal class JwtService : IJwtService
    {
        private readonly IConfiguration _config;
        private readonly IJwtTokenFactory _tokenFactory;

        private readonly Lazy<TokenValidationParameters> _tokenParams;

        public JwtService(
            IConfiguration config,
            IJwtTokenFactory tokenFactory)
        {
            _config = config;
            _tokenFactory = tokenFactory;
            _tokenParams = new Lazy<TokenValidationParameters>(GetTokenParams, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public string GetToken(Role[] roles)
        {
            return _tokenFactory.Get(
                roles.Select(Convert).ToArray(),
                TimeSpan.Parse(_config["Jwt:TokenExpiry"]),
                _tokenParams.Value);
        }

        private TokenValidationParameters GetTokenParams()
        {
            return new TokenValidationParameters
            {
                ValidIssuer = _config["Jwt:ValidIssuer"],
                ValidAudience = _config["Jwt:ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT_KEY"])),
            };
        }

        private string Convert(Role role)
        {
            return role switch
            {
                Role.Landlord => RoleConstant.Landlord,
                Role.Tenant => RoleConstant.Tenant,
                _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
            };
        }
    }
}
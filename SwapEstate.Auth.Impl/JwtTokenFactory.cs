using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using SwapEstate.Auth.Abstract;

namespace SwapEstate.Auth.Impl
{
    internal class JwtTokenFactory : IJwtTokenFactory
    {
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public JwtTokenFactory()
        {
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public string Get(string[] roles, TimeSpan expiry, TokenValidationParameters tokenParams)
        {
            var claims = roles.Select(r => new Claim(ClaimTypes.Role, r));

            var token = new JwtSecurityToken(
                tokenParams.ValidIssuer,
                tokenParams.ValidAudience,
                claims,
                expires: DateTime.Now.Add(expiry),
                signingCredentials: new SigningCredentials(tokenParams.IssuerSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return _tokenHandler.WriteToken(token);
        }
    }
}
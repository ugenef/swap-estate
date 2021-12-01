using System;
using Microsoft.IdentityModel.Tokens;

namespace SwapEstate.Auth.Abstract
{
    public interface IJwtTokenFactory
    {
        string Get(string[] roles, TimeSpan expiry, TokenValidationParameters tokenParams);
    }
}
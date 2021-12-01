using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SevenSem.Identity.Abstract;
using SevenSem.Identity.Abstract.Model;
using SevenSem.Rest.Constants;
using SevenSem.Rest.Dto;
using SevenSem.Rest.Service;

namespace SevenSem.Rest.Controllers
{
    [Route(ApiVersion.V1 + "/user")]
    public class AuthController : Controller
    {
        private readonly IJwtService _jwtService;
        private readonly IIdentityService _identity;

        public AuthController(
            IJwtService jwtService,
            IIdentityService identity)
        {
            _identity = identity;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Method creates an user with requested roles.
        /// </summary>
        [HttpPost, Route("create")]
        public Task CreateAsync([FromBody] CreateDto createDto)
        {
            var creds = Convert(createDto.Creds);
            var roles = createDto.Roles.Select(Convert).ToArray();
            return _identity.CreateAsync(creds, roles);
        }

        /// <summary>
        /// Method signs user in and generates a JWT-token
        /// </summary>
        [HttpPost, Route("signin")]
        public async Task<string> SingInAsync([FromBody] CredsDto credsDto)
        {
            var creds = Convert(credsDto);
            var result = await _identity.SingInAsync(creds).ConfigureAwait(false);
            return _jwtService.GetToken(result.Roles);
        }

        private Creds Convert(CredsDto credsDto)
        {
            return new Creds
            {
                Login = credsDto.Login,
                Password = credsDto.Password,
            };
        }

        private Role Convert(RoleDto roleDto)
        {
            return roleDto switch
            {
                RoleDto.Landlord => Role.Landlord,
                RoleDto.Tenant => Role.Tenant,
                _ => throw new ArgumentOutOfRangeException(nameof(roleDto), roleDto, null)
            };
        }
    }
}
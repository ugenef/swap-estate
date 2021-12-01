using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SevenSem.Identity.Abstract;
using SevenSem.Identity.Abstract.Model;

namespace SwapEstate.Identity.Impl
{
    internal class IdentityService : IIdentityService
    {
        private readonly IServiceScopeFactory _scope;

        public IdentityService( IServiceScopeFactory scope)
        {
            _scope = scope;
        }

        public async Task CreateAsync(Creds creds, Role[] roles)
        { 
            var user = new IdentityUser {UserName = creds.Login};
            using var scope = _scope.CreateScope();
            var userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await ExecuteOrThrowAsync(userManager.CreateAsync(user, creds.Password)).ConfigureAwait(false);
            await ExecuteOrThrowAsync(CreateRolesIdempotentAsync(roleManager, roles.Select(Convert))).ConfigureAwait(false);
            await ExecuteOrThrowAsync(userManager.AddToRolesAsync(user, roles.Select(Convert))).ConfigureAwait(false);
            tran.Complete();
        }

        public async Task<LoginResult> SingInAsync(Creds creds)
        {
            using var scope = _scope.CreateScope();
            var signInManager = scope.ServiceProvider.GetService<SignInManager<IdentityUser>>();
            var userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
            var user = await userManager.FindByNameAsync(creds.Login).ConfigureAwait(false);
            var result = await signInManager.PasswordSignInAsync(user, creds.Password, false, false).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                return new LoginResult{Succeeded = false};
            }

            var roles = await userManager.GetRolesAsync(user).ConfigureAwait(false);
            return new LoginResult{ Succeeded = true, Roles = roles.Select(Convert).ToArray()};
        }

        private async Task<IdentityResult> CreateRolesIdempotentAsync(RoleManager<IdentityRole> roleManager, IEnumerable<string> roles)
        {
            foreach (var role in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role).ConfigureAwait(false);
                if (!roleExists)
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(role)).ConfigureAwait(false);
                    if (!result.Succeeded)
                    {
                        return result;
                    }
                }
            }

            return IdentityResult.Success;
        }
        private async Task ExecuteOrThrowAsync(Task<IdentityResult> actor)
        {
            var result = await actor.ConfigureAwait(false);
            if (!result.Succeeded)
            {
                throw new Exception($"Errors occured:\n{string.Join('\n', result.Errors)}");
            }
        }

        private const string Landlord = "Landlord";
        private const string Tenant = "Tenant";

        private string Convert(Role role)
        {
            return role switch
            {
                Role.Landlord => Landlord,
                Role.Tenant => Tenant,
                _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
            };
        }

        private Role Convert(string role)
        {
            return role switch
            {
                Landlord => Role.Landlord,
                Tenant => Role.Tenant,
                _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
            };
        }
    }
}
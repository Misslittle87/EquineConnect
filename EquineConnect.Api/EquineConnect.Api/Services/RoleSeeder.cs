using EquineConnect.Core.Constants;
using Microsoft.AspNetCore.Identity;

namespace EquineConnect.Api.Services
{
    public class RoleSeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleSeeder(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedRolesAsync()
        {
            var roles = new[]
            {
                RoleConstants.Superuser,
                RoleConstants.Admin,
                RoleConstants.StableStaff,
                RoleConstants.Boarder,
                RoleConstants.CoRider
            };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}

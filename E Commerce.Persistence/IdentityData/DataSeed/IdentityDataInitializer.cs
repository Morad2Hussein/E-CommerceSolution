

using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace E_Commerce.Persistence.IdentityData.DataSeed
{
    public class IdentityDataInitializer : IDataInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataInitializer> _logger;
        public IdentityDataInitializer(
                                        UserManager<ApplicationUser> userManager,
                                        RoleManager<IdentityRole> roleManager,
                                        ILogger<IdentityDataInitializer> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        public async Task InitializeAsync()
        {
            try
            {
                // check if i have roles data or not 
                //if i have users data or not
                if (!_roleManager.Roles.Any())
                {
                    // seed roles
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    // seed 2 users 
                    var User01 = new ApplicationUser
                    {
                        DisplayName = "Ahmed Mohamed",
                        UserName = "AhmedMohamed",
                        Email = "AhmedMohamed@example.com",
                        PhoneNumber = "01282265359",
                    };
                    var User02 = new ApplicationUser
                    {
                        DisplayName = "Salma Mohamed",
                        UserName = "SalmaMohamed",
                        Email = "SalmaMohamed@example.com",
                        PhoneNumber = "0128265359",
                    };
                    // create users
                    await _userManager.CreateAsync(User01, "P@ssw0rd");
                    await _userManager.CreateAsync(User02, "P@ssw0rd");
                    // make User01 as Admin and User02 as SuperAdmin
                    await _userManager.AddToRoleAsync(User01, "Admin");
                    await _userManager.AddToRoleAsync(User02, "SuperAdmin");

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding identity data.");
            }
        }
    }
}

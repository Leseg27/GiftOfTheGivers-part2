using GiftOfTheGivers.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace GiftOfTheGivers.Web.Data;

public static class SeedData
{
    public const string EmployeeRole = "Employee";
    public const string DonorRole = "Donor";

    public static async Task InitializeAsync(IServiceProvider sp)
    {
        var roleMgr = sp.GetRequiredService<RoleManager<IdentityRole>>();
        var userMgr = sp.GetRequiredService<UserManager<ApplicationUser>>();

        foreach (var role in new[] { EmployeeRole, DonorRole })
        {
            if (!await roleMgr.RoleExistsAsync(role))
                await roleMgr.CreateAsync(new IdentityRole(role));
        }

        await EnsureUser(userMgr, "employee@giftofthegivers.org",
            "Employee@123", EmployeeRole, "Test Employee");

        await EnsureUser(userMgr, "donor@giftofthegivers.org",
            "Donor@123", DonorRole, "Test Donor");
    }

    private static async Task EnsureUser(UserManager<ApplicationUser> mgr,
        string email, string password, string role, string fullName)
    {
        var user = await mgr.FindByEmailAsync(email);
        if (user is null)
        {
            user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = fullName,
                EmailConfirmed = true
            };
            var result = await mgr.CreateAsync(user, password);
            if (result.Succeeded)
                await mgr.AddToRoleAsync(user, role);
        }
    }
}
using Microsoft.AspNetCore.Identity;
using outreach3.Data.Ministries;

namespace outreach3.Data
{
    public class IdentitySeedData
    {

        public IdentitySeedData()
        {

        }

        public static async Task Initialize(ApplicationDbContext context,
            Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager,
            Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();       

            string adminRole = "Admin";
            string churchAdmin = "ChurchAdmin";
            string memberRole = "User";
            string adminUserEmail = "cgbrewer2@comcast.net";
            string adminPhone = "904) 514-5450";

            //Create Roles
            if (await roleManager.FindByNameAsync(adminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }
            if (await roleManager.FindByNameAsync(memberRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(churchAdmin));
            }

            //Create the admin user
            if (await userManager.FindByEmailAsync(adminUserEmail) == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = "Charlie",
                    Email = adminUserEmail,
                    PhoneNumber = adminPhone,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                };

                var initialMember = new Member
                {
                    Email = adminUserEmail,
                    Name = "Charlie",
                    Approved = true,
                    ChurchId = 0
                };
                context.Members.Add(initialMember);
                await context.SaveChangesAsync();

                var result = await userManager.CreateAsync(adminUser);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(adminUser, "Tlimlamsps@&ok100");
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                }
            }


          
        }
    }
}

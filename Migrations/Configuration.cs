namespace WebApplication1.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApplication1.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication1.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WebApplication1.Models.ApplicationDbContext context)
        {
            // Create Default Roles
            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);

            var adminRole = new IdentityRole { Name = "Admin" };
            var student = new IdentityRole { Name = "Student" };

            if (!roleManager.RoleExists(adminRole.Name))
                roleManager.Create(adminRole);

            if (!roleManager.RoleExists(student.Name))
                roleManager.Create(student);

            // Initialise Default Admin User
            ApplicationUser adminuser = new ApplicationUser
            {
                UserName = "admin@ctucareer.co.za",
                Email = "admin@ctucareer.co.za"
            };

            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);

            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

            var results = userManager.Create(adminuser, "Admin1");

            if (results.Succeeded)
            {
                userManager.AddToRole(adminuser.Id, adminRole.Name);
            }

            // initialize tables in context
            base.Seed(context);

        }
    }
}

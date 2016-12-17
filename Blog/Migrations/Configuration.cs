namespace Blog.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Blog.Models.BlogDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BlogDbContext context)
        {
            if (!context.Roles.Any())
            {
                this.CreateRole(context, "Admin");
                this.CreateRole(context, "User");
            }
            if (!context.Users.Any())
            {
                this.CreateUser(context, "admin@admin.bg","Admin","321");
                this.SetRoleToUser(context, "admin@admin.bg", "Admin");
            }
        }
        private void CreateRole (BlogDbContext context,string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var result = roleManager.Create(new IdentityRole(roleName));
            if(!result.Succeeded)
            {
                throw new Exception(string.Join(";", result.Errors));
            }
        }
        private void CreateUser(BlogDbContext context, string email, string fullName, string password)
        {
            var userMenager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            userMenager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireDigit = false,
                RequireLowercase = false,
                RequireNonLetterOrDigit = false,
            };
            var admin = new ApplicationUser
            {
                UserName = email,
                FullName = fullName,
                Email = email,
            };
            var result = userMenager.Create(admin, password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(";", result.Errors));
            }
        }
        private void SetRoleToUser(BlogDbContext context, string email, string role)
        {
            var userMenager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = context.Users.Where(u => u.Email == email).First();
            var result = userMenager.AddToRoles(user.Id, role);

            if(!result.Succeeded)
            {
                throw new Exception(string.Join(";", result.Errors));
            }
        }
    }
}

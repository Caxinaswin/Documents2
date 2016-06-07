namespace CrowdTouring.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CrowdTouring.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CrowdTouring.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Cliente"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Cliente" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Resolvedor"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Resolvedor" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Avaliador"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Avaliador" };

                manager.Create(role);
            }

            context.Tags.AddOrUpdate(x => x.Id,
        new Tag() { Id = 1, NomeTag = "Restauração", cor = "Red" },
        new Tag() { Id = 2, NomeTag = "Hotelaria", cor = "Orange" },
        new Tag() { Id = 3, NomeTag = "Praias", cor = "Yellow" },
        new Tag() { Id = 4, NomeTag = "Transportes", cor = "Green" },
        new Tag() { Id = 5, NomeTag = "Zonas Urbanas", cor = "Gray" },
        new Tag() { Id = 6, NomeTag = "Zonas Históricas", cor = "Brown" },
         new Tag() { Id = 7, NomeTag = "Lazer", cor = "Blue" }
        );
        }
    }
}

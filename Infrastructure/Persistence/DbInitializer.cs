using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;
        private readonly StoreIdentityDbContext _identityContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            StoreDbContext context ,
            StoreIdentityDbContext identityContext,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _context = context;
            _identityContext = identityContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializAsync()
        {
            // Create Database If it does not exists && Apply To Any Pending   Migrations
            if (_context.Database.GetPendingMigrations().Any())
            {
               await _context.Database.MigrateAsync();
            }


            // Data Seeding

            // Seeding ProductTypes From Json Files

            if (!_context.productTypes.Any())
            {
                // 1. Read All Data From types Json File as String
                var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");

                // 2. Convert String To C# Object [List<ProductType>]
                var type =  JsonSerializer.Deserialize<List<ProductType>>(typesData);


                // 3. Add List<ProductType> To Database
                if (type is not null && type.Any())
                {
                    await _context.productTypes.AddRangeAsync(type);
                    await _context.SaveChangesAsync();

                }



            }




            // Seeding ProductBrands From Json Files
            if (!_context.productBrands.Any())
            {
                // 1. Read All Data From brands Json File as String
                var brandData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");

                // 2. Convert String To C# Object [List<ProductType>]
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);


                // 3. Add List<ProductType> To Database
                if (brands is not null && brands.Any())
                {
                    await _context.productBrands.AddRangeAsync(brands);
                    await _context.SaveChangesAsync();

                }

            }


            // Seeding Products  From Json Files

            if (!_context.Products.Any())
            {
                // 1. Read All Data From products Json File as String
                var productData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");

                // 2. Convert String To C# Object [List<ProductType>]
                var products = JsonSerializer.Deserialize<List<Product>>(productData);


                // 3. Add List<ProductType> To Database
                if (products is not null && products.Any())
                {
                    await _context.Products.AddRangeAsync(products);
                    await _context.SaveChangesAsync();

                }

            }


            //..\Infrastructure\Persistence\Data\Seeding\types.json
            //..\Infrastructure\Persistence\Data\Seeding\brands.json

        }

        public async Task InitializIdentityAsync()
        {
            // Create Database If it does not exists && Apply To Any Pending   Migrations

            if (_identityContext.Database.GetPendingMigrations().Any())
            {
               await _identityContext.Database.MigrateAsync();
            }

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Admin" 
                
                });
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = "superAdmin"
                });

            }




            // Seeding 
            if (!_userManager.Users.Any())
            {
                var superAdminUser = new AppUser()
                {
                    DisplayName = "Super Admin",
                    Email = "SuperAdmin@gmail.com",
                    UserName = "superadmin",
                    PhoneNumber = "01000000000",
                };

                var adminUser = new AppUser()
                {
                    DisplayName = " Admin",
                    Email = "Admin@gmail.com",
                    UserName = "admin",
                    PhoneNumber = "01000000000",
                };
                await _userManager.CreateAsync(superAdminUser, "P@ssW0rd");
                await _userManager.CreateAsync(adminUser, "P@ssW0rd");

                await _userManager.AddToRoleAsync(superAdminUser, "superAdmin");
                await _userManager.AddToRoleAsync(adminUser, "Admin");



            }



        }
    }
}

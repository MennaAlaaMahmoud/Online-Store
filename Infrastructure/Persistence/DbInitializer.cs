using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;

        public DbInitializer(StoreDbContext context )
        {
            _context = context;
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



    }
}

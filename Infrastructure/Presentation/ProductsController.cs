using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;

namespace Presentation
{
    // Api
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager serviceManager) : ControllerBase
    {
        // sort : nameasc [default]
        // sort : namedesc 
        // sort : pricDesc
        // sort : priceAsc


        [HttpGet] // endpoint : Get: /api/products
        public async Task<IActionResult> GetAllProducts(int? brandId , int? typeId , string? sort)
        {
            var result = await serviceManager.ProductService.GetAllProductsAsync(brandId,typeId,sort );
            if (result is null) return BadRequest(); // 400
            return Ok(result); // 200                                       

        }

        [HttpGet("{id}")] // Get /api/products/{id}
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await serviceManager.ProductService.GetProductByIdAsync(id);
            if (result is null) return NotFound(); // 404
            return Ok(result); // 200
        }

        // TODO : Get All Brands

        [HttpGet("brands")] // Get /api/products/brands
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await serviceManager.ProductService.GetAllBrandsAsync();
            if (result is null) return BadRequest(); // 400
            return Ok(result); // 200
        }



        // TODO : Get All Types

        [HttpGet("types")] // Get /api/products/types
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await serviceManager.ProductService.GetAllTypesAsync();
            if (result is null) return BadRequest(); // 400
            return Ok(result); // 200
        }




    }
}












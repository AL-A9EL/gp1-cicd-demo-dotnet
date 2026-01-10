using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocalMarket.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly List<ProductDto> Products = new()
        {
            new ProductDto(1, "iPhone 15 Pro", 4999.00m, "Apple", "Electronics"),
            new ProductDto(2, "AirPods Pro", 999.00m, "Apple", "Accessories"),
            new ProductDto(3, "Gaming Laptop", 6500.00m, "ASUS", "Electronics"),
            new ProductDto(4, "Coffee Beans 1kg", 85.00m, "Lavazza", "Food"),
        };

        // GET: /api/products
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Products);
        }

        // GET: /api/products/1
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound(new { message = "Product not found" });

            return Ok(product);
        }

        // GET: /api/products/search?name=iphone
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest(new { message = "name query is required" });

            var results = Products
                .Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(results);
        }
    }

    public record ProductDto(
        int Id,
        string Name,
        decimal Price,
        string Brand,
        string Category
    );
}

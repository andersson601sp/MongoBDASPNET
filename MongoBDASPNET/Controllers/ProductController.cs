using application.Entities;
using application.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoBDASPNET.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductService _productService;

        public ProductController(ILogger<ProductController> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }


        [HttpGet]
        public async Task<List<Product>> Get()
        {
           return await _productService.GetAsync();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var prod = await _productService.GetAsync(id);

            if (prod is null)
            {
                return NotFound();
            }

            return prod;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product newProd)
        {
            await _productService.CreateAsync(newProd);

            return CreatedAtAction(nameof(Get), new { id = newProd.Id }, newProd);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Product updatedProd)
        {
            var prod = await _productService.GetAsync(id);

            if (prod is null)
            {
                return NotFound();
            }

            updatedProd.Id = prod.Id;

            await _productService.UpdateAsync(id, updatedProd);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var prod = await _productService.GetAsync(id);

            if (prod is null)
            {
                return NotFound();
            }

            await _productService.RemoveAsync(id);

            return NoContent();
        }
    }
}

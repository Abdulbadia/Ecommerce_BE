using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceProject.models;
using EcommerceProject.DTO;

namespace EcommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly EcommerceContext _context;

        public ProductController(EcommerceContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        //// GET: api/Product/5
        //[HttpGet("{id:int}", Name = "ProductDetialsRoute")]
        //public ActionResult GetProduct(int id)
        //{
        //    var product =_context.Products.Include(ww => ww.Category).Include(ee => ee.Brand).FirstOrDefault(e => e.ID == id);

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(product);
        //}

        // get product with category name and brand name
        // GET: api/Product/5


        // api/product/5
        [HttpGet("{id:int}", Name = "ProductDetialsRoute")]
        public ActionResult GetProduct(int id)
        {
            Product product = _context.Products.Include(ww => ww.Category).Include(ee => ee.Brand).FirstOrDefault(e => e.ID == id);
            ProductWithCategoryAndBrandNameDto productDto = new ProductWithCategoryAndBrandNameDto();
            productDto.ProductName = product.Name;
            productDto.ProductId = product.ID;
            productDto.CategoryName = product.Category.CatName;
            productDto.BrandName = product.Brand.BName;
            productDto.BrandId = product.Brand.id;
            productDto.CategoryId = product.Category.id;
            productDto.Availability = product.Availability;
            productDto.Description = product.Description;
            productDto.image = product.image;
            productDto.discountPercentage = product.discountPercentage;

            return Ok(productDto);

        }


        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute]int id,[FromBody] Product product)
        {
            if (id != product.ID)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

       
        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                string url = Url.Link("ProductDetialsRoute", new { id = product.ID });

                return Created(url, product);
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}

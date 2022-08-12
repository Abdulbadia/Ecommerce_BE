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
        public ActionResult GetProducts()
        {
            var products = from x in _context.Products
                           select new ProductDTO
                           {
                               ID = x.ID,
                               Name = x.Name,
                               Description = x.Description,
                               image = x.image,
                               Price = x.Price,
                               Availability = x.Availability,
                               discountPercentage = x.discountPercentage,
                               Category = x.Category.CatName,
                               Brand = x.Brand.BName

                           };
            return Ok(products);
        }


        // api/product/5
        [HttpGet("{id:int}", Name = "ProductDetialsRoute")]
        public ActionResult GetProduct(int id)
        {
            var product = _context.Products.Include(ww => ww.Category).Include(b => b.Brand).Select(x => new ProductDTO()
            {
                ID = x.ID,
                Name = x.Name,
                Description = x.Description,
                image = x.image,
                Price = x.Price,
                Availability = x.Availability,
                discountPercentage = x.discountPercentage,
                Category = x.Category.CatName,
                Brand = x.Brand.BName

            }).SingleOrDefault(b=> b.ID == id);
            if (product == null)
            {
                return NotFound();
            }
          

            return Ok(product);

        }


        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] Product product)
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

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
    public class CategoryController : ControllerBase
    {
        private readonly EcommerceContext _context;

        public CategoryController(EcommerceContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public  ActionResult GetCategories()
        {
            List<Category> categories = _context.Categories.Include(p => p.Products).ToList();

            return Ok(categories);
        }

        // GET: api/Categories/5
        [HttpGet("{id:int}", Name = "CategoryDetialsRoute")]
        public ActionResult GetCategory(int id)
        {
            var category = _context.Categories.Include(p => p.Products).FirstOrDefault(c => c.id == id);
           
            if (category == null)
            {
                return NotFound();
            }
            CategoryWithProductsDTO CatsDTO = new CategoryWithProductsDTO();

            CatsDTO.ID = category.id;
            CatsDTO.Name = category.CatName;
            foreach (var product in category.Products)
            {
                CatsDTO.products.Add(new ProductBrandAndCategoryDto
                {
                    ID = product.ID,
                    Name = product.Name,
                    image = product.image,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Availability = product.Availability,
                    discountPercentage = product.discountPercentage,
                    
                });
           
            }
            return Ok(CatsDTO);
        }


        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory([FromRoute] int id, [FromBody] Category category)
        {
            if (id != category.id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                string url = Url.Link("CategoryDetialsRoute", new { id = category.id });
                return Created(url, category);
            }


            return BadRequest(ModelState);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.id == id);
        }
    }
}

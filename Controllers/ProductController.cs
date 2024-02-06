using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPICRID.Models;

namespace WebAPICRID.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly BrandContext _dbContext;

        public ProductController(BrandContext dbContext)
        {
            _dbContext = dbContext;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (_dbContext.Products == null)
            {
                return NotFound();
            }
            return await _dbContext.Products.ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Product>> GetProductByID(int id)
        {
            if (_dbContext.Products == null)
            {
                return NotFound();
            }
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _dbContext.Products.Add(product);

            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductByID), new { id = product.Id }, product);
        }

        [HttpPut]
        //   public async Task<ActionResult> UpdateProduct(int id, Product product)
        public async Task<ActionResult> UpdateProduct(Product product)
        {
            //   if (id != product.Id) { return BadRequest(); }

            _dbContext.Entry(product).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                //if (!IsProductAvailable(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
                throw;
            }
            return Ok();

        }
        //public bool IsProductAvailable(int id)
        //{
        //    return (_dbContext.Products?.Any(x => x.Id == id)).GetValueOrDefault();
        //}
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_dbContext.Products == null)
            {
                return NotFound();
            }
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return Ok();

        }
    }

}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using WebAPICRID.Models;

namespace WebAPICRID.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandContext _dbContext;

        public BrandController(BrandContext dbContext)
        {
            _dbContext = dbContext;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            if (_dbContext.Brands == null)
            {
                return NotFound();
            }
            return await _dbContext.Brands.ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Brand>> GetBrandByID(int id)
        {
            if (_dbContext.Brands == null)
            {
                return NotFound();
            }
            var brand = await _dbContext.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return brand;
        }

        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            _dbContext.Brands.Add(brand);

            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBrandByID), new { id = brand.Id }, brand);
         }
        
        [HttpPut]
        public async Task<ActionResult> UpdateBrand(int id, Brand brand)
        {
            if (id != brand.Id) { return BadRequest(); }

            _dbContext.Entry(brand).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                //    if (!IsBrandAvailable(id))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    { ; 
                //    }
                throw;              
            }
            return Ok();

        }
        //public bool IsBrandAvailable(int id)
        //{
        //    return (_dbContext.Brands?.Any(x => x.Id == id)).GetValueOrDefault();
        //}
        [HttpDelete]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            if (_dbContext.Brands == null)
            {
                return NotFound();
            }
            var brand = await _dbContext.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            _dbContext.Brands.Remove(brand);
            await _dbContext.SaveChangesAsync();    
            return Ok();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileServices.Entities;

namespace MobileServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly MobileStoreContext _context;

        public BrandsController(MobileStoreContext context)
        {
            _context = context;
        }

        // GET: api/Brands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brands>>> GetBrands()
        {
            return await _context.Brands.ToListAsync();
        }

        // GET: api/GetBrandsByCategoryId/1
        [Route("GetBrandsByCategory/{cat_id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brands>>> GetBrandsByCategory(int cat_id)
        {
            if (cat_id != 0)
                return await _context.Brands.Where(b => b.CategoryId == cat_id).ToListAsync();
            else
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return null;
            }
                
        }

        // GET: api/Brands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brands>> GetBrands(int id)
        {
            var brands = await _context.Brands.FindAsync(id);

            if (brands == null)
            {
                return NotFound();
            }

            return brands;
        }

        // PUT: api/Brands/5
       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrands(int id, Brands brands)
        {
            if (id != brands.BrandId)
            {
                return BadRequest();
            }

            _context.Entry(brands).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandsExists(id))
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

        // POST: api/Brands
       
        [HttpPost]
        public async Task<ActionResult<Brands>> PostBrands(Brands brands)
        {
            _context.Brands.Add(brands);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrands", new { id = brands.BrandId }, brands);
        }

        // DELETE: api/Brands/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Brands>> DeleteBrands(int id)
        {
            var brands = await _context.Brands.FindAsync(id);
            if (brands == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brands);
            await _context.SaveChangesAsync();

            return brands;
        }

        private bool BrandsExists(int id)
        {
            return _context.Brands.Any(e => e.BrandId == id);
        }
    }
}

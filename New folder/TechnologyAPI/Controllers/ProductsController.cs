using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly Technology_ManagementContext _context;

        public ProductsController(Technology_ManagementContext context)
        {
            _context = context;
        }




        [EnableQuery]
        [HttpGet("getAllProductManager")]
        
        public async Task<ActionResult<IEnumerable<Product>>> getAllProductManager()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
          
            return await _context.Products.ToListAsync();


        }


        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProduct()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var s = from lst in _context.Products
                    where  lst.Status > 0 && lst.Stock > 0
                    select lst;
           
                return Ok(s);
            
          
        }

        // GET: api/Products
        [EnableQuery]
        [HttpGet("GetProductByType/{productType}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByType(string productType)
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
          var s= from lst in _context.Products
                 where lst.CategoryCode.Equals(productType) && lst.Status > 0 && lst.Stock > 0
                 select lst;
            return  Ok(s);
        }
        // GET: api/Products
        [EnableQuery]
        [HttpGet("GetProductByName/{searchString}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string searchString)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var s = from lst in _context.Products
                    where (lst.ProductName.Contains(searchString) || lst.CategoryCode.Contains(searchString)) && lst.Status > 0 && lst.Stock > 0
                    select lst;
            return Ok(s);
        }


        // GET: api/Products
        [EnableQuery]
        [HttpGet("ViewProductDetails/{proID}")]
        public async Task<ActionResult<Product>> ViewProductDetails(int proID)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var s = _context.Products.SingleOrDefault(s => s.Id == proID);
            return Ok(s);
        }



        // GET: api/Products
        [EnableQuery]
        [HttpGet("randomProduct")]
        public async Task<ActionResult<Product>> randomProduct()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var s = from prLst in _context.Products
                        .OrderBy(x => Guid.NewGuid())
                        .Where(x => x.Status > 0)
                        .Take(4)
                    select prLst;
            return Ok(s);
        }


        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
          if (_context.Products == null)
          {
              return Problem("Entity set 'Technology_ManagementContext.Products'  is null.");
          }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
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
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

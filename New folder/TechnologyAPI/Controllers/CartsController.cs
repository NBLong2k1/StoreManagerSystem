using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly Technology_ManagementContext _context;
        private readonly IMapper _mapper;
        public CartsController(Technology_ManagementContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("productListCart/{cusId}")]
        [EnableQuery]
        public IActionResult ProductListCart(int cusId)
        {
           
            var cartList = _context.Carts
                .Where(cart => cart.AccountId == cusId)
                .ToList();

            var productsListCart = new List<(Product Product, int Quantity)>();
            
            foreach (var cart in cartList)
            {
                var product = _context.Products.Find(cart.ProductId);
                if (product != null)
                {
                    productsListCart.Add(((Product Product, int Quantity))(product, cart.Quantity));
                }
            }

            List<Object> listcart =new List<object>();
            foreach (var pro in productsListCart)
            {
                Object lst = new
                {
                    totalPrice = String.Format("{0:0,0}", pro.Quantity * pro.Product.Price).ToString(),
                    pro.Quantity,
                    pro.Product.Id,
                    pro.Product.ProductName,
                    pro.Product.Price,
                    pro.Product.Stock,
                    pro.Product.CategoryCode,
                    pro.Product.ProImage,
                    pro.Product.ProDescription,
                    pro.Product.Status,
                };

                listcart.Add(lst);
            }

            return Ok(listcart);
        }



        // GET: api/Carts
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
          if (_context.Carts == null)
          {
              return NotFound();
          }
            return await _context.Carts.ToListAsync();
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {
          if (_context.Carts == null)
          {
              return NotFound();
          }
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(int id, Cart cart)
        {
            if (id != cart.AccountId)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
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


        
        

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
          if (_context.Carts == null)
          {
              return Problem("Entity set 'Technology_ManagementContext.Carts'  is null.");
          }
            _context.Carts.Add(cart);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CartExists(cart.AccountId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCart", new { id = cart.AccountId }, cart);
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartExists(int id)
        {
            return (_context.Carts?.Any(e => e.AccountId == id)).GetValueOrDefault();
        }
    }
}

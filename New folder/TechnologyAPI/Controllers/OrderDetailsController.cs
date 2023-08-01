using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.DAO;
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
    public class OrderDetailsController : ControllerBase
    {
        private readonly Technology_ManagementContext _context;

        public OrderDetailsController(Technology_ManagementContext context)
        {
            _context = context;
        }


        [HttpGet("GetTotalOrderIncome")]
        [EnableQuery]
        public IActionResult GetTotalOrderIncome()
        {
            if (_context.OrderDetails == null)
            {
                return NotFound();
            }
            incomeDAO income = new incomeDAO();
            string sumToday = String.Format("{0:0,0}", income.getTotalDaily()).ToString();
            string sumMonth = String.Format("{0:0,0}", income.getTotalMonthly()).ToString();
            string sumYear = String.Format("{0:0,0}", income.getTotalYear()).ToString();
            string totalDailyGuest = String.Format("{0:0,0}", income.getTotalDailyGuest()).ToString(); 
            string totalDailyCustomer = String.Format("{0:0,0}", income.getTotalDailyCustomer()).ToString();
            List<Decimal> totalEachMonth =  income.getListTotalEachMonth();
            

            var jsonData = new
            {
                totalDailyGuest,
                totalDailyCustomer,
               totalEachMonth,
               sumToday,
               sumMonth,
               sumYear
            };
            string jsonString = JsonConvert.SerializeObject(jsonData, Formatting.Indented);
            return Ok(jsonData);
        }



        // GET: api/OrderDetails
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails()
        {
          if (_context.OrderDetails == null)
          {
              return NotFound();
          }
            return await _context.OrderDetails.ToListAsync();
        }

        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
        {
          if (_context.OrderDetails == null)
          {
              return NotFound();
          }
            var orderDetail = await _context.OrderDetails.FindAsync(id);

            if (orderDetail == null)
            {
                return NotFound();
            }

            return orderDetail;
        }

        // PUT: api/OrderDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetail(int id, OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailId)
            {
                return BadRequest();
            }

            _context.Entry(orderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(id))
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

        // POST: api/OrderDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail(OrderDetail orderDetail)
        {
          if (_context.OrderDetails == null)
          {
              return Problem("Entity set 'Technology_ManagementContext.OrderDetails'  is null.");
          }
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderDetail", new { id = orderDetail.OrderDetailId }, orderDetail);
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            if (_context.OrderDetails == null)
            {
                return NotFound();
            }
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderDetailExists(int id)
        {
            return (_context.OrderDetails?.Any(e => e.OrderDetailId == id)).GetValueOrDefault();
        }
    }
}

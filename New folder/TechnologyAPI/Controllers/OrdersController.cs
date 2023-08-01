using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
    public class OrdersController : ControllerBase
    {
        private readonly Technology_ManagementContext _context;

        public OrdersController(Technology_ManagementContext context)
        {
            _context = context;
        }



        [HttpGet("individualCustomerOrder/{cusId}")]
        [EnableQuery]
        public IActionResult individualCustomerOrder(int cusId)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }

            //var s = _context.Orders
            //    .OrderBy(s => s.Status)
            //    .Include(s => s.Customer)
            //    .Where(s => s.Status <= 1);
            //return Ok(s);


            var income = from inco in _context.Orders.OrderByDescending(o => o.DateRecipt)
                      .Where(s => s.CustomerId == cusId && s.Status >= 2)
                      .Take(5)
            select new
                         {
                             inco.Id,
                             inco.Customer.Name,
                             customerType = inco.Customer.Role,
                             inco.Customer.Address,
                             inco.Customer.PhoneNumber,
                             inco.Status,
                             DateOrder = inco.DateOrder.ToString(),
                             DateDelivery = inco.DateDelivery.ToString(),
                             DateRecipt = inco.DateRecipt.ToString(),
                             TotalPrice = String.Format("{0:0,0}", inco.TotalPrice).ToString(),

                         };
            return Ok(income);



        }




        [HttpGet("GetProcessOrdersTake5")]
        [EnableQuery]
        public IActionResult GetProcessOrdersTake5()
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }

            //var s = _context.Orders
            //    .OrderBy(s => s.Status)
            //    .Include(s => s.Customer)
            //    .Where(s => s.Status <= 1);
            //return Ok(s);


            var income = from inco in _context.Orders.OrderByDescending(o => o.Status).Where(s => s.Status < 1).Include(s => s.Customer).OrderBy(s => s.Status).Take(5)

                         select new
                         {
                             inco.Id,
                             inco.Customer.Name,
                             customerType = inco.Customer.Role,
                             inco.Customer.Address,
                             inco.Customer.PhoneNumber,
                             inco.Status,
                             DateOrder = inco.DateOrder.ToString(),
                             DateDelivery = inco.DateDelivery.ToString(),
                             DateRecipt = inco.DateRecipt.ToString(),
                             TotalPrice = String.Format("{0:0,0}", inco.TotalPrice).ToString(),

                         };
            return Ok(income);



        }


        [HttpGet("GetCustomerInvidualHistoryOrders/{cusId}")]
        [EnableQuery]
        public IActionResult GetCustomerInvidualHistoryOrders(int cusId)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }

            //var s = _context.Orders
            //    .OrderBy(s => s.Status)
            //    .Include(s => s.Customer)
            //    .Where(s => s.Status <= 1);
            //return Ok(s);

            var income = from inco in _context.Orders.Where(s => s.Status > 1 && s.CustomerId == cusId).Include(s => s.Customer).OrderBy(s => s.Status)

                         select new
                         {
                             inco.Id,
                             inco.Customer.Name,
                             customerType = inco.Customer.Role,
                             inco.Customer.Address,
                             inco.Customer.PhoneNumber,
                             inco.Status,
                             DateOrder = inco.DateOrder.ToString(),
                             DateDelivery = inco.DateDelivery.ToString(),
                             DateRecipt = inco.DateRecipt.ToString(),
                             TotalPrice = String.Format("{0:0,0}", inco.TotalPrice).ToString(),

                         };
            return Ok(income);



        }



        [HttpGet("GetCustomerInvidualProcessOrders/{cusId}")]
        [EnableQuery]
        public IActionResult GetCustomerInvidualProcessOrders(int cusId)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }

            //var s = _context.Orders
            //    .OrderBy(s => s.Status)
            //    .Include(s => s.Customer)
            //    .Where(s => s.Status <= 1);
            //return Ok(s);

            var income = from inco in _context.Orders.Where(s => s.Status <= 1 && s.CustomerId == cusId).Include(s => s.Customer).OrderBy(s => s.Status)

                         select new
                         {
                             inco.Id,
                             inco.Customer.Name,
                             customerType = inco.Customer.Role,
                             inco.Customer.Address,
                             inco.Customer.PhoneNumber,
                             inco.Status,
                             DateOrder = inco.DateOrder.ToString(),
                             DateDelivery = inco.DateDelivery.ToString(),
                             DateRecipt = inco.DateRecipt.ToString(),
                             TotalPrice = String.Format("{0:0,0}", inco.TotalPrice).ToString(),

                         };
            return Ok(income);



        }




        [HttpGet("GetProcessOrders")]
        [EnableQuery]
        public IActionResult GetProcessOrders()
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }

            //var s = _context.Orders
            //    .OrderBy(s => s.Status)
            //    .Include(s => s.Customer)
            //    .Where(s => s.Status <= 1);
            //return Ok(s);

           var income =  from inco in _context.Orders.Where(s => s.Status <= 1).Include(s => s.Customer).OrderBy(s => s.Status)

                         select new
                         {
                             inco.Id,
                             inco.Customer.Name,
                             customerType=inco.Customer.Role,
                             inco.Customer.Address,
                             inco.Customer.PhoneNumber,
                             inco.Status,
                             DateOrder = inco.DateOrder.ToString(),
                             DateDelivery = inco.DateDelivery.ToString(),
                             DateRecipt = inco.DateRecipt.ToString(),
                             TotalPrice = String.Format("{0:0,0}", inco.TotalPrice).ToString(),

                         };
            return  Ok(income);



        }


        [HttpGet("GetHistoryOrders")]
        [EnableQuery]
        public IActionResult GetHistoryOrders()
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }

            //var s = _context.Orders
            //    .OrderBy(s => s.Status)
            //    .Include(s => s.Customer)
            //    .Where(s => s.Status <= 1);
            //return Ok(s);

            var income = from inco in _context.Orders.Where(s => s.Status >1).Include(s => s.Customer).OrderBy(s => s.Status)

                         select new
                         {
                             inco.Id,
                             inco.Customer.Name,
                             customerType = inco.Customer.Role,
                             inco.Customer.Address,
                             inco.Customer.PhoneNumber,
                             inco.Status,
                             DateOrder = inco.DateOrder.ToString(),
                             DateDelivery = inco.DateDelivery.ToString(),
                             DateRecipt = inco.DateRecipt.ToString(),
                             TotalPrice = String.Format("{0:0,0}", inco.TotalPrice).ToString(),

                         };
            return Ok(income);



        }




        // GET: api/Orders
        [HttpGet]
        [EnableQuery]
        public IActionResult GetOrders()
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }


            return Ok(_context.Orders.ToList());
        }



        // GET: api/Orders
        [HttpGet("GetAllIncome")]
        [EnableQuery]
        public IActionResult GetAllIncome()
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var income = from inco in _context.Orders.Where(s => s.Status == 2)
                     
                      select new
                      {
                          inco.Id,
                          DateOrder=  inco.DateOrder.ToString(),
                          DateDelivery = inco.DateDelivery.ToString(),
                          DateRecipt = inco.DateRecipt.ToString(),
                          TotalPrice =   String.Format("{0:0,0}", inco.TotalPrice).ToString(),

                      };
            return Ok(income);
        }



        // GET: api/Orders
        [HttpGet("getAllOrder")]
        [EnableQuery]
        public IActionResult GetAllOrders()
        {
            IQueryable<Order> orderIQ = _context.Orders.Where(s => s.Status == 2);
            return Ok(orderIQ);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
          if (_context.Orders == null)
          {
              return NotFound();
          }
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
          if (_context.Orders == null)
          {
              return Problem("Entity set 'Technology_ManagementContext.Orders'  is null.");
          }
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

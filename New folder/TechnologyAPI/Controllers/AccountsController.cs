using System;
using System.Collections.Generic;
using System.Linq;
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
    public class AccountsController : ControllerBase
    {
        private readonly Technology_ManagementContext _context;

        public AccountsController(Technology_ManagementContext context)
        {
            _context = context;
        }



        // GET: api/Accounts
        [HttpGet("getEmployee")]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Account>>> getEmployee()
        {
            Account acc = new Account();
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            return await _context.Accounts.Where(s => s.Role == 1).ToListAsync();
        }



        // GET: api/Accounts
        [HttpGet("getCustomer")]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Account>>> getCustomer()
        {
            Account acc = new Account();
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            return await _context.Accounts.Where(s => s.Role == 0).ToListAsync();
        }


        // GET: api/Accounts
        [HttpGet("getGuest")]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Account>>> getGuest()
        {
            Account acc = new Account();
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            return await _context.Accounts.Where(s => s.Role == 3).ToListAsync();
        }


        // GET: api/Accounts
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            Account acc = new Account();
          if (_context.Accounts == null)
          {
              return NotFound();
          }

            return await _context.Accounts.Where(s => s.Role == 0).ToListAsync();
        }

       

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
          if (_context.Accounts == null)
          {
              return NotFound();
          }
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, Account account)
        {
            if (id != account.UserId)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
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

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
          if (_context.Accounts == null)
          {
              return Problem("Entity set 'Technology_ManagementContext.Accounts'  is null.");
          }
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = account.UserId }, account);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(int id)
        {
            return (_context.Accounts?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}

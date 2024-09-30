using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataTierWebServer.Models;
using DataTierWebServer.Data;
using Microsoft.CodeAnalysis.Scripting;
using System.Xml.Linq;

namespace DataTierWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        
        private readonly DBManager _context;

        public AccountController(DBManager context)
        {
            _context = context;
        }

        // GET: api/account
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {

            if (_context.Accounts == null)
            {
                return NotFound();
            }
            return await _context.Accounts.ToListAsync();
        }

        // GET: api/account/5
        [HttpGet("{acctNo}")]
        public async Task<ActionResult<Account>> GetAccountById(uint acctNo)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(acctNo);
            if (account == null)
            {
                return NotFound();
            }
            return account;
        }        

        [HttpGet("history/{acctNo}")]
        public async Task<ActionResult<List<string>>> GetAccountHistory(uint acctNo)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts
                .Include(u => u.History)
                .FirstOrDefaultAsync(u => u.AcctNo == acctNo);

            if (account == null)
            {
                return NotFound();
            }
            return Ok(account.History);
        }

        // GET: api/account/1/1000
        [HttpPut("{acctNo}/{amount}")]
        public async Task<ActionResult<Account>> UpdateBalance(uint acctNo, int amount)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(acctNo);

            if (account == null)
            {
                return NotFound();
            }

            account.Balance += amount;
            var historyEntry = new UserHistory
            {
                AccountId = account.AcctNo,
                HistoryString = $"Balance updated by {amount} on {DateTime.Now} +   " +
                $"Old Balance: {account.Balance - amount} ----- New Balance: {account.Balance}"
            };
            account.History.Add(historyEntry);

            _context.Entry(account).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT: api/account/1
        /* BODY -> row -> Enter new updated account details for the acctNo
        * {
            "acctNo": 1, 
            "firstName": "Sajib Updated",
            "lastName": "Updated",
            "email": "updated_email@email.com",
            "age": 30,
            "balance": 5000,
            "address": "1 Street Suburb Updated"
         }
        */

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{acctNo}")]
        public async Task<IActionResult> PutAccount(uint acctNo, Account account)
        {
            if (acctNo != account.AcctNo)
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
                if (!AccountExists(acctNo))
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

        // POST: api/userprofile
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'DBManager.Accounts'  is null.");
            }


            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { acctNo = account.AcctNo }, account);
        }

        // DELETE: api/userprofile/5
        [HttpDelete("{acctNo}")]
        public async Task<IActionResult> DeleteAccount(uint acctNo)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(acctNo);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(uint acctNo)
        {
            return (_context.Accounts?.Any(e => e.AcctNo == acctNo)).GetValueOrDefault();
        }
    }
}



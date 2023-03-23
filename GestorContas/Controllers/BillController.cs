using GestorContas.Model;
using Microsoft.AspNetCore.Mvc;

namespace GestorContas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillController : ControllerBase
    {
        private readonly DataContext _dbContext;

        public BillController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Bill
        [HttpGet]
        public async Task<IActionResult> GetBills()
        {
            var bills = await _dbContext.Bills.ToListAsync();
            return Ok(bills);
        }

        // GET: api/Bill/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBill(int id)
        {
            var bill = await _dbContext.Bills.FirstOrDefaultAsync(b => b.ID == id);
            if (bill == null)
            {
                return NotFound();
            }
            return Ok(bill);
        }

        // POST: api/Bill
        [HttpPost]
        public async Task<IActionResult> CreateBill([FromBody] Bill bill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _dbContext.Bills.Add(bill);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetBill), new { id = bill.ID }, bill);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/Bill/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBill(int id, [FromBody] Bill bill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bill.ID)
            {
                return BadRequest();
            }

            try
            {
                _dbContext.Entry(bill).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.Bills.Any(b => b.ID == id))
                {
                    return NotFound();
                }
                throw;
            }
        }

        // DELETE: api/Bill/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBill(int id)
        {
            var bill = await _dbContext.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            _dbContext.Bills.Remove(bill);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
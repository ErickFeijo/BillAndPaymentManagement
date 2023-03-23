using GestorContas.Model;
using Microsoft.AspNetCore.Mvc;

namespace GestorContas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly DataContext _dbContext;

        public PaymentController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Payment
        [HttpGet]
        public async Task<IActionResult> GetPayments()
        {
            var payments = await _dbContext.Payments.ToListAsync();
            return Ok(payments);
        }

        // GET: api/Payment/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            var payment = await _dbContext.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        // POST: api/Payment
        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _dbContext.Payments.Add(payment);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPayment), new { id = payment.ID }, payment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/Payment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != payment.ID)
            {
                return BadRequest();
            }

            try
            {
                _dbContext.Entry(payment).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.Payments.Any(p => p.ID == id))
                {
                    return NotFound();
                }
                throw;
            }
        }

        // DELETE: api/Payment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _dbContext.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _dbContext.Payments.Remove(payment);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}

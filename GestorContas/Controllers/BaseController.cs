using GestorContas.Model;
using GestorContas.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestorContas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController<T> : ControllerBase where T : class
    {
        private readonly IBaseService<T> _baseService;

        public BaseController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var bills = _baseService.List();
            return Ok(bills);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var bill = _baseService.Find(id);
            if (bill == null)
            {
                return NotFound();
            }
            return Ok(bill);
        }

        [HttpPost]
        public IActionResult Create([FromBody] T dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _baseService.Add(dto);
                return CreatedAtAction(nameof(Get), dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] T dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _baseService.Edit(dto);
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_baseService.Find(id) == null)
                {
                    return NotFound();
                }
                throw;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var dto = _baseService.Find(id);
            if (dto == null)
            {
                return NotFound();
            }

            _baseService.Remove(dto);
            return NoContent();
        }
    }
}
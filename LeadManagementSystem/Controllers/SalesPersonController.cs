using Microsoft.AspNetCore.Mvc;
using LeadManagementSystem.Services;
using LeadManagementSystem.Models;
using Microsoft.Extensions.Logging;
using LeadManagementSystem.ViewModel.Request;
using LeadManagementSystem.ViewModel.Response;

namespace LeadManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesPersonController : ControllerBase
    {
        private readonly SalesPersonService _salesPersonService;
        private readonly ILogger<SalesPersonController> _logger;

        public SalesPersonController(SalesPersonService salesPersonService, ILogger<SalesPersonController> logger)
        {
            _salesPersonService = salesPersonService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<salesperson>>> GetAll()
        {
            var salesPeople = await _salesPersonService.GetAllAsync();
            return Ok(salesPeople);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<salesperson>> GetById(int id)
        {
            var salesPerson = await _salesPersonService.GetByIdAsync(id);
            if (salesPerson == null)
            {
                _logger.LogWarning($"Salesperson with ID {id} not found.");
                return NotFound($"Salesperson with ID {id} not found.");
            }

            return Ok(salesPerson);


        }
  
        [HttpPut("{id}")]
        public  ActionResult<salespersonVM> UpdateById(int id, [FromBody] salespersonVM salesperson)
        {
            var salespersonrespounce =  _salesPersonService.UpdateSalesPerson(id , salesperson);
            if (salespersonrespounce == null)
            {
                _logger.LogWarning($"Salesperson with ID {id} not found.");
                return NotFound($"Salesperson with ID {id} not found.");
            }

            return Ok(salespersonrespounce);

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _salesPersonService.DeleteAsync(id);
            if (!deleted)
            {
                _logger.LogWarning($"Salesperson with ID {id} not found for deletion.");
                return NotFound($"Salesperson with ID {id} not found.");
            }

            return NoContent();
        }


        [HttpPost("post-sales-person")]
        public ActionResult<SalesPersonResponse> CreateSalesPerson([FromBody] SalesPersonRequestVM salesPerson) 
        { 
            if (salesPerson == null)
            {
                return BadRequest("Sales Person Data IS Null");
            }

            return Ok(_salesPersonService.addSalesPerson(salesPerson));
        
        
        }
    }
}

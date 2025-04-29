using Microsoft.AspNetCore.Mvc;
using LeadManagementSystem.Services;
using LeadManagementSystem.Models;

namespace LeadManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesPerson : ControllerBase
    {
        private readonly SalesPersonService _salesPersonService;

        public SalesPerson(SalesPersonService salesPersonService)
        {
            _salesPersonService = salesPersonService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesPerson>>> GetAll()
        {
            var salesPeople = await _salesPersonService.GetAllAsync();
            return Ok(salesPeople);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalesPerson>> GetById(int id)
        {
            var salesPerson = await _salesPersonService.GetByIdAsync(id);
            if (salesPerson == null)
            {
                return NotFound();
            }

            return Ok(salesPerson);
        }
    }
}

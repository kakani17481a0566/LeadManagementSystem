using Microsoft.AspNetCore.Mvc;
using LeadManagementSystem.Services;
using LeadManagementSystem.Models;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;
using LeadManagementSystem.ViewModel.User;
using LeadManagementSystem.Services.ServiceImpl;
using LeadManagementSystem.ViewModel.Request;

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
            var salesPeople = await _salesPersonService.UpdateAllAsync();
            return Ok(salesPeople);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<salesperson>> GetById(int id)
        {
            var salesPerson = await _salesPersonService.UpdateByIdAsync(id);
            if (salesPerson == null)
            {
                _logger.LogWarning($"Salesperson with ID {id} not found.");
                return NotFound($"Salesperson with ID {id} not found.");
            }

            return Ok(salesPerson);


        }
        [HttpPut]
        public async Task<ActionResult<IEnumerable<SalesPerson>>> UpdateAll()
        {

            var salesperson = await _salesPersonService.UpdateAllAsync();
            return Ok(salesperson);

        }
        [HttpPut("{id}")]
        public async Task<ActionResult<salesperson>> UpdateById(int id, [FromBody] salespersonVM salesperson)
        {
            var salesPerson = await _salesPersonService.UpdateByIdAsync(id);
            if (salesPerson == null)
            {
                _logger.LogWarning($"Salesperson with ID {id} not found.");
                return NotFound($"Salesperson with ID {id} not found.");
            }

            return Ok(salesPerson);

        }
    }
}

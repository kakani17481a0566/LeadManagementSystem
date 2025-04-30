using Microsoft.AspNetCore.Mvc;
using LeadManagementSystem.Services;
using LeadManagementSystem.ViewModel.Request;
using LeadManagementSystem.ViewModel.Response;
using Microsoft.Extensions.Logging;
using LeadManagementSystem.Models;

[Route("api/[controller]")]
[ApiController]
// Controller to manage SalesPerson-related HTTP requests
public class SalesPersonController : ControllerBase
{
    private readonly SalesPersonService _service;
    private readonly ILogger<SalesPersonController> _logger;

    public SalesPersonController(
        SalesPersonService service,
        ILogger<SalesPersonController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // POST: api/SalesPerson
    // Adds a new salesperson
    [HttpPost]
    public async Task<ActionResult<SalesPersonResponse>> Create([FromBody] SalesPersonRequestVM request)
    {
        // Validate request model
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Create salesperson using service
        var result = await _service.AddSalesPersonAsync(request);

        // Return 201 Created with location header
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    // GET: api/SalesPerson
    // Returns all salespersons
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SalesPerson>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    // GET: api/SalesPerson/{id}
    // Returns a single salesperson by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<SalesPerson>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return result != null ? Ok(result) : NotFound();
    }

    // PUT: api/SalesPerson/{id}
    // Updates a salesperson by ID
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SalesPersonVM request)
    {
        // Validate request model
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            // Attempt to update salesperson
            var updatedEntity = await _service.UpdateSalesPersonAsync(id, request);

            // If not found, return 404
            if (updatedEntity == null) return NotFound();

            // Add updated field values to response headers
            Response.Headers.Append("X-Updated-Name", updatedEntity.Name);
            Response.Headers.Append("X-Updated-Phone", updatedEntity.PhoneNumber);
            Response.Headers.Append("X-Updated-Email", updatedEntity.Email);

            return Ok(updatedEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating salesperson");
            return StatusCode(500); // Internal Server Error
        }
    }

    // DELETE: api/SalesPerson/{id}
    // Deletes a salesperson by ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}

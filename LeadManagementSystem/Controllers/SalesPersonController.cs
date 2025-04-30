using Microsoft.AspNetCore.Mvc;
using LeadManagementSystem.Services;
using LeadManagementSystem.ViewModel.Request;
using LeadManagementSystem.ViewModel.Response;
using Microsoft.Extensions.Logging;
using LeadManagementSystem.Models;

[Route("api/[controller]")]
[ApiController]
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

    [HttpPost]
    public async Task<ActionResult<SalesPersonResponse>> Create([FromBody] SalesPersonRequestVM request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.AddSalesPersonAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SalesPerson>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SalesPerson>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SalesPersonVM request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var updatedEntity = await _service.UpdateSalesPersonAsync(id, request);
            if (updatedEntity == null) return NotFound();

            // Add updated values to headers
            Response.Headers.Append("X-Updated-Name", updatedEntity.Name);
            Response.Headers.Append("X-Updated-Phone", updatedEntity.PhoneNumber);
            Response.Headers.Append("X-Updated-Email", updatedEntity.Email);

            return Ok(updatedEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating salesperson");
            return StatusCode(500);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}
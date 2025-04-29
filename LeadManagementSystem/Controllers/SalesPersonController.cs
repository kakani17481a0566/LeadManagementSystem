using Microsoft.AspNetCore.Mvc;
using LeadManagementSystem.Services;
using LeadManagementSystem.ViewModel.Request;
using LeadManagementSystem.ViewModel.Response;
using LeadManagementSystem.Models;

[Route("api/[controller]")]
[ApiController]
public class SalesPersonController : ControllerBase
{
    private readonly SalesPersonService _service;

    public SalesPersonController(SalesPersonService service)
    {
        _service = service;
    }

    // CREATE
    [HttpPost]
    public async Task<ActionResult<SalesPersonResponse>> Create([FromBody] SalesPersonRequestVM request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.AddSalesPersonAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    // READ ALL
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SalesPerson>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    // READ BY ID
    [HttpGet("{id}")]
    public async Task<ActionResult<SalesPerson>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return result != null ? Ok(result) : NotFound();
    }

    // UPDATE
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SalesPersonVM request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.UpdateSalesPersonAsync(id, request);
        return result != null ? NoContent() : NotFound();
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}
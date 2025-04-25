using System.Collections.Generic;
using System.Threading.Tasks;
using LeadManagementSystem.Models;
using LeadManagementSystem.Services.Lead;
using LeadManagementSystem.ViewModel.Lead;
using Microsoft.AspNetCore.Mvc;

namespace LeadManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // Use ApiController if it's a Web API controller
    public class LeadController : ControllerBase
    {
        private readonly LeadService _leadService;

        public LeadController(LeadService leadService)
        {
            _leadService = leadService;
        }

        #region Get All Leads
        // GET: api/lead
        [HttpGet]
        public async Task<ActionResult<List<LeadVM>>> GetLeads()
        {
            var leads = await _leadService.GetLeadsAsync();
            return Ok(leads); // Return 200 OK with list of leads
        }
        #endregion

        #region Get Lead by ID
        // GET: api/lead/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LeadVM>> GetLeadById(int id)
        {
            var lead = await _leadService.GetLeadByIdAsync(id);

            if (lead == null)
            {
                return NotFound(); // Return 404 if the lead is not found
            }

            return Ok(lead); // Return 200 OK with lead details
        }
        #endregion

        #region Create Lead
        // POST: api/lead
        [HttpPost]
        public ActionResult<string> CreateLead([FromBody] LeadVMPost leadVMPost)
        {
            if (leadVMPost == null)
            {
                return BadRequest("Lead data is null"); // Return 400 BadRequest if the input is null
            }

            var leadEntity =  _leadService.CreateLeadAsync(leadVMPost);

          


            return Ok("USER CREATED SUCCESFULL"); // Return 200 OK with the created lead
        }
        #endregion

        #region Update Lead
        // PUT: api/lead/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLead(int id, [FromBody] LeadVMPost leadVMPost)
        {
            if (leadVMPost == null)
            {
                return BadRequest("Lead data is null"); // Return 400 BadRequest if the input is null
            }

            var leadEntity = await _leadService.UpdateLeadAsync(id, leadVMPost);

            if (leadEntity == null)
            {
                return NotFound(); // Return 404 if the lead is not found
            }

            return NoContent(); // Return 204 No Content if the lead was successfully updated
        }
        #endregion

        #region Delete Lead
        // DELETE: api/lead/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLead(int id)
        {
            var success = await _leadService.DeleteLeadAsync(id);

            if (!success)
            {
                return NotFound(); // Return 404 if the lead was not found
            }

            return NoContent(); // Return 204 No Content if the lead was successfully deleted
        }
        #endregion
    }
}

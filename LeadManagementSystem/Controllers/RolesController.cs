using LeadManagementSystem.Data;
using LeadManagementSystem.Models;
using LeadManagementSystem.ViewModel.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeadManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject the ApplicationDbContext
        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleViewModel>>> GetRoles()
        {
            var roles = await _context.Roles
                                      .Select(role => new RoleViewModel
                                      {
                                          Id = role.Id,
                                          Name = role.Name
                                      })
                                      .ToListAsync();

            return Ok(roles);
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleViewModel>> GetRole(int id)
        {
            var role = await _context.Roles
                                      .Where(r => r.Id == id)
                                      .Select(r => new RoleViewModel
                                      {
                                          Id = r.Id,
                                          Name = r.Name
                                      })
                                      .FirstOrDefaultAsync();

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<RoleViewModel>> CreateRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new RolesModel
                {
                    Name = model.Name
                };

                _context.Roles.Add(role);
                await _context.SaveChangesAsync();

                model.Id = role.Id;

                return CreatedAtAction(nameof(GetRole), new { id = model.Id }, model);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, RoleViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            role.Name = model.Name;

            _context.Roles.Update(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

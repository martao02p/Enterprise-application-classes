using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly CompanyDbContext _context;

        public DepartmentsController(CompanyDbContext context)
        {
            _context = context;
        }

        private static DepartmentDTO DepartmentToDTO(Department department) =>
        new DepartmentDTO
        {
            DepartmentId = department.DepartmentId,
            Name = department.Name
        };

        private static Department DTOToDepartment(DepartmentDTO dto) =>
            new Department
            {
                DepartmentId = dto.DepartmentId,
                Name = dto.Name
            };

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetDepartments()
        {
            return await _context.Departments
                .Select(d => DepartmentToDTO(d))
                .ToListAsync();
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDTO>> GetDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return DepartmentToDTO(department);
        }

        // PUT: api/Departments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, DepartmentDTO departmentDTO)
        {
            if (id != departmentDTO.DepartmentId)
            {
                return BadRequest();
            }

            var department = DTOToDepartment(departmentDTO);
            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Departments
        [HttpPost]
        public async Task<ActionResult<DepartmentDTO>> PostDepartment(DepartmentDTO departmentDTO)
        {
            var department = DTOToDepartment(departmentDTO);
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            departmentDTO.DepartmentId = department.DepartmentId;

            return CreatedAtAction(nameof(GetDepartment), new { id = departmentDTO.DepartmentId }, departmentDTO);
        }

        // DELETE: api/Departments/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<DepartmentDTO>> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return DepartmentToDTO(department);
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.DepartmentId == id);
        }
    }
}

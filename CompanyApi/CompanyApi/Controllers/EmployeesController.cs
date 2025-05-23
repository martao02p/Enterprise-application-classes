using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyApi.Models;


namespace CompanyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly CompanyDbContext _context;

        private static EmployeeDTO EmployeeToDTO(Employee employee) =>
            new EmployeeDTO
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                ManagerId = employee.ManagerId,
                Salary = employee.Salary,
                Bonus = employee.Bonus,
                DepartmentId = employee.DepartmentId
            };

        private static Employee DTOToEmployee(EmployeeDTO dto) =>
            new Employee
            {
                EmployeeId = dto.EmployeeId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                ManagerId = dto.ManagerId,
                Salary = dto.Salary,
                Bonus = dto.Bonus,
                DepartmentId = dto.DepartmentId
            };


        public EmployeesController(CompanyDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            return await _context.Employees
                .Select(e => EmployeeToDTO(e))
                .ToListAsync();
        }


        // GET: api/Employees/id
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return EmployeeToDTO(employee);
        }


        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeDTO employeeDTO)
        {
            if (id != employeeDTO.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(DTOToEmployee(employeeDTO)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> PostEmployee(EmployeeDTO employeeDTO)
        {
            var employee = DTOToEmployee(employeeDTO);

            employee.EmployeeId = 0;

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            employeeDTO.EmployeeId = employee.EmployeeId;

            return CreatedAtAction(nameof(GetEmployee), new { id = employeeDTO.EmployeeId }, employeeDTO);
        }



        // DELETE: api/Employees/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeDTO>> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return EmployeeToDTO(employee);
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}

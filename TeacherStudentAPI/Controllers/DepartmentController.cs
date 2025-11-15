using Application.Constants;
using Application.DTOs.Department;
using Application.Services.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Results;

namespace TeacherStudentAPI.Controllers
{
    [Route("api/department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartment _departmentService;

        public DepartmentController(IDepartment departmentService)
        {
            _departmentService = departmentService;
        }

        // ============================================
        // Add Department
        // ============================================
        [HttpPost("add", Name = "AddDepartment")]
        [Authorize(Roles = RoleConstants.Admin)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentCreateDTO dto)
        {
            var result = await _departmentService.AddAsync(dto);

            if (result.IsSuccess)
                return CreatedAtRoute("GetDepartmentById", new { id = result.Data.Id }, result.Data);

            return result.ToActionResult();
        }

        // ============================================
        // Get All Departments
        // ============================================
        [HttpGet("all", Name = "GetAllDepartments")]
        [Authorize(Roles = RoleConstants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DepartmentDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDepartments()
        {
            var result = await _departmentService.GetAllAsync();
            return result.ToActionResult();
        }

        // ============================================
        // Get Department by ID
        // ============================================
        [HttpGet("{id}", Name = "GetDepartmentById")]
        [Authorize(Roles = RoleConstants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DepartmentDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var result = await _departmentService.GetByIDAsync(id);
            return result.ToActionResult();
        }

        // ============================================
        // Update Department
        // ============================================
        [HttpPut("update", Name = "UpdateDepartment")]
        [Authorize(Roles = RoleConstants.Admin)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentUpdateDTO dto)
        {
            var result = await _departmentService.UpdateAsync(dto);
            return result.ToActionResult();
        }

        // ============================================
        // Delete Department
        // ============================================
        [HttpDelete("delete", Name = "DeleteDepartment")]
        [Authorize(Roles = RoleConstants.Admin)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var result = await _departmentService.DeleteAsync(id);

            if (result.IsSuccess)
                return NoContent();

            return result.ToActionResult();
        }
    }
}

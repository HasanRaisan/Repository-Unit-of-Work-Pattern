using Application.Constants;
using Application.DTOs.Student;
using Application.Services.Students;
using Application.Services.Teachers;
using Application.DTOs.Teaher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Results;

namespace TeacherStudentAPI.Controllers
{
    [Route("api/teacher")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacher _teacherService;
        public TeacherController(ITeacher Service) 
        {
            _teacherService = Service;
        }

        [HttpPost("add", Name = "AddTeacher")]
        [Authorize(Roles = $"{RoleConstants.Admin}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Add([FromBody] TeacherDTO teacherDTO)
        {
            var result = await _teacherService.AddAsync(teacherDTO);

            // RESTful Success: Use 201 Created with Location Header for resource creation.
            if (result.IsSuccess)
                return CreatedAtRoute("GetStudentById", new { id = result.Data.Id }, result.Data);

            //  Centralized Failure Handling
            return result.ToActionResult();
        }

        [HttpGet("all", Name = "GetAllTeacher")]
        [Authorize(Roles = $"{RoleConstants.Admin}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeacherDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTeacher()
        {
            var result = await _teacherService.GetAllAsync();

            return result.ToActionResult();
        }

        [HttpGet("{id:int}", Name = "GetTeacherById")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Teacher}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTeacherById(int id)
        {
            var result = await _teacherService.GetByIDAsync(id);
            return result.ToActionResult();
        }


        [HttpPut("update", Name = "UpdateTeacher")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Teacher}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateTeacher([FromBody] TeacherDTO dto)
        {
            var result = await _teacherService.UpdateAsync(dto);

            return result.ToActionResult();
        }

        [HttpDelete("{id:int}", Name = "DeleteTeacher")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Teacher}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var result = await _teacherService.DeleteAsync(id);
            // Check for success first.
            if (result.IsSuccess)
                // RESTful Success: Use 204 No Content, as the resource no longer exists and no body is returned.
                return NoContent();

            // Centralized Failure Handling
            return result.ToActionResult();
        }


        [HttpGet("department/{id:int}", Name = "GetTeachersByDepartment")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Teacher}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTeachersByDepartment(int id)
        {
            var result = await _teacherService.GetTeachersByDepartmentAsync(id);
            return result.ToActionResult();
        }
    }
}

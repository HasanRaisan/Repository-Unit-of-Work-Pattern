using Application.Constants;
using Application.DTOs.Student;
using Application.Services.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Application.Results;
namespace TeacherStudentAPI.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudent _studentService;
        public StudentController(IStudent studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("add", Name = "AddStudent")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Teacher}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Add([FromBody] StudentCreateDTO studentDTO)
        {
            var result = await _studentService.AddAsync(studentDTO);

            // RESTful Success: Use 201 Created with Location Header for resource creation.
            if (result.IsSuccess)
                return CreatedAtRoute("GetStudentById", new { id = result.Data.Id }, result.Data);

            //  Centralized Failure Handling
            return result.ToActionResult();
        }
 

        [HttpGet("all", Name = "GetAllStudent")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Teacher}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StudentDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllStudents()
        {
            var result = await _studentService.GetAllAsync();

            return result.ToActionResult();
        }

        [HttpGet("{id}", Name = "GetStudentById")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Teacher},{RoleConstants.Student}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var result = await _studentService.GetByIDAsync(id);
            return result.ToActionResult();
        }


        [HttpPut("update", Name ="UpdateStudent")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Teacher},{RoleConstants.Student}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateStudent([FromBody] StudentUpdateDTO dto)
        {
            var result = await _studentService.UpdateAsync(dto);
            return result.ToActionResult();
        }

        [HttpDelete("delete", Name ="DeleteStudent")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Teacher},{RoleConstants.Student}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var result = await _studentService.DeleteAsync(id);

            // Check for success first.
            if (result.IsSuccess)
                // RESTful Success: Use 204 No Content, as the resource no longer exists and no body is returned.
                return NoContent();
            

            // Centralized Failure Handling
            return result.ToActionResult();
        }
    }
}

using Business.Constants;
using Business.DTOs.Student;
using Business.Services.Students;
using Clean_Three_Tier_First.DTOs.Teaher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean_Three_Tier_First.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public StudentController(IStudent studentService)
        {
            StudentService = studentService;
        }

        public IStudent StudentService { get; }

        [HttpPost("add", Name = "AddStudent")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Teacher}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add([FromBody] StudentDTO studentDTO)
        {
            var result = await StudentService.AddAsync(studentDTO);

            if (!result.IsSuccess)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return CreatedAtRoute( "GetStudentById",
                new { id = result.Data.Id }, result.Data);
        }

        [HttpGet("all", Name = "GetAllStudent")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Teacher}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StudentDTO>))]
        public async Task<IActionResult> GetAllStudents()
        {
            var result = await StudentService.GetAllAsync();

            return Ok(result.Data);
        }

        [HttpGet("{id}", Name = "GetStudentById")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Teacher},{RoleConstants.Student}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var result = await StudentService.GetByIDAsync(id);

            if (!result.IsSuccess)
            {
                return NotFound(new { Errors = result.Errors });
            }
            return Ok(result.Data);
        }


        [HttpPut("update", Name ="UpdateStudent")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Teacher},{RoleConstants.Student}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateStudent([FromBody] StudentDTO dto)
        {
            var result = await StudentService.UpdateAsync(dto);

            if (!result.IsSuccess)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return Ok(result.Data);
        }

        [HttpDelete("delete", Name ="DeleteStudent")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Teacher},{RoleConstants.Student}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var result = await StudentService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                // Check if the error is due to the user not being found
                if (result.Errors.Any(e => e.Contains("not found")))
                {
                    return NotFound(new { Errors = result.Errors });
                }
                // For other Identity errors (e.g., failure due to external factors)
                return BadRequest(new { Errors = result.Errors });
            }

            return NoContent();
        }
    }
}

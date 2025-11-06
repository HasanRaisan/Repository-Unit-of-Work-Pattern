using Business.Constants;
using Business.DTOs.Student;
using Business.Services.Students;
using Business.Services.Teachers;
using Clean_Three_Tier_First.DTOs.Teaher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean_Three_Tier_First.Controllers
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
        public async Task<IActionResult> Add([FromBody] TeacherDTO teacherDTO)
        {
            var result = await _teacherService.AddAsync(teacherDTO);

            if (!result.IsSuccess)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return CreatedAtRoute("GetTeacherById",
                new { id = result.Data.Id }, result.Data);
        }

        [HttpGet("all", Name = "GetAllTeacher")]
        [Authorize(Roles = $"{RoleConstants.Admin}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeacherDTO>))]
        public async Task<IActionResult> GetAllTeacher()
        {
            var result = await _teacherService.GetAllAsync();

            return Ok(result.Data);
        }

        [HttpGet("{id:int}", Name = "GetTeacherById")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Teacher}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTeacherById(int id)
        {
            var result = await _teacherService.GetByIDAsync(id);

            if (!result.IsSuccess)
            {
                return NotFound(new { Errors = result.Errors });
            }
            return Ok(result.Data);
        }


        [HttpPut("update", Name = "UpdateTeacher")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Teacher}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTeacher([FromBody] TeacherDTO dto)
        {
            var result = await _teacherService.UpdateAsync(dto);

            if (!result.IsSuccess)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return Ok(result.Data); // or NoContent():
        }

        [HttpDelete("{id:int}", Name = "DeleteTeacher")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Teacher}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var result = await _teacherService.DeleteAsync(id);
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


        [HttpGet("department/{id:int}", Name = "GetTeachersByDepartment")]
        [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.Teacher}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTeachersByDepartment(int id)
        {
            var result = await _teacherService.GetTeachersByDepartmentAsync(id);

            if (!result.IsSuccess)
            {
                return NotFound(new { Errors = result.Errors });
            }
            return Ok(result.Data);
        }
    }
}

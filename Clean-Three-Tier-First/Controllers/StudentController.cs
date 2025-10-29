using Business.DTOs.Student;
using Business.Services.Students;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean_Three_Tier_First.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public StudentController(IStudent studentService)
        {
            StudentService = studentService;
        }

        public IStudent StudentService { get; }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] StudentDTO studentDTO)
        {
            var result = await StudentService.AddAsync(studentDTO);

            if (!result.IsSuccess)
            {
                return BadRequest(new
                {
                    Title = "Validation Failed",
                    Errors = result.Errors
                });
            }

            return CreatedAtAction(nameof(Add), result.Data);

        }
    }
}

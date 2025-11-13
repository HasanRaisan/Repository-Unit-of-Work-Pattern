using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Results
{
    public static class ResultToActionMapper
    {
        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            // 1. (Success)
            if (result.IsSuccess)
                return new OkObjectResult(result.Data);

            // 2. If the Erorr was null
            if (result.Error == null)
                return new BadRequestObjectResult(new { Errors = new[] { "Unknown error occurred." } });

            // 3. (Switch Expression)
            return result.Error.Type switch
            {
                // 400 Bad Request
                ErrorType.ValidationError => new BadRequestObjectResult(new { Errors = new[] { result.Error.Message } }),

                // 404 Not Found
                ErrorType.NotFound => new NotFoundObjectResult(new { Errors = new[] { result.Error.Message } }),

                // 409 Conflict
                ErrorType.Conflict or ErrorType.NoChangesDetected =>
                    new ConflictObjectResult(new { Errors = new[] { result.Error.Message } }),

                // 500 Internal Server Error
                ErrorType.InternalError =>
                    new ObjectResult(new { Errors = new[] { result.Error.Message } })
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    },

                // Anything else handle as 400
                _ => new BadRequestObjectResult(new { Errors = new[] { result.Error.Message } })
            };
        }
    }
}

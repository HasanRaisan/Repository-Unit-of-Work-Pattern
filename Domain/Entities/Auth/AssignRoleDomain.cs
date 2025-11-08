using Domain.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class AssignRoleDomain
    {
        public string UserId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public Result ValidateBusinessRules()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(UserId))
                errors.Add("UserId is required.");

            if (string.IsNullOrWhiteSpace(RoleName))
                errors.Add("RoleName is required.");

            return errors.Count == 0
                ? Result.Success()
                : Result.Failure(errors);
        }
    }
}

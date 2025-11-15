using Domain.Results;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth.AssignRole
{
    public class AssignRoleDomain
    {
        public string UserId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;

        private AssignRoleDomain(string userId, string role)
        {
            UserId = userId;
            RoleName = role;
        }

        private static List<string> Validate(string userId, string role, List<string> validRoles)
        {
            return AssignRoleRules.ValidateUserId(userId)
                .Concat(AssignRoleRules.ValidateRole(role, validRoles))
                .ToList();
        }

        public static Result<AssignRoleDomain> Create(string userId, string role, List<string> validRoles)
        {
            var errors = Validate(userId, role, validRoles);

            if (errors.Any())
                return Result<AssignRoleDomain>.Fail(errors);

            var assignment = new AssignRoleDomain(userId, role);

            return Result<AssignRoleDomain>.Success(assignment);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth.AssignRole
{
    public static class AssignRoleRules
    {
        public static IEnumerable<string> ValidateUserId(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                yield return "User ID is required for role assignment.";
        }

        public static IEnumerable<string> ValidateRole(string role, List<string> validRoles)
        {
            // for developer 
            if (validRoles == null || !validRoles.Any())
            {
                yield return "Validation error: The list of valid roles cannot be empty or null.";
                yield break; 
            }

            if (string.IsNullOrWhiteSpace(role))
            {
                yield return "The role name is required for assignment.";
            }
            else 
            {
                if (role.Length < 3 || role.Length > 50)
                    yield return "Role name must be between 3 and 50 characters.";

                if (!validRoles.Contains(role, StringComparer.OrdinalIgnoreCase))
                {
                    yield return $"{role} is not a valid role name in the system. Valid roles are: {string.Join(", ", validRoles)}.";
                }
            }
        }
    }
}

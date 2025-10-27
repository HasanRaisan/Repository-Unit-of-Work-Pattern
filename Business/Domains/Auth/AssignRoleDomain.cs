using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Auth
{
    public class AssignRoleDomain
    {
        public string UserId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
}

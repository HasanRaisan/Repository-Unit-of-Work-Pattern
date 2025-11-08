using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Results
{
    public class AuthResult
    {
        public string Message { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> Role { get; set; } = [];
        public string Token { get; set; } = string.Empty;
        public bool IsAuthenticated { get; set; } = false;
        public DateTime Expiration { get; set; }
    }
}

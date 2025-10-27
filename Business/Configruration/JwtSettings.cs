using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configruration
{
    public class JwtSettings
    {   // Must be a strong, secret key used to sign the JWT.
        public string SecretKey { get; set; } = string.Empty; 
        
        // The sender/issuer of the token.
        public string Issuer { get; set; } = string.Empty;
        
        // The intended recipient/audience of the token.
        public string Audience { get; set; } = string.Empty;
        
        // The lifetime of the token in minutes.
        public int TokenValidityInMinutes { get; set; }
        
        // The lifetime of the refresh token in days.
        public int RefreshTokenValidityInDays { get; set; }
    }
}

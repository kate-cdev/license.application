using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace license.application.Models
{
    public class JwtConstants
    {
        public const string Issuer = "LicenseService";
        public const string Audience = "UserMachine";
        public const string key = "12345678zxcvbnm112212344556667777";

        public const string AuthSchemes = "Identity.Application" + "," + JwtBearerDefaults.AuthenticationScheme;
    }
}

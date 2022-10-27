using license.application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace license.application.Controllers.API
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        public AuthenticationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Route("api/Authentication/login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] JWtTokenViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null) return BadRequest();

                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (signInResult.Succeeded)
                {
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Models.JwtConstants.key));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, model.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, model.UserName),
                    };

                    var token = new JwtSecurityToken(
                        Models.JwtConstants.Issuer,
                        Models.JwtConstants.Audience,
                        claims,
                        expires: DateTime.UtcNow.AddHours(8),
                        signingCredentials: creds
                        ) ;

                    var results = new { Token = new JwtSecurityTokenHandler().WriteToken(token), Expiration = token.ValidTo, Message = "Login Successfully" };

                    return Ok(results);
                } else
                {
                    return BadRequest();
                }
            }

            return BadRequest();
        }
    }
}

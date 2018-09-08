using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BranchServer.Resources;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;

namespace BranchServer
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly BranchContext _context;

        public AuthController(BranchContext context) {
            _context = context;
        }

        /// <summary>
        /// Validates the information the user entered to login against
        /// users in the database
        /// </summary>
        /// <param name="loginResource">The email and password entered by the user on the client</param>
        /// <returns>A valid JWT if the login information matches an actual user profile</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginResource loginResource)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginResource.Email);

            if (user == null)
            {
                return NotFound("Couldn't find a matching user for the given email");
            }

            var password = loginResource.Password + user.Salt;
            var isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (isValid)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("developmentKey!@3"));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:62002",
                    audience: "http://localhost:4200",
                    claims: new List<Claim>
                    {
                        new Claim("Email", user.Email)
                    },
                    signingCredentials: signingCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString });
            } else
            {
                return Unauthorized();
            }
        }
    }
}
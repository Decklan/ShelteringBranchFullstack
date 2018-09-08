using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BranchServer.Resources;

namespace BranchServer
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly BranchContext _context;

        public UserController(BranchContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously creates a new user to access the dashboard functionality
        /// of the web application
        /// </summary>
        /// <param name="userResource">The user resource to create a user for</param>
        /// <returns>The new users information</returns>
        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody]CreateUserResource createUserResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == createUserResource.Email);

            if (user != null)
            {
                return BadRequest("Email is already taken.");
            }

            // Create a salt, add it to the password, and hash it
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var saltedPassword = createUserResource.Password + salt;
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(saltedPassword);

            // Create a new user
            var newUser = new Models.User
            {
                Email = createUserResource.Email,
                Password = hashedPassword,
                Salt = salt,
                FirstName = createUserResource.FirstName,
                LastName = createUserResource.LastName
            };

            // Add the new user and save changes
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            // Fetch the newly created user and map it to a resource to return to the client
            var createdUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == newUser.Email);
            var userResource = new UserResource
            {
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };
            return Ok(userResource);
        }

        /// <summary>
        /// Asynchronously retrieves a user account by the given email param
        /// </summary>
        /// <param name="email">The email of the user we are fetching</param>
        /// <returns>The User corresponding to the given email</returns>
        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return NotFound("Couldn't find an account for the given user email.");
            }

            // Map user model to user resource to return to client
            var uResource = new UserResource
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return Ok(uResource);
        }
    }
}
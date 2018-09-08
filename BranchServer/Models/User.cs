using System;
using System.ComponentModel.DataAnnotations;

namespace BranchServer.Models
{
    /**
     * User model for authentication purposes. Stores:
     * 1. The users email address
     * 2. The users password
     * 3. The users unique salt
     * 4. The users first and last name
     * 
     * Will only have two users (Jim and Deborah) who can access 
     * the site dashboard.
     */
    public class User
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        public string Salt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

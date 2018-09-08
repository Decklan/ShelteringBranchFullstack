using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BranchServer.Resources
{
    /**
     * Login resources stores the email and password that
     * the user entered on the client so it can be used to
     * fetch the account and validate the password.
     */
    public class LoginResource
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

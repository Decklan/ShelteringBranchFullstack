using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BranchServer.Resources
{
    public class CreateUserResource : UserResource
    { 
        public string Password { get; set; }
    }
}

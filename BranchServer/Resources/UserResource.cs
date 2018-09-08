namespace BranchServer.Resources
{
    /**
     * Resource that stores the user email, first and last name to 
     * return to the client
     */
    public class UserResource
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

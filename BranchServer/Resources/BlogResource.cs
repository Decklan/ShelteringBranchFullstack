using System;

namespace BranchServer.Resources
{
    public class BlogResource
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
        public string Author { get; set; }
    }
}
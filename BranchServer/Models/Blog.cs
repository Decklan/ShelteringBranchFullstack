using System;
using System.ComponentModel.DataAnnotations;

namespace BranchServer.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
        public string Author { get; set; }
    }
}
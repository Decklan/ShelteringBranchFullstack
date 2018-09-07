using System;
using BranchServer.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BranchServer.Resources
{
    public class BlogResource
    {
        public int BlogId;
        public string Title;
        public string Body;
        public DateTime CreationDate;
        public string Author;
    }
}
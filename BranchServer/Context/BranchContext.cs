using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BranchServer.Models;

namespace BranchServer
{
    public class BranchContext : DbContext
    {
        public BranchContext(DbContextOptions<BranchContext> options): base(options) 
        {}

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

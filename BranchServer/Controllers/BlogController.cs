using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BranchServer.Models;
using Microsoft.EntityFrameworkCore;
using BranchServer.Resources;

namespace BranchServer
{
    [Route("api/[controller]")]
    public class BlogController : Controller
    {
        private readonly BranchContext _context;
        public BlogController(BranchContext context) {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddBlogAsync([FromBody] BlogResource blogResource) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var blog = new Blog {
                BlogId = blogResource.BlogId,
                Title = blogResource.Title,
                Body = blogResource.Body,
                CreationDate = blogResource.CreationDate,
                Author = blogResource.Author
            };

            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();

            blog = await _context.Blogs.FirstOrDefaultAsync(newBlog => newBlog.BlogId == blog.BlogId);

            var resource = new BlogResource {
                BlogId = blog.BlogId,
                Title = blog.Title,
                Body = blog.Body,
                CreationDate = blog.CreationDate,
                Author = blog.Author
            };

            return Ok(resource);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogsAsync() {
            var blogs = await _context.Blogs.ToListAsync();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskByIdAsync(int id) {
            var blog = await _context.Blogs.FirstOrDefaultAsync(b => b.BlogId == id);

            if (blog == null) {
                return NotFound("Couldn't find a matching blog post.");
            }

            var blogResource = new BlogResource {
                BlogId = blog.BlogId,
                Title = blog.Title,
                Body = blog.Body,
                CreationDate = blog.CreationDate,
                Author = blog.Author
            };
            return Ok(blogResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogAsync(int id, [FromBody] BlogResource blogResource) {
            var updating = await _context.Blogs.FirstOrDefaultAsync(toUpdate => toUpdate.BlogId == id);

            if (updating == null) {
                return NotFound("Couldn't find a matching blog post.");
            }

            updating.Title = blogResource.Title;
            updating.Body = blogResource.Body;
            updating.Author = blogResource.Author;

            await _context.SaveChangesAsync();

            var resource = new BlogResource {
                BlogId = updating.BlogId,
                Title = updating.Title,
                Body = updating.Body,
                CreationDate = updating.CreationDate,
                Author = updating.Author
            };
            return Ok(resource);
        }
    }
}

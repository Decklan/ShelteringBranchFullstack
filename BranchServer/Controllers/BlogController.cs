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
        /**
         * _context contains the various DbSets that we operate
         * on including:
         * * Blogs
         * * Auth
         */
        private readonly BranchContext _context;
        public BlogController(BranchContext context) {
            _context = context;
        }

        /// <summary>
        /// Adds a single blog post to the database and returns the newly
        /// created post back to the client
        /// </summary>
        /// <param name="blogResource">The blog that is being sent to the server for creation</param>
        /// <returns>The newly created blog that was stored in the database</returns>
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

            if (blog == null)
            {
                return NotFound("Couldn't find the newly created blog.");
            }

            var resource = new BlogResource {
                BlogId = blog.BlogId,
                Title = blog.Title,
                Body = blog.Body,
                CreationDate = blog.CreationDate,
                Author = blog.Author
            };

            return Ok(resource);
        }

        /// <summary>
        /// Asynchronously fetches the entire collection of blogs to
        /// return to the client
        /// </summary>
        /// <returns>A list of all current blogs</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllBlogsAsync() {
            var blogs = await _context.Blogs.ToListAsync();

            if (blogs == null)
            {
                return NotFound("There are currently no blog posts in the db.");
            }

            // Create list of resources from list of backend models
            var blogResources = new List<BlogResource>();
            blogs.ForEach(blog => {
                var resource = new BlogResource
                {
                    BlogId = blog.BlogId,
                    Title = blog.Title,
                    Body = blog.Body,
                    CreationDate = blog.CreationDate,
                    Author = blog.Author
                };
                blogResources.Add(resource);
            });

            return Ok(blogResources);
        }

        /// <summary>
        /// Fetches a specific blog from the database matching the given id param
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Blog with matching id property</returns>
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

        /// <summary>
        /// Updates a single blog with the information from the resource param
        /// </summary>
        /// <param name="id">The id of the blog we are updating</param>
        /// <param name="blogResource">The contents we want to update with</param>
        /// <returns>The newly updated blog from the database</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlogAsync(int id, [FromBody] BlogResource blogResource) {
            var updating = await _context.Blogs.FirstOrDefaultAsync(toUpdate => toUpdate.BlogId == id);

            if (updating == null) {
                return NotFound("Couldn't find a matching blog post.");
            }

            // Update relevant fields, ignoring the id and creation date
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

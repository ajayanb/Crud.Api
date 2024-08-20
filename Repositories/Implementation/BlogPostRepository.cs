
using CodPulse.API.Data;
using CodPulse.API.Models.Domain;
using CodPulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace CodPulse.API.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContex dbContex;

        public BlogPostRepository(ApplicationDbContex dbContex)
        {
            this.dbContex = dbContex;
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await dbContex.BlogPosts.AddAsync(blogPost);
            await dbContex.SaveChangesAsync();
            return blogPost;
        }

        

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
           return  await dbContex.BlogPosts.Include(x => x.Categories).ToListAsync();
        }

        public async  Task<BlogPost?> GetByIdAsync(Guid id)
        {
           return await dbContex.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
           var existingBlogPost =  await dbContex.BlogPosts.Include(x =>x.Categories).FirstOrDefaultAsync(x =>x.Id == blogPost.Id);

            if (existingBlogPost != null) {
                return null;
            
            }

            //Update BlogPost
            dbContex.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);

            //Update Categories
            existingBlogPost.Categories = blogPost.Categories;

            await  dbContex.SaveChangesAsync();

            return blogPost;    


        }
        public async Task<BlogPost> DeleteAsync(Guid id)
        {
            var existingBlogPost = await dbContex.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
            if (existingBlogPost != null) { 
               dbContex.BlogPosts.Remove(existingBlogPost);
                await dbContex.SaveChangesAsync();
                return existingBlogPost;
            }
            return null;
        }

    }
}

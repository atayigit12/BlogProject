using BlogProject.DAL;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Service
{
    public class BlogService
    {
        private readonly BlogDbContext _context;

        public BlogService(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Blog>> GetAllBlogsAsync()
        {
            return await _context.Blogs.Include(b => b.User).ToListAsync();
        }

        public async Task<Blog> GetBlogByIdAsync(int id)
        {
            return await _context.Blogs.Include(b => b.User).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddBlogAsync(Blog blog)
        {
            if (blog == null)
            {
                throw new ArgumentNullException(nameof(blog), "Blog cannot be null.");
            }

            try
            {
                _context.Blogs.Add(blog);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw new InvalidOperationException("An error occurred while adding the blog.", ex);
            }
        }

        public async Task UpdateBlogAsync(Blog blog)
        {
            if (blog == null)
            {
                throw new ArgumentNullException(nameof(blog), "Blog cannot be null.");
            }

            try
            {
                _context.Blogs.Update(blog);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw new InvalidOperationException("An error occurred while updating the blog.", ex);
            }
        }

        public async Task DeleteBlogAsync(int id)
        {
            try
            {
                var blog = await _context.Blogs.FindAsync(id);
                if (blog != null)
                {
                    _context.Blogs.Remove(blog);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                throw new InvalidOperationException("An error occurred while deleting the blog.", ex);
            }
        }
    }
}

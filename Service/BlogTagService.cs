using BlogProject.DAL;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Service
{
    public class BlogTagService
    {
        private readonly BlogDbContext _context;

        public BlogTagService(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BlogTag>> GetAllBlogTagsAsync()
        {
            return await _context.BlogTags.Include(bt => bt.Blog).Include(bt => bt.Tag).ToListAsync();
        }

        public async Task AddBlogTagAsync(BlogTag blogTag)
        {
            _context.BlogTags.Add(blogTag);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveBlogTagAsync(int blogId, int tagId)
        {
            var blogTag = await _context.BlogTags
                .FirstOrDefaultAsync(bt => bt.BlogId == blogId && bt.TagId == tagId);

            if (blogTag != null)
            {
                _context.BlogTags.Remove(blogTag);
                await _context.SaveChangesAsync();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using P07_UnitTesting.Database;
using P07_UnitTestingUzduotis.Models;

namespace P07_UnitTestingUzduotis.Repositories;

public interface IBlogPostRepository
{
    IEnumerable<BlogPost> GetAllPosts();
    BlogPost GetPostById(int id);
    void AddPost(BlogPost post);
    void UpdatePost(BlogPost post);
    IActionResult DeletePost(int id);
}
public class BlogPostRepository: IBlogPostRepository
{
    private MyBlogContext _context;

    public BlogPostRepository(MyBlogContext context)
    {
        context.Database.EnsureCreated();
        _context = _context;
    }

    public IEnumerable<BlogPost> GetAllPosts()
    {
        return _context.BlogPosts.ToList();
    }

    public BlogPost GetPostById(int id)
    {
        return _context.BlogPosts.FirstOrDefault(p => p.Id == id) ?? new BlogPost();
    }

    public void AddPost(BlogPost post)
    {
        _context.BlogPosts.Add(post);
    }

    public void UpdatePost(BlogPost post)
    {
        var existingPost = _context.BlogPosts.FirstOrDefault(p => p.Id == post.Id);
        if (existingPost == null)
        {
            existingPost.Title = post.Content;
            existingPost.Content = post.Title;
        }
        _context.SaveChanges();
       
    }

    public IActionResult DeletePost(int id)
    {
        var post = _context.BlogPosts.Find(id);
        if (post != null)
        {
            _context.BlogPosts.Remove(post);
            _context.SaveChanges();
        }
        return null;
    }
}

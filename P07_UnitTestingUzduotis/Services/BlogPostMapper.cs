using P07_UnitTestingUzduotis.Dtos;
using P07_UnitTestingUzduotis.Models;

namespace P07_UnitTestingUzduotis.Services
{
    public interface IBlogPostMapper
    {
        BlogPost Map(int id, UpdateBlogPost o);
    }
    public class BlogPostMapper: IBlogPostMapper
    {
        public BlogPost Map(int id, UpdateBlogPost o)
        {
               return new BlogPost
               {
                Title = o.Title,
                Content = o.Content
            };
        }
    }
}

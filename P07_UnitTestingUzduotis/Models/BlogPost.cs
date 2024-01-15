namespace P07_UnitTestingUzduotis.Models;

public class BlogPost
{
    // Primary key
    public int Id { get; set; }

    // Title of the blog post
    public string Title { get; set; }

    // Content of the blog post
    public string Content { get; set; }

    // Date and time the post was created
    public DateTime CreatedAt { get; set; }

    // Date and time the post was last updated
    public DateTime UpdatedAt { get; set; }

    // Constructor to set default values
    public BlogPost()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}

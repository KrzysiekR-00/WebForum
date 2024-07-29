namespace WebForum.Models;

public class Post
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime DateTime { get; set; }
    public string Author { get; set; } = null!;

    public int TopicId { get; set; }
    public Topic Topic { get; set; } = null!;
}

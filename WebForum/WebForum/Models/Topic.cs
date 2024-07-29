namespace WebForum.Models;

public class Topic
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;

    public ICollection<Post> Posts { get; set; } = null!;
}

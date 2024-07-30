namespace WebForum.ViewModels;

public class NewPostViewModel
{
    public string Author { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int TopicId { get; set; }
}

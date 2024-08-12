using System.ComponentModel.DataAnnotations;

namespace WebForum.ViewModels;

public class NewPostViewModel
{
    [StringLength(128)]
    public string Content { get; set; } = null!;
    public int TopicId { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace WebForum.ViewModels;

public class NewPostViewModel
{
    [StringLength(128)]
    [DataType(DataType.MultilineText)]
    public string Content { get; set; } = null!;
    public int TopicId { get; set; }
}

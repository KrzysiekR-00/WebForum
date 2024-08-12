using System.ComponentModel.DataAnnotations;

namespace WebForum.ViewModels;

public class NewTopicViewModel
{
    [StringLength(32)]
    public string Title { get; set; } = null!;
    [StringLength(128)]
    public string Content { get; set; } = null!;
}

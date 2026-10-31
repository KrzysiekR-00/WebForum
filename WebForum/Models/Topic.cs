using System.ComponentModel.DataAnnotations;

namespace WebForum.Models;

public class Topic
{
    public int Id { get; set; }
    [StringLength(32)]
    public string Title { get; set; } = null!;

    public ICollection<Post> Posts { get; set; } = null!;
}

using System.ComponentModel.DataAnnotations;

namespace WebForum.Models;

public class Post
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    [StringLength(128)]
    [DataType(DataType.MultilineText)]
    public string Content { get; set; } = null!;

    public int TopicId { get; set; }
    public Topic Topic { get; set; } = null!;


    public string AuthorId { get; set; } = null!;
    public ApplicationUser Author { get; set; } = null!;
}

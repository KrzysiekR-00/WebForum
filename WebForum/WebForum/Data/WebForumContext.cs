using Microsoft.EntityFrameworkCore;
using WebForum.Models;

namespace WebForum.Data;

public class WebForumContext : DbContext
{
    public WebForumContext(DbContextOptions<WebForumContext> options) : base(options)
    {
    }

    public DbSet<Topic> Topics { get; set; }
    public DbSet<Post> Posts { get; set; }
}

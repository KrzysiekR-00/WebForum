using Microsoft.EntityFrameworkCore;
using WebForum.Models;

namespace WebForum.Data;

public static class DbInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new WebForumContext(serviceProvider.GetRequiredService<DbContextOptions<WebForumContext>>());

        context.Database.EnsureCreated();

        if (context.Topics.Any() || context.Posts.Any())
        {
            return;
        }

        var topics = new Topic[]
        {
            new() { Title = "Topic 1"},
            new() { Title = "Topic 2"},
            new() { Title = "Topic 3"}
        };
        foreach (var t in topics)
        {
            context.Topics.Add(t);
        }
        context.SaveChanges();

        var posts = new Post[]
        {
            new() { TopicId = 1, DateTime = DateTime.Parse("01-01-2020 10:00:00"), Author = "User 1", Content = "Content"},
            new() { TopicId = 2, DateTime = DateTime.Parse("01-01-2020 10:30:00"), Author = "User 1", Content = "Content"},
            new() { TopicId = 2, DateTime = DateTime.Parse("02-01-2020 18:00:00"), Author = "User 2", Content = "Content"},
            new() { TopicId = 3, DateTime = DateTime.Parse("20-10-2020 21:00:00"), Author = "User 1", Content = "Content"},
            new() { TopicId = 3, DateTime = DateTime.Parse("20-10-2020 23:00:00"), Author = "User 2", Content = "Content"},
            new() { TopicId = 3, DateTime = DateTime.Parse("20-10-2020 23:00:01"), Author = "User 2", Content = "Content\r\nContent"}
        };
        foreach (var p in posts)
        {
            context.Posts.Add(p);
        }
        context.SaveChanges();
    }
}

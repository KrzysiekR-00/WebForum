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

        int numberOfTopics = 45;
        Topic[] topics = new Topic[numberOfTopics];
        for (int i = 0; i < numberOfTopics; i++)
        {
            topics[i] = new() { Title = "Topic " + (i + 1) };
        }
        context.Topics.AddRange(topics);
        context.SaveChanges();

        int postsPerTopic = 3;
        Post[] posts = new Post[numberOfTopics * postsPerTopic];
        var postDateTime = DateTime.Parse("01-01-2020 10:00:00");
        int postIndex = 0;
        for (int i = 0; i < numberOfTopics; i++)
        {
            for (int j = 0; j < postsPerTopic; j++)
            {
                posts[postIndex] = new Post()
                {
                    TopicId = topics[i].Id,
                    DateTime = postDateTime,
                    Author = "User " + (j + 1),
                    Content = "Content"
                };

                postDateTime = postDateTime.AddMinutes(10);
                postIndex++;
            }
        }
        context.Posts.AddRange(posts);
        context.SaveChanges();
    }
}

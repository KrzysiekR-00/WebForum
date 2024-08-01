using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebForum.Models;

namespace WebForum.Data;

public static class DbInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using var context = new WebForumContext(serviceProvider.GetRequiredService<DbContextOptions<WebForumContext>>());

        context.Database.EnsureCreated();

        if (context.Topics.Any() ||
            context.Posts.Any() ||
            context.Users.Any())
        {
            return;
        }

        var userManager = (UserManager<ApplicationUser>)serviceProvider.GetService(typeof(UserManager<ApplicationUser>))!;

        int numberOfUsers = 3;
        ApplicationUser[] users = new ApplicationUser[numberOfUsers];
        for (int i = 0; i < numberOfUsers; i++)
        {
            users[i] = new ApplicationUser
            {
                UserName = "user" + (i + 1),
                Login = "user" + (i + 1)
            };
            var result = await userManager.CreateAsync(users[i], "11111111");
        }
        context.SaveChanges();

        int numberOfTopics = 25;
        Topic[] topics = new Topic[numberOfTopics];
        for (int i = 0; i < numberOfTopics; i++)
        {
            topics[i] = new() { Title = "Topic " + (i + 1) };
        }
        context.Topics.AddRange(topics);
        context.SaveChanges();

        Post[] posts = new Post[numberOfTopics * numberOfUsers];
        var postDateTime = DateTime.Parse("01-01-2020 10:00:00");
        int postIndex = 0;
        for (int i = 0; i < numberOfTopics; i++)
        {
            for (int j = 0; j < numberOfUsers; j++)
            {
                posts[postIndex] = new Post()
                {
                    TopicId = topics[i].Id,
                    DateTime = postDateTime,
                    AuthorId = users[j].Id,
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
